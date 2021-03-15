using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F.Rankings.Converters;
using F.Rankings.Infrastructure;
using F.Rankings.Models;
using F.Rankings.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F.Rankings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingsController : ControllerBase
    {
        private readonly IRankingsService _service;
        private readonly IModelDtoConverter<Models.Property, DTO.Property> _propertyConverter;
        private readonly ICacheService _cache;

        private const string withGardenKey = "topwithgarden";
        private const string noGardenKey = "topnogarden";

        public RankingsController(
            IRankingsService service,
            ICacheService cache,
            IModelDtoConverter<Models.Property, DTO.Property> propertyConverter)
        {
            _service = service;
            _cache = cache;
            _propertyConverter = propertyConverter;
        }

        // GET: api/Rankings/topagentswithgarden/{quantity}
        [HttpGet("topagentswithgarden/{quantity}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<RankedAgent>>> GetTopAgentsWithGarden(int quantity)
        {
            if (quantity < 1)
            {
                return BadRequest("Quantity cannot be less than 1");
            }

            if (_cache.TryGet(withGardenKey, out IEnumerable<RankedAgent> lastGet)
                    && lastGet.Count() == quantity)
            {
                return Ok(lastGet);
            }

            var serviceRes = await _service.GetTopByPropertyListedWithGarden(quantity);
            IEnumerable<RankedAgent> res = MapServiceResponseToModels(serviceRes);

            _cache.TryAdd(withGardenKey, res);

            return Ok(res);
        }

        // GET: api/Rankings/topagentsnogarden/{quantity}
        [HttpGet("topagentsnogarden/{quantity}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<RankedAgent>>> GetTopAgentsWithoutGraden(int quantity)
        {
            if (quantity < 1)
            {
                return BadRequest("Quantity cannot be less than 1");
            }

            if (_cache.TryGet(noGardenKey, out IEnumerable<RankedAgent> lastGet)
                    && lastGet.Count() == quantity)
            {
                return Ok(lastGet);
            }

            var serviceRes = await _service.GetTopByPropertyListed(quantity);
            IEnumerable<RankedAgent> res = MapServiceResponseToModels(serviceRes);

            _cache.TryAdd(noGardenKey, res);

            return Ok(res);
        }

        private IEnumerable<RankedAgent> MapServiceResponseToModels(IEnumerable<DTO.Agent> serviceRes)
        {
            var counter = 0;
            var res = serviceRes.Select(a =>
            {
                var agent = new Agent
                {
                    MakelaarId = a.MakelaarId,
                    MakelaarNaam = a.MakelaarNaam,
                    PropertyListed = a.PropertyListed.Select(p => _propertyConverter.ToModel(p))
                };

                return new RankedAgent
                {
                    Agent = agent,
                    Rank = ++counter
                };
            });

            return res;
        }
    }
}
