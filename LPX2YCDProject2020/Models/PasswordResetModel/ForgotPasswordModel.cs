using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.PasswordResetModel
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage ="Email address required"),EmailAddress ,Display(Name = "Registered email address")]
        public string Email { get; set; }

        public bool EmailSent { get; set; }
    }
}
