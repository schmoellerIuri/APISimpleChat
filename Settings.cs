using System.Security.Cryptography;

namespace APISimples
{
    public static class Settings
    {
        private static readonly Lazy<string> lazySecret = new(() => GenerateSecret());

        public static string Secret => lazySecret.Value;

        private static string GenerateSecret()
        {
            byte[] key = new byte[64];

            var rng = RandomNumberGenerator.Create();

            rng.GetBytes(key);
            return Convert.ToBase64String(key);
        }
    }
}
