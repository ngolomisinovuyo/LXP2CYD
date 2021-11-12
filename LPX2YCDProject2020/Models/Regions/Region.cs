using System;
using System.ComponentModel.DataAnnotations.Schema;
using LPX2YCDProject2020.Models.AddressModels;

namespace LPX2YCDProject2020.Models.Regions
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }
    }
}
