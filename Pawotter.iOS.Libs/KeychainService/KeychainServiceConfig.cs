using Security;

namespace Pawotter.iOS.Libs.KeychainService
{
    public sealed class KeychainServiceConfig
    {
        public string Prefix { get; }
        public string Service { get; }
        public SecAccessible Accessible { get; }

        public KeychainServiceConfig(string prefix, string service, SecAccessible accesible)
        {
            Prefix = prefix;
            Service = service;
            Accessible = accesible;
        }
    }
}
