using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.AddressModels
{
    public class Suburb
    {
        public int SuburbId { get; set; }
        public string SuburbName { get; set; }
        public string PostalCode { get; set; }
        public int CityId { get; set; }

        public City City { get; set; }
    }
}
