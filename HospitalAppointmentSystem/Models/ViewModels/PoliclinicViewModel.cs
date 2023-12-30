using HospitalAppointmentSystem.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.Models.ViewModels
{
    public class PoliclinicViewModel
    {
        [Key]
        public int PoliclinicId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? PoliclinicName { get; set; }
        public string MajorDepartment { get; set; }
    }
}
