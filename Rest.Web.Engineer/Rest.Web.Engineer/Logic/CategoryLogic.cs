using Rest.Web.Engineer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Rest.Web.Engineer.Logic
{
    public class CategoryLogic
    {
        private readonly Entities Db;
        public CategoryLogic(Entities db)
        {
            Db = db;
        }

        public IEnumerable<Category> GetCategories()
        {
            return Db.Categories;
        }

        public Category GetCategory(long id)
        {
            return Db.Categories.Find(id);
        }

        public HttpResponseMessage RemoveCategory(long id)
        {
            var category = Db.Categories.Find(id);

            if (category == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            Db.Categories.Remove(category);
            Db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        internal IEnumerable<Category> GetCategories(string name)
        {
            return Db.Categories.Where(p => p.Name.Contains(name));
        }

        public HttpResponseMessage SetCategory(Category value)
        {
            try
            {
                Db.Categories.Add(value);
                Db.SaveChanges();

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage UpdateCategory(long id, Category value)
        {
            var category = Db.Categories.Find(id);

            if (category == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            category = value;
            Db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
