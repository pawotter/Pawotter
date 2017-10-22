using UIKit;

namespace Pawotter.iOS.Views.Components
{
    public sealed class AppNavigationController : BaseNavigationController
    {
        public static AppNavigationController OfModalStyle(UIViewController rootViewController)
        {
            var nav = new AppNavigationController(rootViewController);
            var dismissButton = new UIBarButtonItem(UIBarButtonSystemItem.Stop, (sender, e) =>
            {
                rootViewController.DismissViewController(true, null);
                rootViewController.NavigationItem.RightBarButtonItem.Dispose();
            });
            rootViewController.NavigationItem.SetRightBarButtonItem(dismissButton, false);
            return nav;
        }

        public AppNavigationController(UIViewController rootViewController) : base(rootViewController)
        {
        }
    }
}
