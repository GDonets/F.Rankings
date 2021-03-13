using F.Rankings.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace F.Rankings.Services
{
    public interface IRankingsService
    {
        Task<IEnumerable<Agent>> GetTopByPropertyListed(int count = 10);
        Task<IEnumerable<Agent>> GetTopByPropertyListedWithGarden(int count = 10);
    }
}