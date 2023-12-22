using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    public class PoliclinicController : Controller
    {
        public IActionResult Index()
        {
            return View(); //poliklinik ekle-güncelle-sil-listele
        }
    }
}
