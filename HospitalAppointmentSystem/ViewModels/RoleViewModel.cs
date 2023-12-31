using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.ViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Lütfen rol adı giriniz")]
        public string? RoleName { get; set; }   
    }
}
