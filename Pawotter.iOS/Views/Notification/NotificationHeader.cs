using CoreGraphics;
using UIKit;

namespace Pawotter.iOS.Views.Notification
{
    public sealed class NotificationHeader : UICollectionReusableView
    {
        readonly UISegmentedControl segmentedControl = new UISegmentedControl("All", "Mentions") { SelectedSegment = 0 };
        readonly Border bottomBorder = new Border();

        public NotificationHeader() { CommonInit(); }
        public NotificationHeader(System.IntPtr handle) : base(handle) { CommonInit(); }

        void CommonInit()
        {
            AddSubviews(segmentedControl, bottomBorder);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            segmentedControl.Frame = new CGRect(L.PaddingM, L.PaddingM, this.Width().WithPadding(), L.Banner.WithPadding());
            bottomBorder.Frame = new CGRect(0, this.Height() - L.BorderW, this.Width(), L.BorderW);
        }
    }
}
