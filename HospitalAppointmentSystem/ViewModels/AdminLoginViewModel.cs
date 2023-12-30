using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı alanı boş olamaz.")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş olamaz.")]
        public string? Password { get; set; }
    }
}
