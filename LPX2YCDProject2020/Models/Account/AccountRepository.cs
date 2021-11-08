using LPX2YCDProject2020.Models.PasswordResetModel;
using LPX2YCDProject2020.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserService userSevice,
            IEmailService emailService,
            IConfiguration config,
             RoleManager<IdentityRole> roleManager
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userSevice;
            _emailService = emailService;
            _config = config;
        }

        public async Task<IdentityResult> CreateProvincialLiaisonAsync(SignUpModel signUp)
        {
            var user = new ApplicationUser()
            {
                Email = signUp.Email,
                UserName = signUp.Email,
                DateJoined = signUp.DateJoined,
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, signUp.Password);

            if (result.Succeeded)
                await GenerateNewEmailTokenAsync(user);

            await _userManager.AddToRoleAsync(user, "Provincial Liaison Officer");
            return result;
        }

        public async Task<IdentityResult> CreateRegCoordinatorAsync(SignUpModel signUp)
        {
            var user = new ApplicationUser()
            {
                Email = signUp.Email,
                UserName = signUp.Email,
                DateJoined = signUp.DateJoined,
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, signUp.Password);

            if (result.Succeeded)
                await GenerateNewEmailTokenAsync(user);
            await _userManager.AddToRoleAsync(user, "RegCoordinator");
            return result;
        }

        public async Task<IdentityResult> AddUserToRoles(ApplicationUser user)
        {
            return await _userManager.AddToRoleAsync(user, "Learner");
            
        } 

        public async Task<ApplicationUser> GetUserById(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }
       
        public async Task<IdentityResult> ConfirmEmail(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        public async Task<IdentityResult> CreateLearnerAccountAsync(SignUpModel signUp)
        {
            var user = new ApplicationUser()
            {
                Email = signUp.Email,
                UserName = signUp.Email,
                DateJoined = signUp.DateJoined,
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
            };

            var result = await _userManager.CreateAsync(user, signUp.Password);

            if(result.Succeeded)
                await GenerateNewEmailTokenAsync(user);

            await _userManager.AddToRoleAsync(user, "Learner");

            return result;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpModel signUp)
        {
            var user = new ApplicationUser()
            {
                Email = signUp.Email,
                UserName = signUp.Email,
                DateJoined = signUp.DateJoined,
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                EmailConfirmed = true,
            };
           
            var result = await _userManager.CreateAsync(user, signUp.Password);

            if (result.Succeeded)
                await GenerateNewEmailTokenAsync(user);

            await _userManager.AddToRoleAsync(user, "Employee");
            return result; 
        }

        //Method generates an email confirmation message for a new user
        public async Task GenerateNewEmailTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendConfirmationEmail(user, token);
            }
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInModel signIn)
        {
            var result = await _signInManager.PasswordSignInAsync(signIn.Email, signIn.Password, signIn.rememberMe, true);
            return result;
        }

        public async Task SignOut() => await _signInManager.SignOutAsync();

        //Method allows users to change their passwords
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

        //This methid sends confirmation email to new user
        private async Task SendConfirmationEmail(ApplicationUser user, string token)
        {
            string appDomain = _config.GetSection("Application:AppDomain").Value;
            string confirmationLink = _config.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string> { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink, user.Id, token))

                }
            };

            await _emailService.SendEmailConfirmation(options);
        }

        private async Task SendForgotPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = _config.GetSection("Application:AppDomain").Value;
            string confirmationLink = _config.GetSection("Application:ForgotPassword").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string> { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink,user.Id, token))
                }
            };

            await _emailService.SendForgotPasswordEmail(options);
        }

        private async Task SendUserForgotPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = _config.GetSection("Application:AppDomain").Value;
            string confirmationLink = _config.GetSection("Application:ForgotUserPassword").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string> { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink,user.Id, token))
                }
            };

            await _emailService.SendForgotPasswordEmail(options);
        }

        public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
          
        }

        public async Task GenerateUserForgotPasswordTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendUserForgotPasswordEmail(user, token);
            }

        }

        //Mathod gets user details from the database by Email
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);
        } 
    }


}
