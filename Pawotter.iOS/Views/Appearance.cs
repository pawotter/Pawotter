using UIKit;
using Pawotter.Core.Consts;

namespace Pawotter.iOS.Views
{
    public static class Appearance
    {
        public static UIFont NormalFont => UIFont.SystemFontOfSize(L.LineSpace);

        public static void Configure()
        {
            UIView.Appearance.BackgroundColor = ColorConsts.Background.Color();
            UILabel.Appearance.Font = NormalFont;
            UILabel.Appearance.TextColor = ColorConsts.Text.Color();
            UICollectionView.Appearance.BackgroundColor = ColorConsts.ListBackgroud.Color();
            UITabBar.Appearance.BackgroundColor = ColorConsts.Background.Color();
            UITabBar.Appearance.BarTintColor = ColorConsts.Background.Color();
            UITabBar.Appearance.TintColor = ColorConsts.Tint.Color();
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = ColorConsts.Inactive.Color() }, UIControlState.Normal);
        }
    }
}
