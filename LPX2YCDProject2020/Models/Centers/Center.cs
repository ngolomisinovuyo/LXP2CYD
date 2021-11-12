using System;
using System.ComponentModel.DataAnnotations.Schema;
using LPX2YCDProject2020.Models.Regions;

namespace LPX2YCDProject2020.Models.Centers
{
    public class Center
    {
        public Center()
        {
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public CenterType Type { get; set; }
        public string ManagerId { get; set; }
        public int RegionId { get; set; }
        [ForeignKey(nameof(RegionId))]
        public Region Region { get; set; }
    }
}
