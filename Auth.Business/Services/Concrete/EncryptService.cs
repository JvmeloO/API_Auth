using Auth.Business.Services.Abstract;
using System.Security.Cryptography;

namespace Auth.Business.Services.Concrete
{
    public class EncryptService : IEncryptService
    {
        private const int saltSize = 16;
        private const int hashSize = 20;
        private const int iterations = 100000;

        public string EncryptPassword(string password)
        {
            byte[] salt;
            salt = RandomNumberGenerator.GetBytes(saltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(hashSize);

            byte[] hashBytes = new byte[saltSize + hashSize];
            Array.Copy(salt, 0, hashBytes, 0, saltSize);
            Array.Copy(hash, 0, hashBytes, saltSize, hashSize);

            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string password, string passwordEncrypted)
        {
            byte[] hashBytes = Convert.FromBase64String(passwordEncrypted);

            byte[] salt = new byte[saltSize];
            Array.Copy(hashBytes, 0, salt, 0, saltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(hashSize);

            for (int i = 0; i < hashSize; i++)
                if (hashBytes[i + saltSize] != hash[i])
                    return false;

            return true;
        }
    }
}
