using PostsAndComments.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAndComments.Services.Interface
{
    public interface IUser
    {
        Task<MessageResponseViewModel> RegisterAsync(RegisterViewModel registeration);
        Task<MessageResponseViewModel> LoginUserAsync(LoginViewModel login);
        Task<MessageResponseViewModel> ConfirmEmailAsync(string userId, string token);
        Task<MessageResponseViewModel> ForgetPasswordAsync(string email);
        Task<MessageResponseViewModel> ResetPasswordAsync(ResetPasswordViewModel reset);
    }
}
