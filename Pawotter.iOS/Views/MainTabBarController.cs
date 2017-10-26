using UIKit;
using Pawotter.ViewModels;
using Pawotter.iOS.Views.Timeline;
using Pawotter.iOS.Views.Notification;
using Pawotter.iOS.Views.Others;
using Foundation;

namespace Pawotter.iOS.Views
{
    public sealed class MainTabBarController : UITabBarController, IUITabBarControllerDelegate
    {
        readonly UIViewController home = new TimelineViewController(new HomeTimelineViewModel(), TimelineViewController.DisplayMode.MainTab) { TabBarItem = new UITabBarItem("Home", R.Home, 0) };
        readonly UIViewController local = new TimelineViewController(new LocalTimelineViewModel(), TimelineViewController.DisplayMode.MainTab) { TabBarItem = new UITabBarItem("Local", R.Local, 1) };
        readonly UIViewController federated = new TimelineViewController(new FederatedTimelineViewModel(), TimelineViewController.DisplayMode.MainTab) { TabBarItem = new UITabBarItem("Federated", R.Federated, 2) };
        readonly UIViewController notifications = new NotificationViewController(NotificationViewController.DisplayMode.MainTab) { TabBarItem = new UITabBarItem("Notifications", R.Notifications, 3) };
        readonly UIViewController others = new OthersViewController { TabBarItem = new UITabBarItem("Others", R.Others, 4) };

        UIViewController previousVC;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewControllers = new[] { home, local, federated, notifications, others };
            previousVC = home;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            WeakDelegate = this;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            WeakDelegate = null;
        }

        [Export("tabBarController:didSelectViewController:")]
        public new void ViewControllerSelected(UITabBarController tabBarController, UIViewController viewController)
        {
            if (viewController is IScrollableViewController vc && vc == previousVC)
            {
                vc.ScrollsToTop();
            }
            previousVC = viewController;
        }
    }
}