using CoreGraphics;
using UIKit;

namespace Pawotter.iOS.Views
{
    public sealed class CollectionViewFlowLayout : UICollectionViewFlowLayout
    {
        public CollectionViewFlowLayout()
        {
            SectionInset = UIEdgeInsets.Zero;
            ScrollDirection = UICollectionViewScrollDirection.Vertical;
            MinimumInteritemSpacing = L.BorderW;
            MinimumLineSpacing = L.BorderW;
        }

        public override bool ShouldInvalidateLayoutForBoundsChange(CGRect newBounds) => true;
    }
}
