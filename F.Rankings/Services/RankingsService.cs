using F.Rankings.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace F.Rankings.Services
{
    public class RankingsService
    {
        private readonly HttpClient _client;

        public RankingsService(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration["PartnerApi.BaseUrl"]);
            _client = client;
        }

        public async Task<IEnumerable<Agent>> GetTopByPropertyListed(int count = 10) 
        { 
        }
    }
}
