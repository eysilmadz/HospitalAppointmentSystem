using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.Models.ViewModels
{
    public class MajorDepartmentViewModel
    {
        [Key]
        public int MajorDepartmentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? MajorDepartmentName { get; set; }
    }
}
