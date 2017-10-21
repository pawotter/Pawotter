using Foundation;
using UIKit;
using Pawotter.iOS.Views;

namespace Pawotter.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Appearance.Configure();
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            var vc = new NavigationController(new MainTabBarController());
            Window.RootViewController = vc;
            Window.MakeKeyAndVisible();
            return true;
        }
    }
}
