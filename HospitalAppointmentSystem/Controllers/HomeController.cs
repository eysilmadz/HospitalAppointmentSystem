using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HospitalAppointmentSystem.Controllers
{
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