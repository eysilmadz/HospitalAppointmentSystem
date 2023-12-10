using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models.Entities
{
    public class District
    {
        [Key]
        public int DistrictId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? DistrictName { get; set; }

        [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public Province? Province { get; set; }

        public ICollection<Hospital> ?Hospitals { get; set; }
    }
}
