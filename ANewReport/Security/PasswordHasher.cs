using System;
using System.Security.Cryptography;

namespace ANewReport.Security
{
    public static class PasswordHasher
    {
        private const int Iterations = 10000;
        private const int KeySize = 32; // 256 bit
        private const int SaltSize = 16; // 128 bit

        public static string Hash(string password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var salt = new byte[SaltSize];//Makes an array to hold the salt
                rng.GetBytes(salt);
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    var key = pbkdf2.GetBytes(KeySize);
                    return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
                }
            }
        }

        public static bool Verify(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.');
            if (parts.Length != 3) return false;
            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                var keyToCheck = pbkdf2.GetBytes(KeySize);
                return SlowEquals(keyToCheck, key);
            }
        }

        private static bool SlowEquals(byte[] keyToCheck, byte[] key)
        {
            if (keyToCheck.Length != key.Length) return false;
            int diff = 0;
            for (int i = 0; i < key.Length; i++)
            {
                diff |= keyToCheck[i] ^ key[i];////if (a[i] != b[i]) diff==1 else diff==0
            }
            return diff == 0;
        }
    }
}
