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
    public class MailController : BaseApiController<MailLogic>
    {
        public async Task<HttpResponseMessage> Get(long mailId)
        {
            return await Logic.GetMail(mailId);
        }

        [HttpPost]
        [Route("api/order/{orderId}/mails")]
        public async Task<HttpResponseMessage> Post(long orderId)
        {
            return await Logic.SendMail(orderId);
        }

    }
}
