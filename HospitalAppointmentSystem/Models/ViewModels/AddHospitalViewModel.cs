using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.Models.ViewModels
{
    public class AddHospitalViewModel
    {
        public List<SelectListItem>? ProvinceSelectList { get; set; }
        public List<SelectListItem>? DistrictSelectList { get; set; }

        [Required(ErrorMessage = "Hastane Adı zorunludur.")]
        [MaxLength(50, ErrorMessage = "Hastane Adı en fazla 50 karakter olmalıdır.")]
        public string HospitalName { get; set; }

        [Required(ErrorMessage = "İl zorunludur.")]
        public int SelectedProvinceId { get; set; }

        [Required(ErrorMessage = "İlçe zorunludur.")]
        public int SelectedDistrictId { get; set; }
    }
}
