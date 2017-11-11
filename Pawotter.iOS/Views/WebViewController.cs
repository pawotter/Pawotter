using Foundation;
using SafariServices;

namespace Pawotter.iOS.Views
{
    public class WebViewController : SFSafariViewController
    {
        public static NSString CloseSafariViewControllerNotification => new NSString("kCloseSafariViewControllerNotification");
        NSObject CloseSafariViewControllerNotificationObserver;

        public WebViewController(NSUrl url) : base(url) { }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            CloseSafariViewControllerNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(CloseSafariViewControllerNotification, _ => DismissViewController(true, null), null);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            if (CloseSafariViewControllerNotificationObserver != null) NSNotificationCenter.DefaultCenter.RemoveObserver(CloseSafariViewControllerNotification);
        }
    }
}
