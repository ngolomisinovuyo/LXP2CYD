using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.AddressModels
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public IList<Suburb> Suburbs { get; set; }
        public int provinceId { get; set; }
        public Province Province { get; set; }
    }
}
