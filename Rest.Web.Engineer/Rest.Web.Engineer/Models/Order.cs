using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Web.Engineer.Models
{
    public class Order
    {
        public virtual Cart Cart { get; set; }
        public long OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string AddressTwo { get; set; }
        public string PostalCode { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

    }
}
