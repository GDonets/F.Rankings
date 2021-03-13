using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F.Rankings.Models
{
    public class Agent
    {
        public int MakelaarId { get; set; }
        public string MakelaarNaam { get; set; }
        public IEnumerable<Property> PropertyListed { get; set; }
    }
}
