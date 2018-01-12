using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Web.Engineer.Models
{
    public class CartProduct
    {
        public long CartProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }
        public int Count { get; set; }
    }
}
