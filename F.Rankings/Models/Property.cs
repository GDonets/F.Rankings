using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F.Rankings.Models
{
    public class Property
    {
        public Guid? Id { get; set; }
        public int GlobalId { get; set; }
        public string AangebodenSindsTekst { get; set; }
        public DateTime? AanmeldDatum { get; set; }
        public int? AantalKamers { get; set; }
        public int? AantalKavels { get; set; }
        public string Aanvaarding { get; set; }
        public string Adres { get; set; }
        public string Foto { get; set; }
        public string FotoLarge { get; set; }
        public string FotoLargest { get; set; }
        public string FotoMedium { get; set; }
        public string FotoSecure { get; set; }
        public string Land { get; set; }
        public Price Prijs { get; set; }
        public DateTime PublicatieDatum { get; set; }
        public string URL { get; set; }
        public float WGS84_X { get; set; }
        public float WGS84_Y { get; set; }
        public string Woonplaats { get; set; }
    }
}
