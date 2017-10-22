using CoreGraphics;
using Pawotter.ViewModels;
using Pawotter.iOS.Views.Components;
using UIKit;

namespace Pawotter.iOS.Views.Timeline
{
    public sealed class TimelineCell : BaseCollectionViewCell
    {
        readonly UserIcon userIcon = new UserIcon();
        readonly TimelineItemHeader header = new TimelineItemHeader();

        public TimelineCell() { CommonInit(); }
        public TimelineCell(System.IntPtr handle) : base(handle) { CommonInit(); }

        void CommonInit()
        {
            ContentView.AddSubviews(userIcon, header);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            userIcon.Frame = new CGRect(L.PaddingL, L.PaddingL, L.UserIcon.Width, L.UserIcon.Height);
            header.Frame = new CGRect(userIcon.MaxX().PlusPadding(), userIcon.MinY(), (this.Width() - userIcon.MaxX()).MinusDoublePadding(), TimelineItemHeader.H);
            header.BackgroundColor = UIColor.Orange;
        }

        public void Update(TimelineItemViewModel viewModel)
        {
            header.Update(viewModel);
            SetNeedsLayout();
        }
    }
}
