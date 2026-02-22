using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrApp.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly SignInManager<IdentityUser> _siginManager;

        public IdentityService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManger)
        {
            _userManger = userManager;
            _siginManager = signinManger;
        }


        public async Task<IdentityServiceResult> LoginAsync(string username, string email, string password)
        {
            IdentityServiceResult result = await GetIdentityUserAsync(username, email);
            
            if (result.Succeeded)
                result = await LoginAsync(result.IdentityUser, password);

            return result;
        }

        private async Task<IdentityServiceResult> LoginAsync(IdentityUser user, string password)
        {
            var result = new IdentityServiceResult();
            var signin = await _siginManager.PasswordSignInAsync(user, password, false, false);
            
            if (signin.Succeeded)
                result.Succeeded = true;
            else
                result.AddError("Probleem met inloggen!");

            return result;
        }

        public async Task<IdentityServiceResult> RegisterAsync(RegisterViewModel registerData)
        {
            var result = new IdentityServiceResult();

            var user = new IdentityUser { UserName = registerData.UserName, Email = registerData.Email };
            var registration = await _userManger.CreateAsync(user, registerData.Password);
            
            if(registration.Succeeded)
            {
                result.Succeeded = true;
                result.IdentityUser = user;
            }
            else
            {
                foreach(var error in registration.Errors)
                {
                    result.AddError(error.Description);
                }
            }
            return result;
        }

        private async Task<IdentityServiceResult> GetIdentityUserAsync(string username, string email)
        {
            var result = new IdentityServiceResult();
            IdentityUser user = null!;

            if(username != null)
            {
                user = await _userManger.FindByNameAsync(username);
                if (user == null)
                    result.AddError("Geen gebruiker gevonden met deze gebruikersnaam!");
            }
            else
            {
                if(email != null)
                {
                    user = await _userManger.FindByEmailAsync(email);
                    if (user == null)
                        result.AddError("Geen gebruiker gevonden met dit emailadres!");
                }
            }

            if (user != null)
            {
                result.Succeeded = true;
                result.IdentityUser = user;
            }

            return result;
        }

        public async Task SignOutAsync()
        {
            await _siginManager.SignOutAsync();
        }

    }
}
