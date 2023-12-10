using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models.Entities
{
    public class Policlinic
    {
        [Key]
        public int PoliclinicId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? PoliclinicName { get; set; }

        public ICollection<HospitalPoliclinic> ?Hospitals { get; set; }

        public ICollection<Doctor>? Doctors { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }

        [ForeignKey("MajorDepartment")]
        public int MajorDepartmentId { get; set; }
        public MajorDepartment? MajorDepartment { get; set; }
    }
}
