using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.PasswordResetModel
{
    public class ResetPasswordModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required(ErrorMessage = "Enter a new password"), DataType(DataType.Password), Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Passwords do not match"), DataType(DataType.Password), Display(Name = "Confirm new password")]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }

        public bool IsSucess { get; set; }

    }
}
