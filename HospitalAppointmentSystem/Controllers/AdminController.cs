using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    //[Area("Admin")]

    public class AdminController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public AdminController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index() //kurum login
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            var values = _roleManager.Roles.ToList();
            return View(values); //admin paneli
        }
    }
}
