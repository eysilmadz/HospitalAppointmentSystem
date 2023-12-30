using HospitalAppointmentSystem.DataBase;
using HospitalAppointmentSystem.Models.Entities;
using HospitalAppointmentSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Controllers
{
    public class HospitalController : Controller
    {
        private readonly HospitalDbContext _context;

        public HospitalController(HospitalDbContext _context)
        {
            this._context = _context;
        }

        private string GetProvinceNameByDistrictId(int districtId)
        {
            var district = _context.Districts
                .Include(d => d.Province)
                .FirstOrDefault(d => d.DistrictId == districtId);

            return district?.Province?.ProvinceName ?? "";
        }
        public IActionResult Index()
        {
            //listeleme
            var hospitals = _context.Hospitals
                    .Include(h => h.District) // District tablosunu join et
                    .OrderBy(h => h.HospitalName)
                    .ToList();

            var hospViewModels = hospitals.Select(h => new HospitalViewModel
            {
                HospitalId = h.HospitalId,
                HospitalName = h.HospitalName,
                District = h.District != null ? h.District.DistrictName : "",
                Province = GetProvinceNameByDistrictId(h.DistrictId)
            }).ToList();

            return View(hospViewModels);
        }

        //ProvinceList için yardımcı metot
        private List<SelectListItem> GetProvinceList()
        {
            var provinces = _context.Provinces.ToList();
            return provinces.Select(p => new SelectListItem
            {
                Text = p.ProvinceName,
                Value = p.ProvinceId.ToString()
            }).ToList();
        }

        //DistrictList için yardımcı metot
        private List<SelectListItem> GetDistrictList()
        {
            var districts = _context.Districts.ToList();
            return districts.Select(d => new SelectListItem
            {
                Text = d.DistrictName,
                Value = d.DistrictId.ToString()
            }).ToList();
        }

        [HttpGet]
        public IActionResult GetDistricts(int idProvince)
        {
            var ilceler = _context.Districts
                .Where(d => d.ProvinceId == idProvince)
                .Select(d => new SelectListItem
                {
                    Text = d.DistrictName,
                    Value = d.DistrictId.ToString(),
                }).ToList();
            return Json(ilceler);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddHospitalViewModel
            {
                ProvinceSelectList = GetProvinceList(),
                DistrictSelectList = GetDistrictList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddHospitalViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Yeni hastane nesnesi oluştur
                var newHospital = new Hospital
                {
                    HospitalName = model.HospitalName,
                    DistrictId = model.SelectedDistrictId
                };

                // Yeni hastaneyi veritabanına ekle
                _context.Hospitals.Add(newHospital);


                // Değişiklikleri kaydet
                await _context.SaveChangesAsync();

                // Hastane ekleme başarılı oldu, Index sayfasına yönlendir
                return RedirectToAction(nameof(Index));
            }

            model.ProvinceSelectList = GetProvinceList();
            model.DistrictSelectList = GetDistrictList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);

            if (hospital == null)
            {
                return NotFound(); // Hastane bulunamadıysa 404 hatası döndür
            }

            // Hastaneye bağlı doktorları bul
            var doctorsWithHospitalId = _context.Doctors.Where(d => d.HospitalId == id).ToList();

            // Hastaneye bağlı doktorları sil
            _context.Doctors.RemoveRange(doctorsWithHospitalId);
            
            // Hastaneye bağlı randevuları sil

            var appointmentsWithHospitalId = _context.Appointments.Where(d => d.HospitalId == id).ToList();
            _context.Appointments.RemoveRange(appointmentsWithHospitalId);

            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // Silme işlemi başarılı, Index sayfasına yönlendir
        }


    }
}
