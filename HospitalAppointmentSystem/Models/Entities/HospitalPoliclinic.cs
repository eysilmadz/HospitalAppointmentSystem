using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models.Entities
{
    public class HospitalPoliclinic
    {
        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        [ForeignKey("Policlinic")]
        public int PoliclinicId { get; set; }
        public Policlinic? Policlinic { get; set; }
    }
}
