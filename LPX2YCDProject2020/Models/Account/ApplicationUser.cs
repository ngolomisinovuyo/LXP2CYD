using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser() { }
        public ApplicationUser(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is requred")]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string DateJoined { get; set; }

        //public virtual IList<IdentityRole> Roles { get; set; }
       

    }
}
