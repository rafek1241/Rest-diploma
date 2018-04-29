using System.Net.Http;
using System.Threading.Tasks;
using Rest.Web.Engineer.Logic;

namespace Rest.Web.Engineer.Controllers
{
    public class CountryController : BaseApiController<CountryLogic>
    {
        public async Task<HttpResponseMessage> Get(string name)
        {
            return await Logic.GetCountry(name);
        }
    }
}
