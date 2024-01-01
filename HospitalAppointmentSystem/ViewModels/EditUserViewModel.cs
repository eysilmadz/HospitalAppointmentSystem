using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.ViewModels
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }

        public string? FullName { get; set; }

        public string? UserName { get; set; }

        public string? UserSurname { get; set; } 

        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; } 

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parola eşleşmiyor.")]
        public string? ConfirmPassword { get; set; }

    }
}
