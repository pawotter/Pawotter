using Pawotter.iOS.Views;
using SimpleInjector;
using UIKit;
using Pawotter.iOS.Libs.KeychainService;
using Pawotter.iOS.Services;
using Pawotter.Core.Logger;

namespace Pawotter.iOS
{
    public sealed class Application
    {
        public static Container Container { get; } = new Container();
        public static IRouter Router => Container.GetInstance<IRouter>();
        public static IKeychainService<KeychainKey> Keychain => Container.GetInstance<IKeychainService<KeychainKey>>();

        static void Main(string[] args)
        {
            ConfigureContainer();
            UIApplication.Main(args, null, "AppDelegate");
        }

        static void ConfigureContainer()
        {
            Container.RegisterSingleton<ILogger>(Logger.Shared);
            Container.RegisterSingleton<IRouter>(new Router());
            Container.RegisterSingleton<IKeychainService<KeychainKey>>(new KeychainService<KeychainKey>(new KeychainServiceConfig("PWT", "pawotter", Security.SecAccessible.WhenUnlockedThisDeviceOnly)));

#if DEBUG
            Container.Verify();
#endif
        }
    }
}
