using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.AddressModels
{
    public class Province
    {
        public int ProvinceId { get; set; }

        [Display(Name = "Province name")]
        public string ProvinceName { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        public IList<City> City { get; set; }
    }
}
