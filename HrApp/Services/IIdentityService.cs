using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HrApp.Services
{
    public interface IIdentityService
    {
        Task<IdentityServiceResult> LoginAsync(string username, string email, string password);
        Task SignOutAsync();
        Task<IdentityServiceResult> RegisterAsync(RegisterViewModel registerData);
    }
}
