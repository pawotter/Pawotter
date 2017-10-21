using UIKit;

namespace Pawotter.iOS.Views
{
    public class MainTabBarController : UITabBarController
    {
        readonly UIViewController home = new UIViewController { TabBarItem = new UITabBarItem("Home", R.Home, 0) };
        readonly UIViewController local = new UIViewController { TabBarItem = new UITabBarItem("Local", R.Local, 1) };
        readonly UIViewController federated = new UIViewController { TabBarItem = new UITabBarItem("Federated", R.Federated, 2) };
        readonly UIViewController notifications = new UIViewController { TabBarItem = new UITabBarItem("Notifications", R.Notifications, 3) };
        readonly UIViewController others = new UIViewController { TabBarItem = new UITabBarItem("Others", R.Others, 4) };

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewControllers = new[] { home, local, federated, notifications, others };
        }
    }
}
