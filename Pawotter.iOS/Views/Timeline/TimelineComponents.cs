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
            rebloggingButton.SetImage(R.Reblog, UIControlState.Normal);
            favouritingButton.SetImage(R.Favourite, UIControlState.Normal);
            AddSubviews(replyButton, rebloggingButton, favouritingButton);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            var spaceX = (this.Width() - L.Icon.Width * 4) / 4;
            replyButton.Frame = new CGRect(0, 0, L.Icon.Width, L.Icon.Height);
            rebloggingButton.Frame = new CGRect(replyButton.MaxX() + spaceX, 0, L.Icon.Width, L.Icon.Height);
            favouritingButton.Frame = new CGRect(rebloggingButton.MaxX() + spaceX, 0, L.Icon.Width, L.Icon.Height);
        }
    }
}
