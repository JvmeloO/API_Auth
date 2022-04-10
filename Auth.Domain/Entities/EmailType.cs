using System;
using System.Collections.Generic;

namespace Auth.Domain.Entities
{
    public partial class EmailType
    {
        public EmailType()
        {
            EmailTemplates = new HashSet<EmailTemplate>();
        }

        public int EmailTypeId { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<EmailTemplate> EmailTemplates { get; set; }
    }
}
