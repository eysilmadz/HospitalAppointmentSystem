using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HospitalAppointmentSystem.ViewModels;

namespace HospitalAppointmentSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(RegisterViewModel p)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    IdentityNo = p.UserIdentityNo,
                    UserName = p.UserName,
                    Surname = p.UserSurname,
                    Gender = p.UserGender,
                    Birthday = p.UserBirthday,
                    Email = p.UserMail,
                };

                var result = await _userManager.CreateAsync(user, p.UserPassword);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(p);
        }
    }
}
