using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalAppointmentSystem.Models.ViewModels
{
    public class MajorDepartmentViewModel
    {
        [Display(Name = "Hangi Hastaneye eklenilecek")]
        public int SelectedHospitalId { get; set; }

        public List<SelectListItem>? SelectHospital { get; set; }
        [Key]
        public int MajorDepartmentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? MajorDepartmentName { get; set; }

    }
}
