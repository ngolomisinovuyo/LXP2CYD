using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class  ExternalManagement
    {
        [Key]
        public string UserId { get; set; }


        [Display(Name = "Street address")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Unit/Complex")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Suburb")]
        public int SuburbId { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        [Required(ErrorMessage = " Please enter a value")]
        public string Occupation { get; set; }

        [Display(Name = "Contact number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Office number")]
        public string OfficeNumber { get; set; }

        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select an option for gender")]
        public string Gender { get; set; }

        [NotMapped]
        [Display(Name = "Profile Photo")]
        public IFormFile ProfilePhoto { get; set; }

        public string ImageUrl { get; set; }
    }
}
