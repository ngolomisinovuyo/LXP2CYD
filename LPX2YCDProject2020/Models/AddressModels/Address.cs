using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPX2YCDProject2020.Models.AddressModels
{
    public class Address
    {
  
        public int Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public int CityId { get; set; }
        public int ProvinceId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City City { get; set; }
        [ForeignKey(nameof(ProvinceId))]
        public Province Provnice { get; set; }

        public static Address Create(string line1,string line2, int cityId, int provinceId)
        {
             return new Address
            {
                Line1 = line1,
                Line2 = line2,
                CityId = cityId,
                ProvinceId = provinceId
            };
        }
    }
}
