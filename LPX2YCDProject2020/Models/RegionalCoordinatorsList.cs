using LPX2YCDProject2020.Models.AddressModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models
{
    public class RegionalCoordinatorsList
    {
        [Key]
        public string RegCoordinatorId { get; set; }


        [Display(Name = "Regional Coordinator Name")]
        public string Name { get; set; }

        [Display(Name = "Regional Coordinator Surname")]
        public string Surname { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Display(Name = "Contact number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Office number")]
        public string OfficeNumber { get; set; }
    }
}
