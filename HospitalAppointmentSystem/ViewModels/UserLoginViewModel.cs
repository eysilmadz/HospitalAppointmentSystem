using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "E-Mail alanı boş olamaz.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş olamaz.")]
        public string? Password { get; set; }
    }
}
