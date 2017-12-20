using Rest.Web.Engineer.Logic;
using Rest.Web.Engineer.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Rest.Web.Engineer.Controllers
{
    public class CategoryController : ApiController /*BaseApiController<CategoryLogic, Entities>*/
    {
        private readonly CategoryLogic Logic;

        public CategoryController()
        {
            Logic = new CategoryLogic(new Entities());
        }

        public CategoryController(CategoryLogic logic)
        {
            Logic = logic;
        }

        [HttpGet]
        public IEnumerable<Category> Get() => Logic.GetCategories();

        [HttpGet]
        public IEnumerable<Category> Get(string name) => Logic.GetCategories(name);

        [HttpGet]
        public Category Get(int id) => Logic.GetCategory(id);

        [HttpPost]
        public HttpResponseMessage Post([FromBody]Category value) => Logic.SetCategory(value);

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]Category value) => Logic.UpdateCategory(id, value);

        [HttpDelete]
        public HttpResponseMessage Delete(long id) => Logic.RemoveCategory(id);
    }
}