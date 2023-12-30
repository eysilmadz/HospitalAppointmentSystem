using HospitalAppointmentSystem.DataBase;
using HospitalAppointmentSystem.Models.Entities;
using HospitalAppointmentSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers
{
    public class MajorDepartmentController : Controller
    {
        //MajorDepartment ekle-güncelle-sil-listele
        private readonly HospitalDbContext _context;
        public MajorDepartmentController(HospitalDbContext _context)
        {
            this._context = _context;
        }
        public IActionResult Index()
        {
            var departs=_context.MajorDepartments.ToList();
            var departsViewModels = departs.Select(d => new MajorDepartmentViewModel
            {
                MajorDepartmentId=d.MajorDepartmentId,
                MajorDepartmentName=d.MajorDepartmentName

            }).ToList();
            return View(departsViewModels); 
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MajorDepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var major = new MajorDepartment
                {
                    MajorDepartmentName = model.MajorDepartmentName
                };

                // Veritabanına ekleme
                _context.MajorDepartments.Add(major);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var mj = await _context.MajorDepartments.FindAsync(id);
            if (mj == null)
            {
                return NotFound();
            }

            var policlinicsWithMajorId = _context.Policlinics.Where(d => d.MajorDepartmentId == id).ToList();
            _context.Policlinics.RemoveRange(policlinicsWithMajorId);

            
            var doctorsWithMajorId = _context.Doctors.Where(d => d.MajorDepartmentId == id).ToList();
            _context.Doctors.RemoveRange(doctorsWithMajorId);

            var apointmentsWithmajorId = _context.Appointments.Where(d => d.MajorDepartmentId == id).ToList();
            _context.Appointments.RemoveRange(apointmentsWithmajorId);

            _context.MajorDepartments.Remove(mj);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
