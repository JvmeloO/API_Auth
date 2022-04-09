using System;
using System.Collections.Generic;

namespace Auth.Domain.Entities
{
    public partial class EmailsSent
    {
        public int EmailSentId { get; set; }
        public int EmailTypeId { get; set; }
        public string SenderEmail { get; set; } = null!;
        public string RecipientEmail { get; set; } = null!;
        public string SubjectEmail { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool ContentIsHtml { get; set; }
        public DateTime SendDate { get; set; }
        public int? VerificationCode { get; set; }
        public bool? ValidatedCode { get; set; }

        public virtual EmailsType EmailType { get; set; } = null!;
    }
}
