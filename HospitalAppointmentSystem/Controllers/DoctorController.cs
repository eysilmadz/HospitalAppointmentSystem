using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Doctor ekle-güncelle-sil-listele
        }
    }
}
