using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models.Entities
{
    public class Hospital
    {
        [Key]
        public int HospitalId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? HospitalName { get; set; }

        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District? District { get; set; }

        public ICollection<HospitalMajorDepartment> ?MajorDepartments { get; set; }

        public ICollection<Doctor> ?Doctors { get; set; }

        public ICollection<HospitalPoliclinic> ?Policlinics { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }

    }
}
