namespace Auth.Business.Services.Abstract
{
    public interface IKeyCodeService
    {
        string GenerateKeyCode(int size);
    }
}
