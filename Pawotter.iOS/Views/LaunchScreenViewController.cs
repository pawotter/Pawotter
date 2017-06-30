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
            navigation = createNavigation();
            PresentViewController(navigation, animated, null);
        }

        static UINavigationController createNavigation()
        {
            var vc = new TimelineViewController();
            vc.View.BackgroundColor = UIColor.White;
            var nav = new UINavigationController(vc);
            nav.NavigationBar.Translucent = false;
            nav.NavigationBar.BarTintColor = UIColor.Cyan;
            return nav;
        }
    }
}
