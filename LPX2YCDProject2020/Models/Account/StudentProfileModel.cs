using LPX2YCDProject2020.Models.AddressModels;
using LPX2YCDProject2020.Models.AdminModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class StudentProfileModel
    {
        
        [Key]   
        public string UserId{ get; set; }

        [StringLength(13)]
        [RegularExpression(@"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))", ErrorMessage = "Invalid ID number")]
        [Display(Name = "Identity number")]
        [Required(ErrorMessage = "Identity number is required")]
        public string IDNumber { get; set; }

        [Required(ErrorMessage = "Cellphone number is requred")]
        [Display(Name = "Cellphone number")]
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Date of birth is requred")]
        [Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Subject")]
        public int subjectId { get; set; }

        public virtual ICollection<StudentSubjects> Enrolments { get; set; } 

        public ICollection<EventReservations> Rsvps { get; set; }

        [Display(Name ="Gender")]
        public string Gender { get; set; }

        [Display(Name ="Suburb")]
        public int SuburbId { get; set; }
        public Suburb suburb { get; set; }

        [Display(Name ="Street address")]
        public string AddressLine1 { get; set; }

        [Display(Name ="Building/Complex")]
        public string AddressLine2 { get; set; }

        [Display(Name ="School name")]
        public string NameOfSchool { get; set; }

        [Display(Name ="Grade")]
        public string Grade { get; set; }


        [Display(Name ="City")]
        public int CityId { get; set; }

        [Display(Name ="Province")]
        public int ProvinceId { get; set; }

        [NotMapped]
        [Display(Name="Profile Photo")]
        public IFormFile ProfilePhoto { get; set; }

        public string ImageUrl { get; set; }
    }
}
