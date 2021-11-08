using LPX2YCDProject2020.Models.AddressModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.HomeModels
{
    public class CenterDetails
    {
        [Key]
        public int CenterId { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [Display(Name = "Center name")]
        public string BusinessName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Phone nummber is required")]
        [Display(Name = "Phone number")]
        public string ContactNumber { get; set; }


        [Display(Name = "Web address")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Suburb name is required")]
        [Display(Name = "Suburb")]
        public int SuburbId { get; set; }

        public Suburb Suburb { get; set; }

        [Display(Name = "Street address")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Unit/Complex")]
        public string AddressLine2 { get; set; }

        public bool Saved { get; set; }

        [NotMapped]
        [Display(Name = "Profile Photo")]
        public IFormFile ProfilePhoto { get; set; }

        public string ImageUrl { get; set; }
    }
}
