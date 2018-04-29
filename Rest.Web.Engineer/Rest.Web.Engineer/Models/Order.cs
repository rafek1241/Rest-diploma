using System.Collections.Generic;
using System.Transactions;

namespace Rest.Web.Engineer.Models
{
    public class Order
    {
        public long OrderId { get; set; }

        public string PaymentMethod { get; set; }

        public TransactionStatus Status { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual PersonalInformation PersonalInformation { get; set; }

        public virtual ICollection<Mail> Mails { get; set; }
    }
}