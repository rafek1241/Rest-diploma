using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Logic
{
    public class CategoryLogic : LogicBase
    {

        public IEnumerable<Category> GetCategories()
        {
            return DbContext.Categories;
        }

        public Category GetCategory(long id)
        {
            return DbContext.Categories.Find(id);
        }

        public HttpResponseMessage RemoveCategory(long id)
        {
            var category = DbContext.Categories.Find(id);

            if (category == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            DbContext.Categories.Remove(category);
            DbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, DbContext.Categories.AsEnumerable());
        }

        internal IEnumerable<Category> GetCategories(string name)
        {
            return DbContext.Categories.Where(p => p.Name.Contains(name));
        }

        public HttpResponseMessage SetCategory(Category value)
        {
            try
            {
                DbContext.Categories.Add(value);
                DbContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, DbContext.Categories.AsEnumerable());
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage UpdateCategory(long id, Category value)
        {
            var category = DbContext.Categories.Find(id);

            if (category == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            category.Products.Where(categoryProduct => value.Products.All(p => p.ProductId != categoryProduct.ProductId))
                .ToList().ForEach(p => category.Products.Remove(p));

            category.Name = value.Name;
            category.Description = value.Description;

            DbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

    }
}