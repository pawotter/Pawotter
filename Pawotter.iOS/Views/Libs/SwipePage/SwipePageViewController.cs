using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Pawotter.iOS.Views.Libs.SwipePage
{
    public class SwipePageViewController : BaseViewController, IUICollectionViewDelegateFlowLayout, IUIPageViewControllerDelegate, IUICollectionViewDataSource, IUIPageViewControllerDataSource, IUIScrollViewDelegate
    {
        public NSObject WeakDataSource { set { pageViewController.WeakDataSource = value; } }

        readonly UIPageViewController pageViewController;
        readonly SwipePageViewConfig config;
        readonly TabsView tabs;
        readonly UIView indicator = new UIView();
        nfloat currentPageContentOffset;

        int CurrentPageIndex => config.ViewControllers.IndexOf(pageViewController.ViewControllers.First());

        public SwipePageViewController(SwipePageViewConfig config)
        {
            this.config = config;
            pageViewController = new UIPageViewController(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal);
            tabs = new TabsView(config);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            pageViewController.SetViewControllers(new UIViewController[] { config.ViewControllers.First() }, UIPageViewControllerNavigationDirection.Forward, true, null);
            AddChildViewController(pageViewController);
            View.AddSubviews(tabs, pageViewController.View, indicator);
            indicator.BackgroundColor = config.ActiveColor;
            tabs.BringSubviewToFront(indicator);
            DidMoveToParentViewController(this);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            var tabHeight = config.TabHeight;
            tabs.Frame = new CGRect(0, 0, View.Frame.Width, tabHeight);
            pageViewController.View.Frame = new CGRect(0, tabHeight, View.Frame.Width, View.Frame.Height - tabHeight);
            OnIndexUpdated();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            pageViewController.WeakDelegate = this;
            pageViewController.WeakDataSource = this;
            tabs.WeakDataSource = this;
            tabs.WeakDelegate = this;
            foreach (var view in pageViewController.View.Subviews)
            {
                if (view is UIScrollView scrollView) scrollView.WeakDelegate = this;
            }
        }


        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            foreach (var view in pageViewController.View.Subviews)
            {
                if (view is UIScrollView scrollView) scrollView.WeakDelegate = null;
            }
            tabs.WeakDataSource = null;
            tabs.WeakDelegate = null;
            pageViewController.WeakDelegate = null;
            pageViewController.WeakDataSource = null;
        }

        public void SetViewControllers(UIViewController[] viewControllers, UIPageViewControllerNavigationDirection direction, bool animated, UICompletionHandler completionHandler)
        {
            pageViewController.SetViewControllers(viewControllers, direction, animated, completionHandler);
        }

        #region IUIScrollViewDelegate
        [Export("scrollViewWillBeginDragging:")]
        public void DraggingStarted(UIScrollView scrollView)
        {
            IndexUpdated(CurrentPageIndex);
            currentPageContentOffset = scrollView.ContentOffset.X;
        }

        [Export("scrollViewDidScroll:")]
        public void Scrolled(UIScrollView scrollView)
        {
            var borderValue = ( scrollView.ContentOffset.X - currentPageContentOffset ) / View.Frame.Width;
            if (-0.5 < borderValue && borderValue < 0.5) return;
            if (-0.5 >= borderValue) return;
            if (0.5 >= borderValue) return;
        }
        #endregion

        #region IUICollectionViewDelegateFlowLayout
        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var textWidth = config.TitleFont.W(config.ViewControllers.ElementAt(indexPath.Row).Title);
            var width = textWidth + config.TabMarginX * 2;
            return new CGSize(width, config.TabHeight);
        }

        [Export("collectionView:didSelectItemAtIndexPath:")]
        public void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            if (CurrentPageIndex != indexPath.Row)
            {
                var direction = CurrentPageIndex < indexPath.Row ? UIPageViewControllerNavigationDirection.Forward : UIPageViewControllerNavigationDirection.Reverse;
                pageViewController.SetViewControllers(new UIViewController[] { config.ViewControllers.ElementAt(indexPath.Row) }, direction, true, _ => IndexUpdated(indexPath.Row));
            }
        }

        [Export("collectionView:layout:minimumLineSpacingForSectionAtIndex:")]
        public nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => 0;

        [Export("collectionView:layout:minimumInteritemSpacingForSectionAtIndex:")]
        public nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => 0;
        #endregion

        #region IUICollectionViewDataSource
        [Export("collectionView:numberOfItemsInSection:")]
        public nint GetItemsCount(UICollectionView collectionView, nint section) => config.ViewControllers.Count();

        [Export("collectionView:cellForItemAtIndexPath:")]
        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(typeof(TabCollectionViewCell).Name, indexPath) as TabCollectionViewCell;
            var index = indexPath.Row;
            var title = index < config.ViewControllers.Count() ? config.ViewControllers.ElementAt(index).Title : "";
            cell.Update(title, config, indexPath.Row, CurrentPageIndex);
            return cell;
        }
        #endregion

        #region IUIPageViewControllerDelegate
        [Export("pageViewController:didFinishAnimating:previousViewControllers:transitionCompleted:")]
        public void DidFinishAnimating(UIPageViewController pageViewController, bool finished, UIViewController[] previousViewControllers, bool completed)
        {
            if (completed)
            {
                var index = config.ViewControllers.IndexOf(pageViewController.ViewControllers.First());
                IndexUpdated(index);
            }
        }
        #endregion

        #region IUIPageViewControllerDataSource
        [Export("pageViewController:viewControllerAfterViewController:")]
        public UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            if (!config.ViewControllers.Contains(referenceViewController)) return null;
            if (referenceViewController == config.ViewControllers.Last()) return null;
            var index = config.ViewControllers.IndexOf(referenceViewController);
            return config.ViewControllers.ElementAt(index + 1);
        }

        [Export("pageViewController:viewControllerBeforeViewController:")]
        public UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            if (!config.ViewControllers.Contains(referenceViewController)) return null;
            if (referenceViewController == config.ViewControllers.First()) return null;
            var index = config.ViewControllers.IndexOf(referenceViewController);
            return config.ViewControllers.ElementAt(index - 1);
        }
        #endregion

        void IndexUpdated(nint row)
        {
            tabs.ScrollToItem(NSIndexPath.FromRowSection(row, 0), UICollectionViewScrollPosition.CenteredHorizontally, true);
            UIView.Animate(0.1, () => OnIndexUpdated());
        }

        void OnIndexUpdated()
        {
            var cellFrame = tabs.GetLayoutAttributesForItem(NSIndexPath.FromRowSection(CurrentPageIndex, 0)).Frame;
            indicator.Frame = new CGRect(cellFrame.X, config.TabHeight - config.IndicatorWidth, cellFrame.Width, config.IndicatorWidth);
            tabs.OnIndexUpdated(CurrentPageIndex);
        }
    }
}
