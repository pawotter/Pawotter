using UIKit;
using CoreGraphics;
using Foundation;
using System;
using Pawotter.ViewModels;

namespace Pawotter.iOS.Views.Timeline
{
    public sealed class TimelineViewController : BaseViewController, IUICollectionViewDataSource, IUICollectionViewDelegate
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
            collectionView.WeakDelegate = this;
            collectionView.WeakDataSource = this;
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
            switch (mode)
            {
                case DisplayMode.MainTab:
                    TabBarController.Title = viewModel.Title;
                    var vc = new NavigationController(new StatusEditorViewController(new StatusEditorViewModel()));
                    var item = new UIBarButtonItem(R.Edit, UIBarButtonItemStyle.Plain, (sender, e) => PresentViewController(vc, true, null));
                    TabBarController.NavigationItem.RightBarButtonItem = item;
                    break;
            }
        }

        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);
            collectionView.CollectionViewLayout.InvalidateLayout();
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
        public nint GetItemsCount(UICollectionView collectionView, nint section) => 10;

        [Export("numberOfSectionsInCollectionView:")]
        public nint NumberOfSections(UICollectionView collectionView) => 1;
        #endregion

        #region IUICollectionViewDelegateFlowLayout
        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            return new CGSize(collectionView.Width(), 100);
        }

        [Export("scrollViewDidScroll:")]
        public void Scrolled(UIScrollView scrollView) { }

        [Export("collectionView:layout:referenceSizeForFooterInSection:")]
        public CGSize GetReferenceSizeForFooter(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => new CGSize(collectionView.Width(), 0.1f);
        #endregion
    }
}
