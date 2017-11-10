using SafariServices;

namespace Pawotter.iOS.Views
{
    public class WebViewController : SFSafariViewController
    {
        public WebViewController(Foundation.NSUrl url) : base(url) { }
    }
}
