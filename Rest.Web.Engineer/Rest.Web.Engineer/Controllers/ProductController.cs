using Rest.Web.Engineer.Logic;
using Rest.Web.Engineer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rest.Web.Engineer.Models.Api;

namespace Rest.Web.Engineer.Controllers
{
    public class ProductController : BaseApiController<ProductLogic, Entities>
    {
        // GET api/<controller>
        public IEnumerable<ProductViewModel> Get(int page, int perPage) => Logic.GetProducts(page, perPage);

        // GET api/<controller>/5
        public Product Get(int id) => Logic.GetProduct(id);

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Product value) => Logic.SetProduct(value);

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Product value) => Logic.UpdateProduct(id, value);

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id) => Logic.RemoveProduct(id);
    }
}