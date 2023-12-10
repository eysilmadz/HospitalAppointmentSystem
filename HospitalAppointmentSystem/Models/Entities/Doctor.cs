using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models.Entities
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? DoctorName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? DoctorSurname { get; set; }

        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        [ForeignKey("Policlinic")]
        public int PoliclinicId { get; set; }
        public Policlinic? Policlinic { get; set; }

        [ForeignKey("MajorDepartment")]
        public int MajorDepartmentId { get; set; }
        public MajorDepartment? MajorDepartment { get; set; }

        public ICollection<Appointment> ?Appointments { get; set; }
    }
}
