using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Rest.Web.Engineer.Logic;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Controllers
{
    public class CategoryController : BaseApiController<CategoryLogic>
    {

        public IEnumerable<Category> Get()
        {
            return Logic.GetCategories();
        }

        public IEnumerable<Category> Get(string name)
        {
            return Logic.GetCategories(name);
        }

        public Category Get(int id)
        {
            return Logic.GetCategory(id);
        }

        public HttpResponseMessage Post([FromBody] Category value)
        {
            return Logic.SetCategory(value);
        }

        public HttpResponseMessage Put(int id, [FromBody] Category value)
        {
            return Logic.UpdateCategory(id, value);
        }

        public HttpResponseMessage Delete(long id)
        {
            return Logic.RemoveCategory(id);
        }
    }
}