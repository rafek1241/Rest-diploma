using Rest.Web.Engineer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Rest.Web.Engineer.Models.Api;

namespace Rest.Web.Engineer.Logic
{
    public class ProductLogic : LogicBase<Entities>
    {
        public IEnumerable<ProductViewModel> GetProducts(int page, int perPage)
        {
            return from x in DbContext.Products.OrderBy(p => p.ProductId).Skip(((page - 1) * perPage)).Take(perPage)
                   select new ProductViewModel()
                   {
                       ProductId = x.ProductId,
                       ProductCategories = x.ProductCategories,
                       Name = x.Name,
                       Description = x.Description,
                       Image = x.Images.FirstOrDefault()
                   };
        }

        public Product GetProduct(int id)
        {
            return DbContext.Products.Find(id);
        }

        public HttpResponseMessage SetProduct(Product value)
        {
            try
            {
                DbContext.Products.Add(value);
                DbContext.SaveChanges();

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage UpdateProduct(int id, Product value)
        {
            var product = DbContext.Products.Find(id);

            if (product == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            product = value;
            DbContext.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage RemoveProduct(int id)
        {
            var product = DbContext.Products.Find(id);

            if (product == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            DbContext.Products.Remove(product);
            DbContext.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
