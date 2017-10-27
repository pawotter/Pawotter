using UIKit;
using CoreGraphics;
using Pawotter.ViewModels;
using Foundation;
using System;

namespace Pawotter.iOS.Views.Search
{
    public class SearchViewController : BaseViewController
    {
        readonly SearchViewModel viewModel;

        readonly UICollectionView collectionView = new UICollectionView(CGRect.Empty, new CollectionViewFlowLayout());

        public SearchViewController(SearchViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = viewModel.Title;
            EdgesForExtendedLayout = UIRectEdge.None;
            collectionView.AlwaysBounceVertical = true;
            collectionView.RegisterClassForSupplementaryView(typeof(SearchHeader), UICollectionElementKindSection.Header, nameof(SearchHeader));
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
            return collectionView.DequeueReusableCell(nameof(UICollectionViewCell), indexPath) as UICollectionViewCell;
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
        public CGSize GetReferenceSizeForHeader(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => new CGSize(collectionView.Width(), 0.1f);

        [Export("collectionView:viewForSupplementaryElementOfKind:atIndexPath:")]
        public UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
        {
            if (!elementKind.Equals(UICollectionElementKindSectionKey.Header)) return null;
            return collectionView.DequeueReusableSupplementaryView(UICollectionElementKindSectionKey.Header, nameof(SearchHeader), indexPath) as SearchHeader;
        }
        #endregion
    }
}
