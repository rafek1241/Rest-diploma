using System.Net.Http;
using System.Web.Http;
using Rest.Web.Engineer.Logic;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Controllers
{
    [RoutePrefix("api/cart")]
    public class CartController : BaseApiController<CartLogic>
    {
        [Route("{token}")]
        public HttpResponseMessage Get(string token)
        {
            return Logic.Get(token);
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody] Cart cart)
        {
            return Logic.Put(cart);
        }

        [Route("{token}")]
        [HttpDelete]
        public HttpResponseMessage Delete(string token)
        {
            return Logic.Delete(token);
        }
    }
}