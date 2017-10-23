using CoreGraphics;
using UIKit;
using Pawotter.Core.Consts;

namespace Pawotter.iOS.Views.Components
{
    public class FollowButton : UIButton
    {
        public static CGSize Size => new CGSize(L.BoldFont.W("Following").PlusDoublePadding(), L.BoldFont.LineHeight.PlusPadding() - L.BorderW * 4);

        bool isFollowing;
        bool IsFollowing
        {
            get => isFollowing;
            set
            {
                isFollowing = value;
                SetTitle(isFollowing ? "Following" : "Follow", UIControlState.Normal);
                SetTitleColor(isFollowing ? ColorConsts.White.Color() : ColorConsts.Tint.Color(), UIControlState.Normal);
                BackgroundColor = isFollowing ? ColorConsts.Tint.Color() : ColorConsts.White.Color();
            }
        }

        public FollowButton()
        {
            TitleLabel.Font = L.BoldFont;
            Layer.BorderColor = ColorConsts.Tint.Color().CGColor;
            Layer.BorderWidth = L.BorderW * 2;
            Layer.CornerRadius = Size.Height / 2;
            ClipsToBounds = true;
            IsFollowing = true;
        }
    }
}
