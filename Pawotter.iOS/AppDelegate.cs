using Foundation;
using UIKit;
using Pawotter.iOS.Views;
using Pawotter.iOS.Views.Components;
using HockeyApp.iOS;
using Pawotter.Core.Logger;
using System;
using Pawotter.API;
using System.Text.RegularExpressions;

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

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            if (Uri.TryCreate(url.AbsoluteString, UriKind.Absolute, out var uri) && uri.Scheme.Equals("pawotter") && uri.Host.Equals("app"))
            {
                switch (uri.AbsolutePath)
                {
                    case string authorize when Regex.IsMatch(authorize, @"^/authorize?.+"):
                        var refreshToken = MastodonOAuthClient.GetRefreshTokenFromRedirectUri(new Uri(url.AbsoluteString));
                        Logger.Shared.Debug($"Application.OpenUrl: AUTHORIZE (code = {refreshToken})");
                        NSNotificationCenter.DefaultCenter.PostNotificationName(WebViewController.CloseSafariViewControllerNotification, url);
                        break;
                    case string home when Regex.IsMatch(home, @"^/home"):
                        Logger.Shared.Debug($"Application.OpenUrl: HOME");
                        break;
                    default:
                        Logger.Shared.Debug($"Application.OpenUrl: {uri}");
                        break;
                }
                return true;
            }
            Logger.Shared.Debug($"Application.OpenUrl: INVALID (uri = {uri})");
            return false;
        }
    }
}
