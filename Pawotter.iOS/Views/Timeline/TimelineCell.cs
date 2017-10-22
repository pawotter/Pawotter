using CoreGraphics;
using Pawotter.ViewModels;
using Pawotter.iOS.Views.Components;
using UIKit;
using System;

namespace Pawotter.iOS.Views.Timeline
{
    public sealed class TimelineCell : BaseCollectionViewCell
    {
        static nfloat TextAreaW(nfloat width) => (width - L.UserIcon.Width.PlusHalfPadding()).MinusDoublePadding();
        public static nfloat H(TimelineItemViewModel viewModel, nfloat width)
        {
            var textAreaW = TextAreaW(width);
            nfloat h = 0f;
            h += L.PaddingL;
            h += TimelineItemHeader.H.PlusHalfPadding();
            h += TimelineItemStatus.H(viewModel.Status, textAreaW).PlusHalfPadding();
            h += TimelineItemImages.H(textAreaW).PlusHalfPadding();
            h += TimelineItemButtons.H.PlusPadding();
            return h;
        }

        readonly UserIcon userIcon = new UserIcon();
        readonly TimelineItemHeader header = new TimelineItemHeader();
        readonly TimelineItemStatus status = new TimelineItemStatus();
        readonly TimelineItemImages images = new TimelineItemImages();
        readonly TimelineItemButtons buttons = new TimelineItemButtons();

        public TimelineCell() { CommonInit(); }
        public TimelineCell(IntPtr handle) : base(handle) { CommonInit(); }

        void CommonInit()
        {
            ContentView.AddSubviews(userIcon, header, status, images, buttons);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            userIcon.Frame = new CGRect(L.PaddingL, L.PaddingL, L.UserIcon.Width, L.UserIcon.Height);
            var textAreaX = userIcon.MaxX().PlusHalfPadding();
            var textAreaW = TextAreaW(this.Width());
            header.Frame = new CGRect(textAreaX, userIcon.MinY(), textAreaW, TimelineItemHeader.H);
            status.Frame = new CGRect(textAreaX, header.MaxY().PlusHalfPadding(), textAreaW, status.H(textAreaW));
            images.Frame = new CGRect(textAreaX, status.MaxY().PlusHalfPadding(), textAreaW, TimelineItemImages.H(textAreaW));
            buttons.Frame = new CGRect(textAreaX, images.MaxY().PlusHalfPadding(), textAreaW, L.Icon.Height);

            // fixme
            header.BackgroundColor = UIColor.Orange;
            buttons.BackgroundColor = UIColor.Orange;
        }

        public override void PrepareForReuse()
        {
            base.PrepareForReuse();
            header.PrepareForReuse();
            status.PrepareForReuse();
            images.PrepareForReuse();
            buttons.PrepareForReuse();
        }

        public void Update(TimelineItemViewModel viewModel)
        {
            header.Update(viewModel);
            status.Update(viewModel);
            buttons.Update(viewModel);
            SetNeedsLayout();
        }
    }
}
