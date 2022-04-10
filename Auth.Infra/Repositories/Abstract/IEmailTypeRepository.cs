using Auth.Domain.Entities;

namespace Auth.Infra.Repositories.Abstract
{
    public interface IEmailTypeRepository
    {
        int GetEmailTypeIdByTypeName(string typeName);
    }
}
