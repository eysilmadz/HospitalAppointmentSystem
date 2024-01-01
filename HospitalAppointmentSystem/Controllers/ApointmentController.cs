using HospitalAppointmentSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalAppointmentSystem.DataBase;
using HospitalAppointmentSystem.Models.Entities;
using HospitalAppointmentSystem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace HospitalAppointmentSystem.Controllers
{
    public class ApointmentController : Controller
    {
        private readonly HospitalDbContext _context;

        public ApointmentController(HospitalDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //veri tabanından illeri çeker
            var provinces = await _context.Provinces.ToListAsync();

            //major depart çeker
            var majordeparts = await _context.MajorDepartments.ToListAsync();

            //doktorları çeker
            //var doctors = await _context.Doctors.ToListAsync();


            //ViewModel oluşturur
            var model = new ApointmentViewModel();

            model.ProvinceSelectList = new List<SelectListItem>();
            model.DistrictSelectList = new List<SelectListItem>();
            model.MajorDepartSelectList = new List<SelectListItem>();
            model.ClinicSelectList = new List<SelectListItem>();
            model.DoctorSelectList = new List<SelectListItem>();

            foreach (var province in provinces)
            {
                model.ProvinceSelectList.Add(new SelectListItem
                {
                    Text = province.ProvinceName,
                    Value = province.ProvinceId.ToString()
                });
            }
            foreach (var m in majordeparts)
            {
                model.MajorDepartSelectList.Add(new SelectListItem
                {
                    Text = m.MajorDepartmentName,
                    Value = m.MajorDepartmentId.ToString()
                });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ApointmentViewModel model)
        {
            var selectedProvince = model.SelectedProvince;
            var selectedDistrict = model.SelectedDistrict;
            var selectedMajor = model.SelectedMajorDepart;
            var selectedClinic = model.SelectedClinic;
            var selectedDoctor = model.SelectedDoctor;
            var selectedHospital = model.SelectedHospital;
            var selectedClinicId = model.SelectedClinic;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult GetDistricts(int id_province)
        {
            var ilceler = _context.Districts
                .Where(d => d.ProvinceId == id_province)
                .Select(d => new SelectListItem
                {
                    Text = d.DistrictName,
                    Value = d.DistrictId.ToString(),
                }).ToList();
            return Json(ilceler);
        }
        [HttpGet]
        public IActionResult GetHospitalsForMajorDepart(int id_district, int id_majorDepart)
        {
            var hospitals = _context.Hospitals
                .Include(h => h.MajorDepartments)  // Include 
                .Where(h =>
                       h.MajorDepartments.Any(md => md.MajorDepartmentId == id_majorDepart) &&
                       h.DistrictId == id_district)
                .Select(h => new SelectListItem
                {
                    Text = h.HospitalName,
                    Value = h.HospitalId.ToString()
                })
                    .ToList();
            return Json(hospitals);
        }
        [HttpGet]
        public IActionResult GetPoliclinicsForHospitalAndMajorDepart(int id_hospital, int id_majorDepart)
        {
            var model = new ApointmentViewModel();
            // Seçilen hastane ve major department'a göre HospitalMajorDepartment'leri ve Polyclinics'leri getir
            var query = from hmd in _context.HospitalMajorDepartment
                        join p in _context.Policlinics on hmd.MajorDepartmentId equals p.MajorDepartmentId
                        where hmd.HospitalId == id_hospital &&
                              hmd.MajorDepartmentId == id_majorDepart
                        select p;

            var polyclinics = query.ToList();

            // Poliklinikleri ViewModel'e doldurun
            model.ClinicSelectList = polyclinics
                .Select(p => new SelectListItem
                {
                    Value = p.PoliclinicId.ToString(),
                    Text = p.PoliclinicName
                })
                .ToList();


            return Json(model.ClinicSelectList);
        }

        [HttpGet]
        public IActionResult GetDoctors(int id_clinic)
        {
            var doktorlar = _context.Doctors
                .Where(d => d.PoliclinicId == id_clinic)
                .Select(d => new SelectListItem
                {
                    Text = d.DoctorName+" "+d.DoctorSurname,
                    Value = d.DoctorId.ToString(),
                }).ToList();
            return Json(doktorlar);
        }
    }
}