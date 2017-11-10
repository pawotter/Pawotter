using Foundation;
using Pawotter.Core.Logger;
using Security;

namespace Pawotter.iOS.Libs.KeychainService
{
    public sealed class KeychainService<KeyType> : IKeychainService<KeyType>
    {
        readonly KeychainServiceConfig config;

        public KeychainService(KeychainServiceConfig config)
        {
            this.config = config;
        }

        public bool TryGet(KeyType key, out string val)
        {
            var record = SecKeyChain.QueryAsRecord(GetQuery(config.Prefix + key), out SecStatusCode result);
            val = result.Equals(SecStatusCode.Success) ? record.ValueData.ToString() : null;
            Logger.Shared.Debug($"KeychainService.TryGet: {result} => {config.Prefix + key} : {val}");
            return result.Equals(SecStatusCode.Success);
        }

        public bool TrySet(KeyType key, string val)
        {
            if (Exists(config.Prefix + key))
            {
                var result = Update(config.Prefix + key, val);
                return result.Equals(SecStatusCode.Success);
            }
            else
            {
                var result = Insert(config.Prefix + key, val);
                return result.Equals(SecStatusCode.Success);
            }
        }

        public bool TryRemove(KeyType key)
        {
            var result = SecKeyChain.Remove(GetQuery(config.Prefix + key));
            Logger.Shared.Debug($"KeychainService.TryRemove: {result} => {key}");
            return result.Equals(SecStatusCode.Success);
        }

        SecStatusCode Insert(string key, string val)
        {
            var record = GetSecRecord(key, val);
            var result = SecKeyChain.Add(record);
            Logger.Shared.Debug($"KeychainService.Insert: {result} => {key} : {val}");
            return result;
        }

        SecStatusCode Update(string key, string val)
        {
            var attr = new SecRecord { ValueData = NSData.FromString(val) };
            var result = SecKeyChain.Update(GetQuery(key), attr);
            Logger.Shared.Debug($"KeychainService.Update: {result} => {key} : {val}");
            return result;
        }

        bool Exists(string key)
        {
            SecKeyChain.QueryAsRecord(GetQuery(key), out SecStatusCode result);
            return result.Equals(SecStatusCode.Success) && !result.Equals(SecStatusCode.ItemNotFound);
        }

        SecRecord GetQuery(string key) => new SecRecord(SecKind.GenericPassword)
        {
            Accessible = config.Accessible,
            Service = config.Service,
            Generic = NSData.FromString(key),
        };

        SecRecord GetSecRecord(string key, string value) => new SecRecord(SecKind.GenericPassword)
        {
            Accessible = config.Accessible,
            Label = key,
            Description = key,
            Account = key,
            Service = config.Service,
            Comment = key,
            Generic = NSData.FromString(key, NSStringEncoding.UTF8),
            ValueData = NSData.FromString(value, NSStringEncoding.UTF8),
        };
    }
}
