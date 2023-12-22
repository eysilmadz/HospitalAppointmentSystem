using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    public class HospitalController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Hospital ekle-güncelle-sil-listele
        }
    }
}
