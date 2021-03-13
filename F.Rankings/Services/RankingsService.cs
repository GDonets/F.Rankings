using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using F.Rankings.Converters;

namespace F.Rankings.Services
{
    public class RankingsService : IRankingsService
    {
        private const string WithoutGardenParameters = "type=koop&zo=/amsterdam";
        private const string WithGardenParameters = "type=koop&zo=/amsterdam/tuin";
        private const string PageSizeParameter = "pagesize=25";

        private readonly string _apiKey = string.Empty;
        private readonly HttpClient _client;
        private readonly IModelDtoConverter<Models.Property, DTO.Property> _propertyConverter;

        public RankingsService(
            HttpClient client,
            IConfiguration configuration,
            IModelDtoConverter<Models.Property, DTO.Property> propertyConverter)
        {
            client.BaseAddress = new Uri(configuration["PartnerApi.BaseUrl"]);
            _client = client;
            _propertyConverter = propertyConverter;
            _apiKey = configuration["PartnerApi.Key"];
        }

        public async Task<IEnumerable<Models.Agent>> GetTopByPropertyListed(int count = 10)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append($"/{_apiKey}/?{WithoutGardenParameters}&page=0&{PageSizeParameter}");

            return await GetAllByQuery(count, queryBuilder);
        }

        public async Task<IEnumerable<Models.Agent>> GetTopByPropertyListedWithGarden(int count = 10)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append($"/{_apiKey}/?{WithGardenParameters}&page=0&{PageSizeParameter}");

            return await GetAllByQuery(count, queryBuilder);
        }

        private async Task<IEnumerable<Models.Agent>> GetAllByQuery(int count, StringBuilder queryBuilder)
        {
            var agentCache = new Dictionary<int, DTO.Agent>();

            while (true)
            {
                var response = await _client.GetAsync(queryBuilder.ToString());

                var res = await JsonSerializer.DeserializeAsync<DTO.ApiResponse>(await response.Content.ReadAsStreamAsync());

                foreach (var o in res.Objects)
                {
                    if (agentCache.ContainsKey(o.MakelaarId))
                    {
                        agentCache[o.MakelaarId].PropertyListed.Add(o);
                    }
                    else
                    {
                        var agent = new DTO.Agent
                        {
                            MakelaarId = o.MakelaarId,
                            MakelaarNaam = o.MakelaarNaam,
                            PropertyListed = new List<DTO.Property> { o },
                        };

                        agentCache.Add(agent.MakelaarId, agent);
                    }
                }

                if (res.Paging.AantalPaginas <= 0) break;

                queryBuilder.Replace($"&page={res.Paging.HuidigePagina}", $"&page={res.Paging.HuidigePagina + 1}");
            }

            return agentCache.Values
                .OrderByDescending(x => x.PropertyListed.Count())
                .Take(count)
                .Select(x => new Models.Agent
                {
                    MakelaarId = x.MakelaarId,
                    MakelaarNaam = x.MakelaarNaam,
                    PropertyListed = x.PropertyListed.Select(x => _propertyConverter.ToModel(x)),
                });
        }
    }
}
