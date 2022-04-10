using Auth.Domain.Entities;
using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infra.Repositories.Concrete
{
    public class EmailTemplateRepository : IEmailTemplateRepository, IDisposable
    {
        private readonly authdbContext _context;

        public EmailTemplateRepository(authdbContext context)
        {
            _context = context;
        }

        public EmailTemplate GetEmailTemplateByTemplateName(string templateName) 
        {
            return _context.EmailTemplates.SingleOrDefault(t => t.TemplateName == templateName);
        }

        public EmailTemplate GetEmailTemplateByEmailTemplateId(int emailTemplateId)
        {
            return _context.EmailTemplates.Find(emailTemplateId);
        }

        public void InsertEmailTemplate(EmailTemplate emailTemplate)
        { 
            _context.EmailTemplates.Add(emailTemplate);
        }
        public void DeleteEmailTemplate(int emailTemplateId)
        {
            var emailTemplate = _context.EmailTemplates.Find(emailTemplateId);
            _context.EmailTemplates.Remove(emailTemplate);
        }
        public void UpdateEmailTemplate(EmailTemplate emailTemplate)
        {
            _context.Entry(emailTemplate).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _context.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
