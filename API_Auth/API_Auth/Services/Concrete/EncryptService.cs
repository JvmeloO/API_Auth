using API_Auth.Services.Abstract;
using System.Text;

namespace API_Auth.Services.Concrete
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
