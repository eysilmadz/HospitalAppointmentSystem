using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.Models.Entities
{
    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ProvinceName { get; set; }

        public ICollection<District> ?Districts { get; set; }
    }
}
