using Auth.Business.Services.Abstract;
using System.Text;

namespace Auth.Business.Services.Concrete
{
    public class EncryptService : IEncryptService
    {
        public string EncryptPassword(string password) 
        {
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
    }
}
