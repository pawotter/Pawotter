using CoreGraphics;
using Pawotter.ViewModels;
using Pawotter.iOS.Views.Components;
using UIKit;
using System;

namespace Pawotter.iOS.Views.Timeline
{
    public sealed class TimelineCell : BaseCollectionViewCell
    {
        public static nfloat H(TimelineItemViewModel viewModel, nfloat width)
        {
            nfloat h = 0f;
            h += L.PaddingL;
            h += TimelineItemHeader.H.PlusPadding();
            h += TimelineItemImages.H(width).PlusPadding();
            h += TimelineItemButtons.H.PlusPadding();
            return h;
        }

        readonly UserIcon userIcon = new UserIcon();
        readonly TimelineItemHeader header = new TimelineItemHeader();
        readonly TimelineItemImages images = new TimelineItemImages();
        readonly TimelineItemButtons buttons = new TimelineItemButtons();

        public TimelineCell() { CommonInit(); }
        public TimelineCell(IntPtr handle) : base(handle) { CommonInit(); }

        void CommonInit()
        {
            ContentView.AddSubviews(userIcon, header, images, buttons);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            userIcon.Frame = new CGRect(L.PaddingL, L.PaddingL, L.UserIcon.Width, L.UserIcon.Height);
            var textAreaX = userIcon.MaxX().PlusPadding();
            var textAreaW = (this.Width() - userIcon.MaxX()).MinusDoublePadding();
            header.Frame = new CGRect(textAreaX, userIcon.MinY(), textAreaW, TimelineItemHeader.H);
            images.Frame = new CGRect(textAreaX, header.MaxY().PlusPadding(), textAreaW, TimelineItemImages.H(this.Width()));
            buttons.Frame = new CGRect(textAreaX, images.MaxY().PlusPadding(), this.Width().MinusPadding() - textAreaX, L.Icon.Height);

            // fixme
            header.BackgroundColor = UIColor.Orange;
            images.BackgroundColor = UIColor.Orange;
            buttons.BackgroundColor = UIColor.Orange;
        }

        public void Update(TimelineItemViewModel viewModel)
        {
            header.Update(viewModel);
            SetNeedsLayout();
        }
    }
}
