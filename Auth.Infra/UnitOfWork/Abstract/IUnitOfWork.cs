namespace Auth.Infra.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}
