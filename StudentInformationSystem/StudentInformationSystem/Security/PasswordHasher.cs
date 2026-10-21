using System;
using System.Text;
using System.Security.Cryptography;

namespace StudentInformationSystem.Security
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100_000;
        private const string Prefix = "PBKDF2";

        // Returns ONE string for the password_hash column:
        // PBKDF2$<iterations>$<salt in base64>$<hash in base64>
        public static string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);

            return $"{Prefix}${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        // Checks a typed password against a stored value. Never throws.
        public static bool Verify(string password, string stored)
        {
            try
            {
                string[] parts = (stored ?? "").Split('$');
                if (parts.Length != 4 || parts[0] != Prefix) return false;

                int iterations = int.Parse(parts[1]);
                byte[] salt = Convert.FromBase64String(parts[2]);
                byte[] expected = Convert.FromBase64String(parts[3]);

                byte[] actual = Rfc2898DeriveBytes.Pbkdf2(
                    password, salt, iterations, HashAlgorithmName.SHA256, expected.Length);

                return CryptographicOperations.FixedTimeEquals(actual, expected);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}