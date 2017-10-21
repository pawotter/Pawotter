using UIKit;
using Pawotter.ViewModels;
using Pawotter.iOS.Views.Timeline;
using Pawotter.iOS.Views.Notification;

namespace Pawotter.iOS.Views
{
    public sealed class MainTabBarController : UITabBarController
    {
        readonly UIViewController home = new TimelineViewController(new HomeTimelineViewModel()) { TabBarItem = new UITabBarItem("Home", R.Home, 0) };
        readonly UIViewController local = new TimelineViewController(new LocalTimelineViewModel()) { TabBarItem = new UITabBarItem("Local", R.Local, 1) };
        readonly UIViewController federated = new TimelineViewController(new FederatedTimelineViewModel()) { TabBarItem = new UITabBarItem("Federated", R.Federated, 2) };
        readonly UIViewController notifications = new NotificationViewController { TabBarItem = new UITabBarItem("Notifications", R.Notifications, 3) };
        readonly UIViewController others = new UIViewController { TabBarItem = new UITabBarItem("Others", R.Others, 4) };

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewControllers = new[] { home, local, federated, notifications, others };
        }
    }
}
