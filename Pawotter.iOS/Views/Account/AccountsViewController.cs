using System;
using CoreGraphics;
using Foundation;
using Pawotter.ViewModels;
using UIKit;

namespace Pawotter.iOS.Views.Account
{
    /// <summary>
    /// account list view
    /// </summary>
    public class AccountsViewController : BaseViewController
    {
        readonly AccountsViewModel viewModel;

        readonly UICollectionView collectionView = new UICollectionView(CGRect.Empty, new CollectionViewFlowLayout());

        public AccountsViewController(AccountsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = viewModel.Title;
            EdgesForExtendedLayout = UIRectEdge.None;
            collectionView.AlwaysBounceVertical = true;
            collectionView.RegisterClassForSupplementaryView(typeof(AccountHeader), UICollectionElementKindSection.Header, nameof(AccountHeader));
            collectionView.RegisterClassForCell(typeof(AccountCell), nameof(AccountCell));
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
            return collectionView.DequeueReusableCell(nameof(AccountCell), indexPath) as UICollectionViewCell;
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
            return new CGSize(collectionView.Width(), AccountCell.H);
        }

        [Export("scrollViewDidScroll:")]
        public void Scrolled(UIScrollView scrollView) { }

        [Export("collectionView:layout:referenceSizeForHeaderInSection:")]
        public CGSize GetReferenceSizeForHeader(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => new CGSize(collectionView.Width(), 0.1f);

        [Export("collectionView:viewForSupplementaryElementOfKind:atIndexPath:")]
        public UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
        {
            if (!elementKind.Equals(UICollectionElementKindSectionKey.Header)) return null;
            return collectionView.DequeueReusableSupplementaryView(UICollectionElementKindSectionKey.Header, nameof(AccountHeader), indexPath) as AccountHeader;
        }
        #endregion
    }
}
