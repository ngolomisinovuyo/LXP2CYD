using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models
{
    public class ContactUsFormModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Please enter first name"), Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name"), Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter last name"), Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage ="Please select"), Display(Name = "Select a description that describes you best")]
        public string PatronType { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

    }
}
