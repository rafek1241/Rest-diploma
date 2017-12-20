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

        public ICollection<Category> ProductCategories { get; set; }

        public ICollection<File> Images { get; set; }
    }
}
