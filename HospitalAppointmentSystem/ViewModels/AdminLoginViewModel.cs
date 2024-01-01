using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "Email alanı boş olamaz.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş olamaz.")]
        public string? Password { get; set; }
    }
}
