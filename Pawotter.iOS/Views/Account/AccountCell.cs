using Pawotter.iOS.Views.Components;
using CoreGraphics;
using UIKit;
using System;
using Pawotter.Core.Consts;

namespace Pawotter.iOS.Views.Account
{
    public class AccountCell : BaseCollectionViewCell
    {
        public static nfloat H => L.SmallUserIcon.Height.PlusDoublePadding();

        readonly UserIcon userIcon = new UserIcon();
        readonly UILabel displayName = new UILabel { TextColor = ColorConsts.Text.Color(), Font = L.BoldFont, Text = "DisplayName" };
        readonly UILabel acct = new UILabel { TextColor = ColorConsts.Inactive.Color(), Font = L.NormalFont, Text = "userid@instance" };
        readonly FollowButton followButton = new FollowButton();

        public AccountCell() { CommonInit(); }
        public AccountCell(IntPtr handle) : base(handle) { CommonInit(); }

        void CommonInit()
        {
            userIcon.BackgroundColor = UIColor.Orange;
            AddSubviews(userIcon, displayName, acct, followButton);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            var textW = this.Width() - 100;
            userIcon.Frame = new CGRect(L.PaddingL, L.PaddingL, L.SmallUserIcon.Width, L.SmallUserIcon.Height);
            followButton.Frame = new CGRect(this.Width().MinusPadding() - FollowButton.Size.Width, userIcon.MinY(), FollowButton.Size.Width, FollowButton.Size.Height);
            displayName.Frame = new CGRect(userIcon.MaxX().PlusPadding(), userIcon.MinY(), textW, L.NormalFont.LineHeight);
            acct.Frame = new CGRect(displayName.X(), displayName.MaxY(), textW, L.NormalFont.LineHeight);
        }
    }
}
