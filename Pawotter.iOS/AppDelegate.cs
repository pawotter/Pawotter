using Foundation;
using UIKit;

namespace Pawotter.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override void FinishedLaunching(UIApplication application)
        {
            Window = new UIWindow();
            Window.RootViewController = new ViewController();
            Window.MakeKeyAndVisible();
        }
    }
}
