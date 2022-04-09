using System;
using System.Collections.Generic;

namespace Auth.Domain.Entities
{
    public partial class EmailsType
    {
        public EmailsType()
        {
            EmailsSents = new HashSet<EmailsSent>();
        }

        public int EmailTypeId { get; set; }
        public string EmailTypeName { get; set; } = null!;

        public virtual ICollection<EmailsSent> EmailsSents { get; set; }
    }
}
