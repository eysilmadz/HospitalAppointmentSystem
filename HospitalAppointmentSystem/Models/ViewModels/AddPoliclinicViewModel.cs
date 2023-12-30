using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.Models.ViewModels
{
    public class AddPoliclinicViewModel
    {
        [Required(ErrorMessage = "Poliklinik Adı zorunludur.")]
        [MaxLength(50, ErrorMessage = "Poliklinik Adı en fazla 50 karakter olmalıdır.")]
        public string PoliclinicName { get; set; }

        public List<SelectListItem>? MajorDepartmentSelectList { get; set; }
        [Required(ErrorMessage = "Ana Bilim Dalını seçmek zorunludur.")]
        public int SelectedMajorDepartmentId { get; set; }

    }
}
