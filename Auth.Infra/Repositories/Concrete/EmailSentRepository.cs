using Auth.Domain.Entities;
using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;

namespace Auth.Infra.Repositories.Concrete
{
    public class EmailSentRepository : IEmailSentRepository, IDisposable
    {
        private readonly authdbContext _context;

        public EmailSentRepository(authdbContext context)
        {
            _context = context;
        }

        public EmailSent GetLastEmailSentByRecipientEmailAndTemplateName(string recipientEmail, string templateName) 
        {
            return _context.EmailSents.LastOrDefault(e => e.RecipientEmail == recipientEmail 
            && e.EmailTemplateId == _context.EmailTemplates.SingleOrDefault(e => e.TemplateName == templateName).EmailTemplateId);
        }

        public void InsertEmailSent(EmailSent emailSent)
        {
            _context.EmailSents.Add(emailSent);
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
