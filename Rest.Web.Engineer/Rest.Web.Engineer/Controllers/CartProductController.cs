using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Rest.Web.Engineer.Logic;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Controllers
{
    [RoutePrefix("api/cart/{token}/product")]
    public class CartProductController : BaseApiController<CartProductLogic>
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Product product, string token) => Logic.AddProduct(product, token);

        [HttpPatch]
        public HttpResponseMessage Patch([FromBody]JObject requestBody, string token)=> Logic.ChangeProductCount(requestBody, token);

        [Route("{productId}")]
        [HttpDelete]
        public HttpResponseMessage Delete(string token, long productId) => Logic.RemoveProduct(productId, token);
    }
}
