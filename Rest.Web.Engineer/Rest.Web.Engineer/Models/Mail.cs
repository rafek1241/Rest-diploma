using System.Net.Mail;

namespace Rest.Web.Engineer.Models
{
    public class Mail
    {
        public long MailId { get; set; }
        public SmtpStatusCode Status { get; set; }
        public virtual Order Order { get; set; }
    }
}
