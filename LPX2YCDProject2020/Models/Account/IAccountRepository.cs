using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Models.PasswordResetModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models
{
    public interface IAccountRepository
    {
        Task GenerateUserForgotPasswordTokenAsync(ApplicationUser user);
        Task<IdentityResult> CreateUserAsync(SignUpModel signUp);
        Task<IdentityResult> CreateLearnerAccountAsync(SignUpModel signUp);
        Task<SignInResult> PasswordSignInAsync(SignInModel signIn);
        Task SignOut();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);
        Task<IdentityResult> ConfirmEmail(string uid, string token);
        Task GenerateNewEmailTokenAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task GenerateForgotPasswordTokenAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
        Task<ApplicationUser> GetUserById(string Id);
        Task<IdentityResult> CreateAdminAsync(SignUpModel signUp);
        Task<IdentityResult> CreateProvincialLiaisonAsync(SignUpModel signUp);
        Task<IdentityResult> CreateRegCoordinatorAsync(SignUpModel signUp);

    }
}
