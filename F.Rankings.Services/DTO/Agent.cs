using System.Collections.Generic;

namespace F.Rankings.DTO
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
            return MakelaarId.GetHashCode();
        }
    }
}
