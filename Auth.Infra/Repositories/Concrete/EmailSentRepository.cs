using Auth.Domain.Entities;
using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;

namespace Auth.Infra.Repositories.Concrete
{
    public class EmailSentRepository : BaseRepository<EmailSent>, IEmailSentRepository
    {
        public EmailSentRepository(authdbContext context) : base(context) { }
    }
}
