namespace API_Auth.Business.Services.Abstract
{
    public interface IEncryptService
    {
        string EncryptPassword(string password);
    }
}
