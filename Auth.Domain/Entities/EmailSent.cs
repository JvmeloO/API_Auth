using System;
using System.Collections.Generic;

namespace Auth.Domain.Entities
{
    public partial class EmailSent
    {
        public int EmailSentId { get; set; }
        public string SenderEmail { get; set; } = null!;
        public string RecipientEmail { get; set; } = null!;
        public DateTime SendDate { get; set; }
        public string? VerificationCode { get; set; }
        public bool? ValidatedCode { get; set; }
        public int EmailTemplateId { get; set; }

        public virtual EmailTemplate EmailTemplate { get; set; } = null!;
    }
}
