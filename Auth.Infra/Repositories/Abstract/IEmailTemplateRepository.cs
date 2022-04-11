using Auth.Domain.Entities;

namespace Auth.Infra.Repositories.Abstract
{
    public interface IEmailTemplateRepository : IBaseRepository<EmailTemplate> 
    {
        EmailTemplate GetByTemplateName(string templateName);
    }
}
