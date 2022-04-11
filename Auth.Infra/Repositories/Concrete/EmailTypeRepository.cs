using Auth.Domain.Entities;
using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;

namespace Auth.Infra.Repositories.Concrete
{
    public class EmailTypeRepository : BaseRepository<EmailType>, IEmailTypeRepository
    {
        public EmailTypeRepository(authdbContext context) : base(context) { }
    }
}
