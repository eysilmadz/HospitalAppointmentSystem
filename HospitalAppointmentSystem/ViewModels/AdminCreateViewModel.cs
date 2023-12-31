using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.ViewModels
{
    public class AdminCreateViewModel
    {
        public string? Fullname { get; set; }

        [Required]
        public string? UserName { get; set; }= string.Empty;

        [Required]
        public string? UserSurname { get; set; } = string.Empty;

        [Required]
        public string? UserIdentityNo { get; set; } = string.Empty;

        [Required]
        public string? UserGender { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(\+[0-9]{1,3}[-\.\s]?)?(\([0-9]{1,5}\)[-.\s]?)?([0-9]{1,4}[-.\s]?){5,}$", ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string? PhoneNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public string? UserBirthday { get; set; }

        [Required]
        [EmailAddress]
        public string? EmailAddress { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string? Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parola eşleşmiyor.")]
        public string? ConfirmPassword { get; set; } = string.Empty;

        public string? Role { get; set; }

        public IEnumerable<SelectListItem>? RolesList { get; set; }
    }
}
