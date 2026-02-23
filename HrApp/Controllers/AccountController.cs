using HrApp.Services;
using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HrApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityService _service;

        public AccountController(IIdentityService service)
        {
            _service = service;
        }

        #region Login
        public IActionResult Login()
        {
            return View();
        }
        #endregion

        #region Login Username

        [HttpGet]
        public IActionResult LoginUserName()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUserName(LoginUserNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var searchUser = await _userManager.FindByNameAsync(model.UserName);
                if (searchUser is not null)
                {
                    var result = await _signInManager.PasswordSignInAsync(searchUser, model.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                }
            }
            return View(model);
        }

        #endregion

        #region Login Email

        [HttpGet]
        public IActionResult LoginEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginEmailAsyc(LoginEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var searchUser = await _userManager.FindByEmailAsync(model.Email);
                if (searchUser is not null)
                {
                    var result = await _signInManager.PasswordSignInAsync(searchUser, model.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                }
            }
            return View(model);
        }

        #endregion


        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    Email = registerModel.Email,
                    UserName = registerModel.UserName
                };
                var result = await _userManager.CreateAsync(identityUser, registerModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View();
        }

        #endregion

        #region Logout

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        #endregion
    }
}
