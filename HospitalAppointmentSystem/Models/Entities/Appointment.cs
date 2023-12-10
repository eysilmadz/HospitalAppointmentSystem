using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models.Entities
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        public DateTime AppointmentTime { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        [ForeignKey("Policlinic")]
        public int PoliclinicId { get; set; }
        public Policlinic? Policlinic { get; set; }

        [ForeignKey("MajorDepartment")]
        public int MajorDepartmentId { get; set; }
        public MajorDepartment? MajorDepartment { get; set; }

        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

    }
}
