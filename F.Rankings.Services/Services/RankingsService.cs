using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F.Rankings.DTO;
using F.Rankings.Services.Services;
using F.Rankings.Services.Clients;
using Microsoft.Extensions.Logging;

namespace F.Rankings.Services
{
    public class RankingsService : IRankingsService
    {
        private const string WithoutGardenParameters = "type=koop&zo=/amsterdam";
        private const string WithGardenParameters = "type=koop&zo=/amsterdam/tuin";
        private const string PageSizeParameter = "pagesize=25";

        private readonly string _apiKey = string.Empty;
        private readonly string _baseUrl = string.Empty;
        private readonly IBaseHttpClient _client;
        private readonly ILogger<RankingsService> _logger;

        public RankingsService(IBaseHttpClient client, RankingsServiceOptions options, ILogger<RankingsService> logger)
        {
            _baseUrl = options.ApiBaseUrl;
            _client = client;
            _apiKey = options.ApiKey;
            _logger = logger;
        }

        public async Task<IEnumerable<Agent>> GetTopByPropertyListed(int count = 10)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(_baseUrl);
            queryBuilder.Append($"/{_apiKey}/?{WithoutGardenParameters}&{PageSizeParameter}");

            return await GetAllByQuery(count, queryBuilder);
        }

        public async Task<IEnumerable<Agent>> GetTopByPropertyListedWithGarden(int count = 10)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(_baseUrl);
            queryBuilder.Append($"/{_apiKey}/?{WithGardenParameters}&{PageSizeParameter}");

            return await GetAllByQuery(count, queryBuilder);
        }

        private async Task<IEnumerable<Agent>> GetAllByQuery(int count, StringBuilder queryBuilder)
        {
            // can be cleaned from accidentally added rogue page param with regex
            queryBuilder.Append("&page=1");
            var agentCache = new Dictionary<int, Agent>();

            while (true)
            {
                var response = await _client.GetAsync(queryBuilder.ToString());

                if (!response.IsSuccessStatusCode) 
                {
                    _logger.LogError($"Cannot get response! Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");

                    throw new Exception($"Cannot get response! Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                }

                var res = await JsonSerializer.DeserializeAsync<ApiResponse>(await response.Content.ReadAsStreamAsync());

                foreach (var o in res.Objects)
                {
                    if (agentCache.ContainsKey(o.MakelaarId))
                    {
                        agentCache[o.MakelaarId].PropertyListed.Add(o);
                    }
                    else
                    {
                        var agent = new Agent
                        {
                            MakelaarId = o.MakelaarId,
                            MakelaarNaam = o.MakelaarNaam,
                            PropertyListed = new List<Property> { o },
                        };

                        agentCache.Add(agent.MakelaarId, agent);
                    }
                }

                if (res.Paging.HuidigePagina == res.Paging.AantalPaginas) break;

                queryBuilder.Replace($"&page={res.Paging.HuidigePagina}", $"&page={res.Paging.HuidigePagina + 1}");
            }

            return agentCache.Values
                .OrderByDescending(x => x.PropertyListed.Count())
                .Take(count);
        }
    }
}
