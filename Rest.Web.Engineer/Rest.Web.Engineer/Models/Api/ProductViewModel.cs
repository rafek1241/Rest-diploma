using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Web.Engineer.Models.Api
{
    public class ProductViewModel
    {
        public long ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Category> ProductCategories { get; set; }

        public File Image { get; set; }
    }
}