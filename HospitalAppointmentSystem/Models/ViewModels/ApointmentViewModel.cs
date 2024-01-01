using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalAppointmentSystem.Models.ViewModels
{
    public class ApointmentViewModel
    {
        public List<SelectListItem> ?ProvinceSelectList { get; set; }
        public List<SelectListItem> ?DistrictSelectList { get; set; }
        public List<SelectListItem> ?MajorDepartSelectList { get; set; }
        public List<SelectListItem> ?HospitalSelectList { get; set; }
        public List<SelectListItem> ?ClinicSelectList { get; set; }
        public List<SelectListItem> ?DoctorSelectList { get; set; }

        //secilen degerler
        public int SelectedProvince {  get; set; }
        public int SelectedDistrict {  get; set; }
        public int SelectedMajorDepart {  get; set; }
        public int SelectedHospital {  get; set; }
        public int SelectedClinic {  get; set; }
        public int SelectedDoctor {  get; set; }
    }
}
