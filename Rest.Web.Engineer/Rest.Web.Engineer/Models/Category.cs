using System.Collections.Generic;

namespace Rest.Web.Engineer.Models
{
    public class Category
    {
        public long CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}