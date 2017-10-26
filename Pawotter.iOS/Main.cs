using Pawotter.iOS.Views;
using SimpleInjector;
using UIKit;
using Pawotter.Core.Logger;

namespace Pawotter.iOS
{
    public sealed class Application
    {
        public static Container Container { get; } = new Container();
        public static IRouter Router => Container.GetInstance<IRouter>();

        static void Main(string[] args)
        {
            ConfigureContainer();
            UIApplication.Main(args, null, "AppDelegate");
        }

        static void ConfigureContainer()
        {
            Container.RegisterSingleton<IRouter>(new Router());
            Container.RegisterSingleton<ILogger>(new Logger(LogLevel.Debug));

#if DEBUG
            Container.Verify();
#endif
        }
    }
}
