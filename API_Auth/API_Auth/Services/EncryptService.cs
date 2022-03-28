using System.Text;

namespace API_Auth.Services
{
    public static class EncryptService
    {
        public static string EncryptPassword(string password) 
        {
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
    }
}
