using Foundation;
using UIKit;
using Pawotter.iOS.Views;
using Pawotter.iOS.Views.Components;
using HockeyApp.iOS;
using Pawotter.Core.Logger;
using Pawotter.iOS.Libs.KeychainService;
using Pawotter.iOS.Services;

namespace Pawotter.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Appearance.Configure();

            Logger.Shared.LogLevel = LogLevel.Debug;
            IKeychainService<KeychainKey> keychain = new KeychainService<KeychainKey>(new KeychainServiceConfig("PWT", "Pawotter", Security.SecAccessible.WhenUnlockedThisDeviceOnly));
            keychain.TryGet(KeychainKey.Token, out var token);
            keychain.TrySet(KeychainKey.Token, "hosffasdfjpasdfjoiajsdiofjge");
            keychain.TryGet(KeychainKey.Token, out var token2);
            keychain.TryRemove(KeychainKey.Token);
            keychain.TryGet(KeychainKey.Token, out var token3);

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
