using HospitalAppointmentSystem.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAppointmentSystem.Models.ViewModels;
using HospitalAppointmentSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalAppointmentSystem.Controllers
{
    public class PoliclinicController : Controller
    {
        private readonly HospitalDbContext _context;
        public PoliclinicController(HospitalDbContext _context)
        {
            this._context = _context;
        }
        public IActionResult Index()
        {
            //listeleme
            var policlinics = _context.Policlinics
                .Include(p => p.MajorDepartment) //join
                .OrderBy(p => p.PoliclinicName)
                .ToList();

            var poliViewModels = policlinics.Select(p => new PoliclinicViewModel
            {
                PoliclinicId = p.PoliclinicId,
                PoliclinicName = p.PoliclinicName,
                MajorDepartment = p.MajorDepartment != null ? p.MajorDepartment.MajorDepartmentName : ""

            }).ToList();

            return View(poliViewModels); //poliklinik listele
        }

        //MajorList için yardımcı metot
        private List<SelectListItem> GetMajorDepartList()
        {
            var majors = _context.MajorDepartments.ToList();
            return majors.Select(m => new SelectListItem
            {
                Text = m.MajorDepartmentName,
                Value = m.MajorDepartmentId.ToString()
            }).ToList();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddPoliclinicViewModel
            {
                MajorDepartmentSelectList = GetMajorDepartList()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddPoliclinicViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newPol = new Policlinic
                {
                    PoliclinicName = model.PoliclinicName,
                    MajorDepartmentId=model.SelectedMajorDepartmentId
                };

                _context.Policlinics.Add(newPol);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            model.MajorDepartmentSelectList= GetMajorDepartList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var pol = await _context.Policlinics.FindAsync(id);
            if (pol == null)
            {
                return NotFound(); 
            }

            // pol.ye bağlı doktorları bul
            var doctorsWithPolId = _context.Doctors.Where(d => d.PoliclinicId == id).ToList();

            // doktorları sil
            _context.Doctors.RemoveRange(doctorsWithPolId);

            var apointmentsWithpolId = _context.Appointments.Where(d => d.PoliclinicId == id).ToList();

            // doktorları sil
            _context.Appointments.RemoveRange(apointmentsWithpolId);

            _context.Policlinics.Remove(pol);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
