using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Transactions;
using Rest.Web.Engineer.Models;
using WebGrease.Css.Extensions;

namespace Rest.Web.Engineer.Logic
{
    public class ProductLogic : LogicBase
    {

        public IEnumerable<Product> GetProducts(int page, int perPage)
        {
            return DbContext.Products.OrderBy(p => p.ProductId).Skip((page - 1) * perPage).Take(perPage);
        }

        public Product GetProduct(int id)
        {
            return DbContext.Products.Find(id);
        }

        public HttpResponseMessage SetProduct(Product value)
        {
            try
            {

                var tags = value.ProductCategories;
                value.ProductCategories = new List<Category>();
                tags.ForEach(category => value.ProductCategories.Add(DbContext.Categories.Find(category.CategoryId)));

                foreach (var image in value.Images)
                {
                    image.Content = Convert.FromBase64String(image.ContentBase64);
                }

                DbContext.Products.Add(value);
                DbContext.SaveChanges();

                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage UpdateProduct(int id, Product value)
        {
            try
            {
                var product = DbContext.Products.Find(id);

                if (product == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                var productImages = product.Images.ToList();

                var destinationCategories = DbContext.Categories.AsEnumerable().Where(p =>
                    value.ProductCategories.Any(w => w.CategoryId == p.CategoryId)).ToList();


                foreach (var valueImage in value.Images.Where(p => !string.IsNullOrEmpty(p.ContentBase64)))
                {

                    valueImage.Content = Convert.FromBase64String(valueImage.ContentBase64);

                    if (product.Images.Any(p => p.FileId == valueImage.FileId)) continue;

                    DbContext.Files.Add(valueImage);
                    productImages.Add(valueImage);
                }

                product.Images = productImages;
                var resultCategories =
                    destinationCategories.Where(p => p.Products.All(w => w.ProductId != product.ProductId));

                foreach (var category in resultCategories)
                {
                    product.ProductCategories.Add(category);
                }

                product.Name = value.Name;
                product.Description = value.Description;
                product.Price = value.Price;


                DbContext.SaveChanges();
            }
            catch (Exception er)
            {
                throw new Exception("Transakcja aktualizacji produktu nie dobiegła do końca.", er);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage RemoveProduct(int id)
        {
            var product = DbContext.Products.Find(id);

            if (product == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            DbContext.Files.RemoveRange(product.Images);
            DbContext.Products.Remove(product);
            DbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, DbContext.Products.AsEnumerable());
        }

        public HttpResponseMessage RemoveImageFromProduct(long productId, long imgId)
        {
            var product = DbContext.Products.Find(productId);

            if (product == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Nie znaleziono produktu!");

            DbContext.Files.Remove(product.Images.First(w => w.FileId == imgId));
            DbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, product);
        }
    }
}