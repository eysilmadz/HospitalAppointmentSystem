using HospitalAppointmentSystem.DataBase;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Controllers
{
    [Authorize(Roles ="Admin")]

    public class AdminController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult ListUser()
        {
            var users = _userManager.Users;
            return View(users);
        }


        public IActionResult ListRol()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateRol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRol(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new AppRole
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRol");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var result = await _roleManager.DeleteAsync(values);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRol");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            var model = new AdminCreateViewModel
            {
                RolesList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };
            return View("CreateUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AdminCreateViewModel model)
        {
            var user = new AppUser
            {
                FullName = model.UserName + model.UserSurname,
                IdentityNo = model.UserIdentityNo,
                UserName = model.UserName,
                Surname = model.UserSurname,
                Gender = model.UserGender,
                Birthday = model.UserBirthday,
                Email = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
                return RedirectToAction("ListUser");
            }

            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View();
        }

        public async Task<IActionResult> EditUser(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ListUser");
            }
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                return View(new EditUserViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    EmailAddress = user.Email,
                    UserName = user.UserName,
                    UserSurname = user.Surname,
                    PhoneNumber = user.PhoneNumber,
                });


            }
            return RedirectToAction("ListUser");
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, EditUserViewModel model)
        {


            if (id != model.Id)
            {
                return RedirectToAction("ListUser");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    user.Email = model.EmailAddress;
                    user.FullName = model.FullName;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Surname = model.UserSurname;
                    user.UserName = model.UserName;


                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, model.Password);
                    }

                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("ListUser");
        }

    }
}
