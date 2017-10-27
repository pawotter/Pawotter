using UIKit;

namespace Pawotter.iOS.Libs.NetworkActivityManager
{
    public class NetworkActivityManager : IActivityManager
    {
        public static NetworkActivityManager Instance = new NetworkActivityManager(UIApplication.SharedApplication);

        readonly IActivityManager manager;

        NetworkActivityManager(UIApplication application)
        {
            manager = new AnyActivityIndicatorManager(new NetworkActivityIndicator(application));
        }

        public void Attach() => manager.Attach();

        public void Detach() => manager.Detach();
    }
}
