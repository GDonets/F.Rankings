using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F.Rankings.DTO
{
    public class ApiResponse
    {
        public IEnumerable<Property> Objects { get; set; }
        public Paging Paging { get; set; }
        public int TotaalAantalObjecten { get; set; }
    }
}
