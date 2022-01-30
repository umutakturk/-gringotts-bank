using System;
using System.Linq;
using System.Security.Cryptography;
using GringottsBank.Infrastructure.Identity.Abstractions;

namespace GringottsBank.Infrastructure.Identity
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 1000;

        public string Hash(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA512);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);
            return $"{key}.{salt}";
        }

        public bool Verify(string hash, string password)
        {
            var parts = hash.Split('.', 2);
            if (parts.Length != 2)
            {
                throw new FormatException("Hash should be formatted as `{hash}.{salt}`");
            }

            var key = Convert.FromBase64String(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);

            using var algorithm = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA512);
            var keyToCheck = algorithm.GetBytes(KeySize);
            var verified = keyToCheck.SequenceEqual(key);

            return verified;
        }
    }
}
