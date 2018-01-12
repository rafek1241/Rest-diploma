using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Web.Engineer.Models
{
    public class Product
    {
        public long ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<CartProduct> CartProducts { get; set; }
        public virtual ICollection<Category> ProductCategories { get; set; }

        public virtual ICollection<File> Images { get; set; }
        public decimal Price { get; set; }
    }
}