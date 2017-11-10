namespace Pawotter.iOS.Libs.KeychainService
{
    /// <summary>
    /// abstraction of keychain operations
    /// </summary>
    public interface IKeychainService<KeyType>
    {
        bool TryGet(KeyType key, out string val);

        bool TrySet(KeyType key, string val);

        bool TryRemove(KeyType key);
    }
}
