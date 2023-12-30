using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    [AllowAnonymous]

    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(AdminLoginViewModel param)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(param.FullName, param.Password, false, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                return View("AdminLogin");
            }
            return View();
        }

        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.TcKimlikNo, model.Password, false, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn", "User");
                }
                return View("UserLogin");
            }
            return View(model);
        }
    }
}
