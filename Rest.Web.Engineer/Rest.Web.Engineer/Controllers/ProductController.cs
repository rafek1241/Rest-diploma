using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Rest.Web.Engineer.Logic;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Controllers
{
    public class ProductController : BaseApiController<ProductLogic>
    {
        // GET api/<controller>
        public IEnumerable<Product> Get(int page, int perPage)
        {
            return Logic.GetProducts(page, perPage);
        }

        // GET api/<controller>/5
        public Product Get(int id)
        {
            return Logic.GetProduct(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Product value)
        {
            return Logic.SetProduct(value);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody] Product value)
        {
            return Logic.UpdateProduct(id, value);
        }

        [HttpDelete]
        [Route("api/product/{productId}/images/{imgId}")]
        public HttpResponseMessage DeleteImageFromProduct(long productId, long imgId)
        {
            return Logic.RemoveImageFromProduct(productId, imgId);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return Logic.RemoveProduct(id);
        }
    }
}