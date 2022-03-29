namespace API_Auth.Services.Abstract
{
    public interface IEncryptService
    {
        string EncryptPassword(string password);
    }
}
