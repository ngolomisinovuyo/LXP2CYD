using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.AddressModels
{
    public class CascadingAddressClass
    {
        public IEnumerable<City> cities { get; set; }
        public IEnumerable<Province> provinces { get; set; }
        public IEnumerable<Suburb> suburbs { get; set; }

        public Province Province { get; set; }
        public City City { get; set; }
        public Suburb Suburb { get; set; }
    }
}
