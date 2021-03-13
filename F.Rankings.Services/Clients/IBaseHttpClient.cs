using System.Net.Http;
using System.Threading.Tasks;

namespace F.Rankings.Services.Clients
{
    public interface IBaseHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}