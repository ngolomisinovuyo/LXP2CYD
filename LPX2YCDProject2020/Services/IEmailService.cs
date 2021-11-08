using LPX2YCDProject2020.Models.Account;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmation(UserEmailOptions userEmailOptions);
        Task SendForgotPasswordEmail(UserEmailOptions userEmailOptions);
        Task SendEqnuiryResponseEmail(UserEmailOptions userEmailOptions);
    }
}