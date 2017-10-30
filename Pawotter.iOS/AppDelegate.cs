using Foundation;
using UIKit;
using Pawotter.iOS.Views;
using Pawotter.iOS.Views.Components;
using HockeyApp.iOS;

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
            var vc = new AppNavigationController(new MainTabBarController());
            Window.RootViewController = vc;
            Window.MakeKeyAndVisible();
            return true;
        }

        void ConfigureHockyApp()
        {
            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure(BuildSecrets.HockeyAppId);
            manager.CrashManager.CrashManagerStatus = BITCrashManagerStatus.AutoSend;
            manager.StartManager();
            manager.Authenticator.AuthenticateInstallation();
        }
    }
}
