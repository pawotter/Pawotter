using UIKit;
using Pawotter.Core.Consts;

namespace Pawotter.iOS.Views
{
    public static class Appearance
    {
        public static void Configure()
        {
            UIWindow.Appearance.BackgroundColor = ColorConsts.Tint.Color();
            UILabel.Appearance.BackgroundColor = UIColor.Clear;
            UILabel.Appearance.Font = L.NormalFont;
            UILabel.Appearance.TextColor = ColorConsts.Text.Color();
            UIImageView.Appearance.BackgroundColor = ColorConsts.Background.Color();
            UICollectionView.Appearance.BackgroundColor = ColorConsts.ListBackgroud.Color();
            UITableView.Appearance.BackgroundColor = ColorConsts.ListBackgroud.Color();
            UITableView.Appearance.SectionIndexBackgroundColor = ColorConsts.ListBackgroud.Color();
            UITableView.Appearance.SeparatorColor = ColorConsts.ListBackgroud.Color();
            UITabBar.Appearance.BackgroundColor = ColorConsts.Background.Color();
            UITabBar.Appearance.BarTintColor = ColorConsts.Background.Color();
            UITabBar.Appearance.TintColor = ColorConsts.Tint.Color();
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = ColorConsts.Inactive.Color() }, UIControlState.Normal);
            UISegmentedControl.Appearance.TintColor = ColorConsts.Tint.Color();
            UISegmentedControl.Appearance.BackgroundColor = ColorConsts.Background.Color();
            UINavigationBar.Appearance.BarTintColor = ColorConsts.Background.Color();
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes { Font = L.ExtraLargeBoldFont };
            UIBarButtonItem.Appearance.TintColor = ColorConsts.Tint.Color();
            UIBarButtonItem.Appearance.SetBackButtonTitlePositionAdjustment(new UIOffset(-60, -60), UIBarMetrics.Default);
            UICollectionViewCell.Appearance.BackgroundColor = ColorConsts.Background.Color();
            UITableViewCell.Appearance.BackgroundColor = ColorConsts.Background.Color();
        }
    }
}
