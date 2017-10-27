using UIKit;
using CoreGraphics;
using Foundation;
using System;
using Pawotter.ViewModels;
using Pawotter.iOS.Views.Components;
using Pawotter.iOS.Views.Search;

namespace Pawotter.iOS.Views.Timeline
{
    public sealed class TimelineViewController : BaseViewController, IUICollectionViewDataSource, IUICollectionViewDelegate, IScrollableViewController
    {
        public enum DisplayMode
        {
            MainTab,
        }

        readonly ITimelineViewModel viewModel;
        readonly DisplayMode mode;

        readonly UICollectionView collectionView = new UICollectionView(CGRect.Empty, new CollectionViewFlowLayout());

        public TimelineViewController(ITimelineViewModel viewModel, DisplayMode mode)
        {
            this.viewModel = viewModel;
            this.mode = mode;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            EdgesForExtendedLayout = UIRectEdge.None;
            collectionView.AlwaysBounceVertical = true;
            collectionView.RegisterClassForCell(typeof(TimelineCell), nameof(TimelineCell));
            View.AddSubviews(collectionView);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            collectionView.Frame = View.Bounds;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            collectionView.WeakDelegate = this;
            collectionView.WeakDataSource = this;
            switch (mode)
            {
                case DisplayMode.MainTab:
                    if (TabBarController == null) break;
                    TabBarController.Title = viewModel.Title;
                    var itemL = new UIBarButtonItem(R.Edit, UIBarButtonItemStyle.Plain, (sender, e) => Application.Router.PresentViewController(AppNavigationController.OfModalStyle(new TimelineItemEditorViewController(new TimelineItemEditorViewModel())), true, null));
                    TabBarController.NavigationItem.RightBarButtonItem = itemL;
                    var itemR = new UIBarButtonItem(R.Search, UIBarButtonItemStyle.Plain, (sender, e) => Application.Router.PresentViewController(AppNavigationController.OfModalStyle(new SearchViewController(new SearchViewModel())), true, null));
                    TabBarController.NavigationItem.LeftBarButtonItem = itemR;
                    break;
            }
        }

        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);
            collectionView.CollectionViewLayout.InvalidateLayout();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            collectionView.WeakDelegate = null;
            collectionView.WeakDataSource = null;
        }

        #region IUICollectionViewDataSource
        [Export("collectionView:cellForItemAtIndexPath:")]
        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(typeof(TimelineCell).Name, indexPath) as TimelineCell;
            cell.Update(new TimelineItemViewModel());
            return cell;
        }

        [Export("collectionView:numberOfItemsInSection:")]
        public nint GetItemsCount(UICollectionView collectionView, nint section) => 30;

        [Export("numberOfSectionsInCollectionView:")]
        public nint NumberOfSections(UICollectionView collectionView) => 1;
        #endregion

        #region IUICollectionViewDelegateFlowLayout
        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var width = collectionView.Width();
            return new CGSize(width, TimelineCell.H(new TimelineItemViewModel(), width));
        }

        [Export("scrollViewDidScroll:")]
        public void Scrolled(UIScrollView scrollView) { }

        [Export("collectionView:layout:referenceSizeForFooterInSection:")]
        public CGSize GetReferenceSizeForFooter(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => new CGSize(collectionView.Width(), 0.1f);

        [Export("collectionView:didSelectItemAtIndexPath:")]
        public void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            NavigationController?.PushViewController(new TimelineItemViewController(new TimelineItemViewModel()), true);
        }
        #endregion

        public void ScrollsToTop() => collectionView.SetContentOffset(CGPoint.Empty, true);
    }
}
