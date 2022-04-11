using Auth.Domain.Entities;
using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infra.Repositories.Concrete
{
    public class EmailTemplateRepository : BaseRepository<EmailTemplate>, IEmailTemplateRepository
    {
        private readonly DbSet<EmailTemplate> _dbSet;

        public EmailTemplateRepository(authdbContext context) : base(context) 
        {
            _dbSet = context.Set<EmailTemplate>();
        }

        public EmailTemplate GetByTemplateName(string templateName) 
        {
            return _dbSet.SingleOrDefault(x => x.TemplateName == templateName);
        }
    }
}
