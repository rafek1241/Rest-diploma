using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Web.Engineer.Models
{
    public class Cart
    {
        public long CartId { get; set; }

        public Guid CookieToken { get; set; }

        public DateTime? ExpireDate { get; set; }

        public virtual ICollection<CartProduct> Products { get; set; }
    }
}