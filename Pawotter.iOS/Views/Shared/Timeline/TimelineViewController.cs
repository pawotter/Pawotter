using System;
using UIKit;
using CoreGraphics;
using Foundation;
using Pawotter.ViewModels;

namespace Pawotter.iOS
{
    public class TimelineViewController : UIViewController, IUICollectionViewDelegateFlowLayout, IUICollectionViewDataSource
    {
        readonly TimelineViewModel viewModel = new TimelineViewModel();
        static string DummyText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        readonly UICollectionViewLayout layout;
        readonly UICollectionView collectionView;

        public TimelineViewController()
        {
            layout = new UICollectionViewFlowLayout();
            collectionView = new UICollectionView(CGRect.Empty, layout);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            EdgesForExtendedLayout = UIRectEdge.None;
            collectionView.BackgroundColor = UIConst.TimelineBackgroundColor;
            collectionView.AlwaysBounceVertical = true;
            collectionView.RegisterClassForCell(typeof(CardCell), typeof(CardCell).Name);
            collectionView.WeakDelegate = this;
            collectionView.WeakDataSource = this;
            View.AddSubview(collectionView);
        }

        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);
            layout.InvalidateLayout();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            collectionView.Frame = View.Bounds;
        }

        #region DataSource and Delegate
        [Export("collectionView:numberOfItemsInSection:")]
        public nint GetItemsCount(UICollectionView collectionView, nint section) => 1;

        [Export("numberOfSectionsInCollectionView:")]
        public nint NumberOfSections(UICollectionView collectionView) => 20;

        [Export("collectionView:cellForItemAtIndexPath:")]
        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(typeof(CardCell).Name, indexPath) as CardCell;
            cell.Update("display_mame", "user_name", DateTime.Now, DummyText);
            return cell;
        }

        [Export("collectionView:viewForSupplementaryElementOfKind:atIndexPath:")]
        public UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath) => null;

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath) => new CGSize(collectionView.Frame.Width, CardCell.Height(DummyText, collectionView.Frame.Width));

        [Export("collectionView:layout:referenceSizeForHeaderInSection:")]
        public CGSize GetReferenceSizeForHeader(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => CGSize.Empty;

        [Export("collectionView:layout:referenceSizeForFooterInSection:")]
        public CGSize GetReferenceSizeForFooter(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => CGSize.Empty;

        [Export("collectionView:didSelectItemAtIndexPath:")]
        public void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            Console.WriteLine("DisplayName");
        }

        [Export("collectionView:layout:minimumLineSpacingForSectionAtIndex:")]
        public nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => 0;

        [Export("collectionView:layout:minimumInteritemSpacingForSectionAtIndex:")]
        public nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => 0;

        [Export("collectionView:layout:insetForSectionAtIndex:")]
        public UIEdgeInsets GetInsetForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section) => new UIEdgeInsets(0.5f, 0.0f, 0.0f, 0.5f);
        #endregion
    }

}
