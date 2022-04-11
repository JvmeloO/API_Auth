using Auth.Domain.Entities;
using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infra.Repositories.Concrete
{
    public class EmailTemplateRepository : BaseRepository<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(authdbContext context) : base(context) { }
    }
}
