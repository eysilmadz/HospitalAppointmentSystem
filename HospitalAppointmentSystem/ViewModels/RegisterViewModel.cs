using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? UserIdentityNo { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? UserSurname { get; set; }

        [Required]
        public string? UserGender { get; set; }

        [Required]
        public string? UserBirthday { get; set; }

        [Required]
        public string? UserMail { get; set; }

        [Required]
        public string? UserPassword { get; set; }

        [Required]
        [Compare("UserPassword",ErrorMessage ="Şifreler uyuşmuyor")]
        public string? ConfirmPassword { get; set; }
    }
}
