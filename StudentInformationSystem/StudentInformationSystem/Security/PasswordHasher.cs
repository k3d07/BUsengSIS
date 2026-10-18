using System;
using System.Security.Cryptography;
using System.Text;

namespace BUsengSIS.Security
{
    public static class PasswordHasher
    {
        public static string GenerateSalt()
        {
            byte[] salt = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] combined = new byte[saltBytes.Length + passwordBytes.Length];

            saltBytes.CopyTo(combined, 0);
            passwordBytes.CopyTo(combined, saltBytes.Length);

            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(combined);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
