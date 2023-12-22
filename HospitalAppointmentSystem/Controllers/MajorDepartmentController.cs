using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    public class MajorDepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View(); //MajorDepartment ekle-güncelle-sil-listele
        }
    }
}
