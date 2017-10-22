using UIKit;
using Pawotter.ViewModels;
using CoreGraphics;
using System;
using Pawotter.Core.Consts;

namespace Pawotter.iOS.Views.Timeline
{
    /// <summary>
    /// timeline header: displayname, acct, posted_at
    /// </summary>
    public sealed class TimelineItemHeader : BaseView
    {
        public static nfloat H => L.LineSpace;

        readonly UILabel headline = new UILabel();

        public TimelineItemHeader()
        {
            headline.Font = L.LargeBoldFont;
            headline.TextColor = ColorConsts.Text.Color();
            AddSubviews(headline);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            headline.Frame = new CGRect(0, 0, this.Width(), L.LineSpace);
        }

        public void Update(TimelineItemViewModel viewModel)
        {
            headline.Text = viewModel.DisplayName;
            headline.SizeToFit();
        }
    }

    /// <summary>
    /// timeline status: text
    /// </summary>
    public sealed class TimelineItemStatus : BaseView
    {
        public static nfloat H(string text, nfloat width) => L.NormalFont.H(text, width);
        public nfloat H(nfloat width) => H(status.Text, width);

        readonly UILabel status = new UILabel();

        public TimelineItemStatus()
        {
            status.Lines = 0;
            status.LineBreakMode = UILineBreakMode.WordWrap;
            status.Font = L.NormalFont;
            status.TextColor = ColorConsts.Text.Color();
            AddSubviews(status);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            status.Frame = new CGRect(0, 0, this.Width(), this.Height());
        }

        public void Update(TimelineItemViewModel viewModel)
        {
            status.Text = viewModel.Status;
        }
    }

    /// <summary>
    /// timeine images: attachments
    /// </summary>
    public sealed class TimelineItemImages : BaseView
    {
        public static nfloat H(nfloat width) => width / L.ImageAspectRatio;

        public TimelineItemImages()
        {
        }
    }

    /// <summary>
    /// timeline header: reply, reblog, favourite buttons
    /// </summary>
    public sealed class TimelineItemButtons : BaseView
    {
        public static nfloat H => L.Icon.Height;

        readonly UIButton replyButton = new UIButton(UIButtonType.Custom) { TintColor = ColorConsts.Inactive.Color() };
        readonly UIButton rebloggingButton = new UIButton(UIButtonType.Custom) { TintColor = ColorConsts.Inactive.Color() };
        readonly UIButton favouritingButton = new UIButton(UIButtonType.Custom) { TintColor = ColorConsts.Inactive.Color() };

        public TimelineItemButtons()
        {
            replyButton.SetImage(R.Reply, UIControlState.Normal);
            replyButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            rebloggingButton.SetImage(R.Reblog, UIControlState.Normal);
            rebloggingButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            rebloggingButton.TitleLabel.Font = L.NormalFont;
            favouritingButton.SetImage(R.Favourite, UIControlState.Normal);
            favouritingButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            favouritingButton.TitleLabel.Font = L.NormalFont;
            AddSubviews(replyButton, rebloggingButton, favouritingButton);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            var spaceX = (this.Width() - L.Icon.Width * 4) / 4;
            replyButton.Frame = new CGRect(0, 0, L.Icon.Width, L.Icon.Height);
            rebloggingButton.Frame = new CGRect(L.Icon.Width + spaceX, 0, L.Icon.Width + L.PaddingL, L.Icon.Height);
            favouritingButton.Frame = new CGRect((L.Icon.Width + spaceX) * 2, 0, L.Icon.Width + L.PaddingL, L.Icon.Height);
        }

        public void Update(TimelineItemViewModel viewModel)
        {
            rebloggingButton.SetTitle("0", UIControlState.Normal);
            favouritingButton.SetTitle("0", UIControlState.Normal);
        }
    }
}
