using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HospitalAppointmentSystem.Controllers
{
    [AllowAnonymous]

    public class HomeController : Controller
    {
        public IActionResult Index() //Anasayfa
        {
            return View();
        }

        public IActionResult AccessDenied() //404 sayfası
        {
            return View();
        }
    }
}