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
    public class AllCenterDetails
    {
        [Key]
        public string CenterId { get; set; }


        [Display(Name = "Center Name")]
        public string BusinessName { get; set; }

        [Display(Name = "Center Manager")]
        public string Manager { get; set; }

        [Display(Name = "Region")]
        public int Suburb { get; set; }

        [Display(Name = "Number of youth")]
        public int YouthNumber { get; set; }

        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Contact number")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Physical address is required")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address 2")]
        public string AddressLine2 { get; set; }

    }
}
