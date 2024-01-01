using HospitalAppointmentSystem.DataBase;
using HospitalAppointmentSystem.Models.Entities;
using HospitalAppointmentSystem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HospitalAppointmentSystem.Controllers
{
    [Authorize(Roles = "Admin")]

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
            var model = new MajorDepartmentViewModel
            {
                SelectHospital = GetHospitalSelectListAsync().Result
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MajorDepartmentViewModel model)
        {
            model.SelectHospital = await GetHospitalSelectListAsync();

            if (ModelState.IsValid)
            {
                var major = new MajorDepartment
                {
                    MajorDepartmentName = model.MajorDepartmentName
                };

                // Veritabanına ekleme
                _context.MajorDepartments.Add(major);
                await _context.SaveChangesAsync();

                // HospitalMajorDepartment tablosuna ekler
                int selectedHospitalId = model.SelectedHospitalId;

                var hospitalMajorDepartment = new HospitalMajorDepartment
                {
                    HospitalId = selectedHospitalId,
                    MajorDepartmentId = major.MajorDepartmentId
                };

                _context.HospitalMajorDepartment.Add(hospitalMajorDepartment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Eğer model geçerli değilse, formu tekrar göster ve SelectHospital'ı doldur
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

        private async Task<List<SelectListItem>> GetHospitalSelectListAsync()
        {
            var hospitals = await _context.Hospitals.ToListAsync();
            return hospitals.Select(h => new SelectListItem
            {
                Text = h.HospitalName,
                Value = h.HospitalId.ToString()
            }).ToList();
        }
    }
}
