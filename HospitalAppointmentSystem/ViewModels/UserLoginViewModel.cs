using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "T.C. Kimlik alanı boş olamaz.")]
        public string? TcKimlikNo { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş olamaz.")]
        public string? Password { get; set; }
    }
}
