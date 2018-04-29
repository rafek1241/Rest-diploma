using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rest.Web.Engineer.Logic
{
    public class CountryLogic : LogicBase
    {
        private Uri BaseUrl { get; }
        private HttpClient Client { get; }

        public CountryLogic()
        {
            BaseUrl = new Uri("https://restcountries.eu/");
            var clientHandler = new HttpClientHandler()
            {
                UseDefaultCredentials = true
            };

            Client = new HttpClient(clientHandler)
            {
                BaseAddress = BaseUrl
            };
        }

        public async Task<HttpResponseMessage> GetCountry(string name)
        {
            var result = await Client.GetAsync("https://restcountries.eu/rest/v2/name/" + name);
            return result;
        }
    }
}
