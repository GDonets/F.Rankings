using System.Net.Http;
using System.Threading.Tasks;

namespace F.Rankings.Services.Clients
{
    public class BaseHttpClient : IBaseHttpClient
    {
        private readonly HttpClient _client;

        public BaseHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }
    }
}
