using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models.Entities
{
    public class HospitalMajorDepartment
    {
        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        [ForeignKey("MajorDepartment")]
        public int MajorDepartmentId { get; set; }
        public MajorDepartment? MajorDepartment { get; set; }
    }
}
