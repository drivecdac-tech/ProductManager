using System;
using System.Security.Cryptography;
using System.Text;

namespace ProductManager.Security
{
    public static class PasswordHasher
    {
        private const int Iterations = 10000;
        private const int SaltSize = 16;   // 128-bit
        private const int KeySize = 32;    // 256-bit

        public static string Hash(string password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var salt = new byte[SaltSize];
                rng.GetBytes(salt);

                using (var derive = new Rfc2898DeriveBytes(
                    password,
                    salt,
                    Iterations,
                    HashAlgorithmName.SHA256))
                {
                    var key = derive.GetBytes(KeySize);

                    return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
                }
            }
        }
        public static bool Verify(string password, string hash)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3) return false;

            var iterations = int.Parse(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var storedKey = Convert.FromBase64String(parts[2]);

            using (var derive = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256))
            {
                var key = derive.GetBytes(storedKey.Length);
                return SlowEquals(key, storedKey);
            }
        }
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];

            return diff == 0;
        }
    }

}