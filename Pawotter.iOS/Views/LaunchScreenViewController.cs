using UIKit;

namespace Pawotter.iOS
{
    public class LaunchScreenViewController : UIViewController
    {
        UINavigationController navigation;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            EdgesForExtendedLayout = UIRectEdge.None;
            View.BackgroundColor = UIColor.White;
        }

        public override void ViewDidAppear(bool animated)
        {
            navigation = CreateNavigation();
            PresentViewController(navigation, animated, null);
        }

        static UINavigationController CreateNavigation()
        {
            var vc = new HomeSwipePageController();
            var nav = new UINavigationController(vc);
            nav.NavigationBar.Translucent = false;
            nav.NavigationBar.BarTintColor = UIColor.Cyan;
            return nav;
        }
    }
}
