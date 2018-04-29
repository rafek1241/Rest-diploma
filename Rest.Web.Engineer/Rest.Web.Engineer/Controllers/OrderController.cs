using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Rest.Web.Engineer.Logic;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Controllers
{
    public class OrderController : BaseApiController<OrderLogic>
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Order order) => Logic.CreateOrder(order);
    }
}
