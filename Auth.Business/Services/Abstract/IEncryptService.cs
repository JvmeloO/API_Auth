namespace Auth.Business.Services.Abstract
{
    public interface IEncryptService
    {
        string EncryptPassword(string password);

        bool VerifyPassword(string password, string passwordEncrypted);
    }
}
