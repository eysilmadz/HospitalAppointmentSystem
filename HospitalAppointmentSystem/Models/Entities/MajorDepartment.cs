using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models.Entities
{
    public class MajorDepartment
    {
        [Key]
        public int MajorDepartmentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? MajorDepartmentName { get; set; }
        
        public ICollection<HospitalMajorDepartment> ?Hospitals { get; set; }

        public ICollection<Doctor>? Doctors { get; set; }

        public ICollection<Policlinic>? Policlinics { get; set; }

        public ICollection<Appointment>  ?Appointments { get; set; }

    }
}
