using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Pawotter.iOS.Views.Libs.SwipePage
{
    class TabsView : UIView
    {
        readonly UICollectionView collectionView;
        readonly UIView border = new UIView();
        readonly SwipePageViewConfig config;

        public NSObject WeakDataSource { set { collectionView.WeakDataSource = value; } }
        public NSObject WeakDelegate { set { collectionView.WeakDelegate = value; } }
        public void ScrollToItem(NSIndexPath indexPath, UICollectionViewScrollPosition scrollPosition, bool animated) => collectionView.ScrollToItem(indexPath, scrollPosition, animated);
        public UICollectionViewLayoutAttributes GetLayoutAttributesForItem(NSIndexPath indexPath) => collectionView.GetLayoutAttributesForItem(indexPath);

        internal TabsView(SwipePageViewConfig config)
        {
            this.config = config;
            var layout = new UICollectionViewFlowLayout();
            layout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;
            layout.SectionInset = UIEdgeInsets.Zero;
            collectionView = new UICollectionView(CGRect.Empty, layout);
            collectionView.RegisterClassForCell(typeof(TabCollectionViewCell), typeof(TabCollectionViewCell).Name);
            collectionView.BackgroundColor = config.BackgroundColor;
            collectionView.ShowsVerticalScrollIndicator = false;
            collectionView.ShowsHorizontalScrollIndicator = false;
            border.BackgroundColor = config.BorderColor;
            AddSubviews(collectionView, border);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            collectionView.Frame = Frame;
            border.Frame = new CGRect(0, Frame.Height - config.BorderHeight, Frame.Width, config.BorderHeight);
        }

        public void OnIndexUpdated(int currentIndex)
        {
            foreach (var cell in collectionView.VisibleCells)
            {
                if (cell is TabCollectionViewCell c)
                {
                    c.OnIndexUpdated(currentIndex);
                }
            }
        }
    }

    class TabCollectionViewCell : UICollectionViewCell
    {
        readonly UILabel label = new UILabel();
        SwipePageViewConfig config = new SwipePageViewConfig();
        int row;

        TabCollectionViewCell(IntPtr handle) : base(handle) { Initialize(); }

        void Initialize()
        {
            label.TextAlignment = UITextAlignment.Center;
            ContentView.AddSubviews(label);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            label.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);
        }

        internal void Update(string text, SwipePageViewConfig config, int row, int currentIndex)
        {
            this.row = row;
            if (this.config != config)
            {
                this.config = config;
                BackgroundColor = config.BackgroundColor;
                label.Font = config.TitleFont;
                label.TextColor = row == currentIndex ? config.ActiveColor : config.InactiveColor;
            }
            label.Text = text;
        }

        internal void OnIndexUpdated(int currentIndex) => label.TextColor = row == currentIndex ? config.ActiveColor : config.InactiveColor;

        public override CGSize IntrinsicContentSize => new CGSize(label.Frame.Width + config.TabMarginX * 2, config.TabHeight);
    }
}
