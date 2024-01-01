using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace HospitalAppointmentSystem.Controllers
{
    [AllowAnonymous]

    public class HomeController : Controller
    {
        public IActionResult SetCulture(string culture)
        {
            // Dil seçimini session'a kaydet
            ViewData["Culture"] = culture;

            // Dil seçimi yapıldıktan sonra yönlendirilecek sayfa
            return RedirectToAction("Index");
        }
        public IActionResult Index() //Anasayfa
        {
            var culture = ViewData["Culture"] as string ?? "tr";
            return View();
        }

        public IActionResult AccessDenied() //404 sayfası
        {
            return View();
        }
    }
}