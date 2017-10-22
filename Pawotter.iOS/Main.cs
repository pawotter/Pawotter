using Pawotter.iOS.Views;
using SimpleInjector;
using UIKit;

namespace Pawotter.iOS
{
    public sealed class Application
    {
        public static Container Container { get; } = new Container();

        static void Main(string[] args)
        {
            ConfigureContainer();
            UIApplication.Main(args, null, "AppDelegate");
        }

        static void ConfigureContainer()
        {
            Container.RegisterSingleton<IRouter>(new Router());

#if DEBUG
            Container.Verify();
#endif
        }
    }
}
