using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class StaffProfiles
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Suburb")]
        public int SuburbId { get; set; }

        [Display(Name = "Street address")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Unit/Complex")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Date employed")]
        public DateTime DateEmployed { get; set; }

        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Display(Name = "Hourly rate")]
        public string RatePerHour { get; set; }
    }
}
