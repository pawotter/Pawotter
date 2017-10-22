using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Pawotter.iOS.Views.Notification
{
    public sealed class NotificationViewController : BaseViewController, IUICollectionViewDelegate, IUICollectionViewDataSource
    {
        public enum DisplayMode
        {
            MainTab,
        }

        readonly DisplayMode mode;
        readonly UICollectionView collectionView = new UICollectionView(CGRect.Empty, new CollectionViewFlowLayout());

        public NotificationViewController(DisplayMode mode)
        {
            this.mode = mode;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            EdgesForExtendedLayout = UIRectEdge.None;
            collectionView.WeakDelegate = this;
            collectionView.WeakDataSource = this;
            collectionView.AlwaysBounceVertical = true;
            collectionView.RegisterClassForSupplementaryView(typeof(NotificationHeader), UICollectionElementKindSection.Header, nameof(NotificationHeader));
            collectionView.RegisterClassForCell(typeof(UICollectionViewCell), nameof(UICollectionViewCell));
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
                    TabBarController.Title = "Notifications";
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
            return collectionView.DequeueReusableCell(typeof(UICollectionViewCell).Name, indexPath) as UICollectionViewCell;
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

        [Export("collectionView:layout:referenceSizeForHeaderInSection:")]
        public CGSize GetReferenceSizeForHeader(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => new CGSize(collectionView.Width(), L.Banner);

        [Export("collectionView:viewForSupplementaryElementOfKind:atIndexPath:")]
        public UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
        {
            if (!elementKind.Equals(UICollectionElementKindSectionKey.Header)) return null;
            return collectionView.DequeueReusableSupplementaryView(UICollectionElementKindSectionKey.Header, nameof(NotificationHeader), indexPath) as NotificationHeader;
        }
        #endregion
    }
}
