using Application.Ports;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class CryptographyService : ICryptographyService
    {
        private const int _hashSize = 64;
        public string GenerateSalt()
        {
            var buffer = new byte[_hashSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public string HashPassword(string password, string salt)
        {
            var argon2 = new Argon2i(Encoding.UTF8.GetBytes(password))
            {
                DegreeOfParallelism = 16,
                MemorySize = 8192,
                Iterations = 40,
                Salt = Encoding.UTF8.GetBytes(salt)
            };

            var hash = argon2.GetBytes(_hashSize);
            return Convert.ToBase64String(hash);
        }
    }
}
