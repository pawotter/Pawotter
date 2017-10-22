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
        readonly TimelineItemButtons buttons = new TimelineItemButtons();

        public TimelineCell() { CommonInit(); }
        public TimelineCell(System.IntPtr handle) : base(handle) { CommonInit(); }

        void CommonInit()
        {
            ContentView.AddSubviews(userIcon, header, buttons);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            userIcon.Frame = new CGRect(L.PaddingL, L.PaddingL, L.UserIcon.Width, L.UserIcon.Height);
            var textAreaX = userIcon.MaxX().PlusPadding();
            header.Frame = new CGRect(textAreaX, userIcon.MinY(), (this.Width() - userIcon.MaxX()).MinusDoublePadding(), TimelineItemHeader.H);
            buttons.Frame = new CGRect(textAreaX, this.Height() - L.Icon.Height.PlusHalfPadding(), this.Width().MinusPadding() - textAreaX, L.Icon.Height);

            // fixme
            header.BackgroundColor = UIColor.Orange;
            buttons.BackgroundColor = UIColor.Orange;
        }

        public void Update(TimelineItemViewModel viewModel)
        {
            header.Update(viewModel);
            SetNeedsLayout();
        }
    }
}
