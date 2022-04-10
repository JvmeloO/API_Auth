using System;
using System.Collections.Generic;

namespace Auth.Domain.Entities
{
    public partial class EmailTemplate
    {
        public EmailTemplate()
        {
            EmailSents = new HashSet<EmailSent>();
        }

        public int EmailTemplateId { get; set; }
        public string TemplateName { get; set; } = null!;
        public string EmailSubject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool ContentIsHtml { get; set; }
        public int EmailTypeId { get; set; }

        public virtual EmailType EmailType { get; set; } = null!;
        public virtual ICollection<EmailSent> EmailSents { get; set; }
    }
}
