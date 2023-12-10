using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.Models.Entities
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? PatientName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? PatientSurame { get; set; }

        [Required]
        [MaxLength(50)]
        public string? PatientIdentityNo { get; set; }

        [Required]
        [MaxLength(50)]
        public string? PatientPassword { get; set; }

        public ICollection<Appointment> ?Appointments { get; set; }
        
    }
}
