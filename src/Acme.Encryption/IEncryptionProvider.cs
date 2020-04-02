namespace Acme.Encryption
{
    public interface IEncryptionProvider
    {
        public string Strategy { get; }

        public string Decrypt(string value, string key);

        public string Encrypt(string value, string key);

        public string Hash(string value);
    }
}