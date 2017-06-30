using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Pawotter.iOS
{
    /// <summary>
    /// UIPageViewController + page title tabs
    /// </summary>
    public class SwipePageViewController : UIViewController, IUIScrollViewDelegate, IUICollectionViewDelegateFlowLayout, IUIPageViewControllerDelegate, IUICollectionViewDataSource, IUIPageViewControllerDataSource
    {
        public SwipePageViewConfig Config { get; set; } = new SwipePageViewConfig();
        public NSObject WeakDataSource { set { pageViewController.WeakDataSource = value; } }

        readonly UIPageViewController pageViewController;
        readonly TabsView tabsView;
        readonly TabIndicatorView indicatorView;
        nfloat currentPageContentOffset;

        int CurrentPageIndex { get { return Config.ViewControllers.IndexOf(pageViewController.ViewControllers.First()); } }

        public SwipePageViewController(SwipePageViewConfig config)
        {
            Config = config;
            pageViewController = new UIPageViewController(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal);
            tabsView = new TabsView(config);
            indicatorView = new TabIndicatorView(config);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            pageViewController.WeakDelegate = this;
            pageViewController.WeakDataSource = this;
            pageViewController.SetViewControllers(new UIViewController[] { Config.ViewControllers.First() }, UIPageViewControllerNavigationDirection.Forward, true, null);
            tabsView.CollectionView.WeakDataSource = this;
            tabsView.CollectionView.WeakDelegate = this;
            AddChildViewController(pageViewController);
            View.AddSubview(tabsView);
            View.AddSubview(pageViewController.View);
            tabsView.CollectionView.AddSubview(indicatorView);
            tabsView.CollectionView.BringSubviewToFront(indicatorView);
            DidMoveToParentViewController(this);
            foreach (var view in pageViewController.View.Subviews)
            {
                if (view is UIScrollView scrollView) scrollView.WeakDelegate = this;
            }
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            var tabHeight = Config.TabHeight;
            tabsView.Frame = new CGRect(0, 0, View.Frame.Width, tabHeight);
            pageViewController.View.Frame = new CGRect(0, tabHeight, View.Frame.Width, View.Frame.Height - tabHeight);
            LayoutIndicatorView();
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
            var borderValue = (scrollView.ContentOffset.X - currentPageContentOffset) / View.Frame.Width;
            if (-0.5 < borderValue && borderValue < 0.5) return;
            if (-0.5 >= borderValue) return;
            if (0.5 >= borderValue) return;
        }
        #endregion

        #region IUICollectionViewDelegateFlowLayout
        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var textWidth = Config.TabTitleFont.Width(Config.ViewControllers.ElementAt(indexPath.Row).Title);
            var width = textWidth + Config.TabMarginX * 2;
            return new CGSize(width, Config.TabHeight);
        }

        [Export("collectionView:didSelectItemAtIndexPath:")]
        public void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            if (CurrentPageIndex != indexPath.Row)
            {
                var direction = CurrentPageIndex < indexPath.Row ? UIPageViewControllerNavigationDirection.Forward : UIPageViewControllerNavigationDirection.Reverse;
                pageViewController.SetViewControllers(new UIViewController[] { Config.ViewControllers.ElementAt(indexPath.Row) }, direction, true, _ => IndexUpdated(indexPath.Row));
            }
        }

        [Export("collectionView:layout:minimumLineSpacingForSectionAtIndex:")]
        public nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => 0;

        [Export("collectionView:layout:minimumInteritemSpacingForSectionAtIndex:")]
        public nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => 0;
        #endregion

        #region IUICollectionViewDataSource
        [Export("collectionView:numberOfItemsInSection:")]
        public nint GetItemsCount(UICollectionView collectionView, nint section) => Config.ViewControllers.Count();

        [Export("collectionView:cellForItemAtIndexPath:")]
        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (collectionView.DequeueReusableCell(typeof(TabCollectionViewCell).Name, indexPath) as TabCollectionViewCell) ?? new TabCollectionViewCell(CGRect.Empty);
            var index = indexPath.Row;
            var title = index < Config.ViewControllers.Count() ? Config.ViewControllers.ElementAt(index).Title : "";
            cell.Update(title, Config);
            return cell;
        }
        #endregion

        #region IUIPageViewControllerDelegate
        [Export("pageViewController:didFinishAnimating:previousViewControllers:transitionCompleted:")]
        public void DidFinishAnimating(UIPageViewController pageViewController, bool finished, UIViewController[] previousViewControllers, bool completed)
        {
            if (completed)
            {
                var index = Config.ViewControllers.IndexOf(pageViewController.ViewControllers.First());
                IndexUpdated(index);
            }
        }
        #endregion

        #region IUIPageViewControllerDataSource
        [Export("pageViewController:viewControllerAfterViewController:")]
        public UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            if (!Config.ViewControllers.Contains(referenceViewController)) return null;
            if (referenceViewController == Config.ViewControllers.Last()) return null;
            var index = Config.ViewControllers.IndexOf(referenceViewController);
            return Config.ViewControllers.ElementAt(index + 1);
        }

        [Export("pageViewController:viewControllerBeforeViewController:")]
        public UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            if (!Config.ViewControllers.Contains(referenceViewController)) return null;
            if (referenceViewController == Config.ViewControllers.First()) return null;
            var index = Config.ViewControllers.IndexOf(referenceViewController);
            return Config.ViewControllers.ElementAt(index - 1);
        }
        #endregion

        void IndexUpdated(nint row)
        {
            tabsView.CollectionView.ScrollToItem(NSIndexPath.FromRowSection(row, 0), UICollectionViewScrollPosition.CenteredHorizontally, true);
            UIView.Animate(0.2, () => LayoutIndicatorView());
        }

        void LayoutIndicatorView()
        {
            var cellFrame = tabsView.CollectionView.GetLayoutAttributesForItem(NSIndexPath.FromRowSection(CurrentPageIndex, 0)).Frame;
            indicatorView.Frame = new CGRect(cellFrame.X + Config.TabMarginX, Config.TabHeight - Config.IndicatorWidth, cellFrame.Width - 2 * Config.TabMarginX, Config.IndicatorWidth);
        }
    }

    class TabCollectionViewCell : UICollectionViewCell
    {
        readonly UILabel label = new UILabel();
        SwipePageViewConfig config = new SwipePageViewConfig();

        internal TabCollectionViewCell(CGRect frame) : base(frame) { Initialize(); }
        internal TabCollectionViewCell(IntPtr handle) : base(handle) { Initialize(); }

        void Initialize()
        {
            label.TextAlignment = UITextAlignment.Center;
            ContentView.AddSubview(label);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            label.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);
        }

        internal void Update(string text, SwipePageViewConfig config)
        {
            if (this.config != config)
            {
                this.config = config;
                BackgroundColor = config.TabBackgroundColor;
                label.Font = config.TabTitleFont;
            }
            label.Text = text;
        }

        public override CGSize IntrinsicContentSize
        {
            get { return new CGSize(label.Frame.Width + config.TabMarginX * 2, config.TabHeight); }
        }
    }

    class TabsView : UIView
    {
        internal UICollectionView CollectionView;
        SwipePageViewConfig config;

        internal TabsView(SwipePageViewConfig config)
        {
            this.config = config;
            var layout = new UICollectionViewFlowLayout();
            layout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;
            layout.SectionInset = new UIEdgeInsets(0, 0, 0, 0);
            CollectionView = new UICollectionView(CGRect.Empty, layout);
            CollectionView.RegisterClassForCell(typeof(TabCollectionViewCell), typeof(TabCollectionViewCell).Name);
            CollectionView.BackgroundColor = config.TabBackgroundColor;
            CollectionView.ShowsVerticalScrollIndicator = false;
            CollectionView.ShowsHorizontalScrollIndicator = false;
            AddSubview(CollectionView);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            CollectionView.Frame = Frame;
        }
    }

    class TabIndicatorView : UIView
    {
        public TabIndicatorView(SwipePageViewConfig config)
        {
            BackgroundColor = config.IndicatorColor;
        }
    }
}
