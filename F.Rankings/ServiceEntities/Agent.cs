using F.Rankings.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F.Rankings.ServiceEntities
{
    public class Agent
    {
        public int MakelaarId { get; set; }
        public string MakelaarNaam { get; set; }
        public List<Property> PropertyListed { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Agent cast && MakelaarId == cast.MakelaarId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MakelaarId);
        }
    }
}
