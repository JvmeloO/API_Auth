using Auth.Business.Services.Abstract;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Business.Services.Concrete
{
    public class KeyCodeService : IKeyCodeService
    {
        private readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        public string GenerateKeyCode(int size) 
        {
            var data = new byte[4 * size];

            using (var crypto = RandomNumberGenerator.Create())
                crypto.GetBytes(data);

            var result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
    }
}
