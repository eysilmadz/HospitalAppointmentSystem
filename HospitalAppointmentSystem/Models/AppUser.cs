using Microsoft.AspNetCore.Identity;

namespace HospitalAppointmentSystem.Models
{
    public class AppUser:IdentityUser
    {
        public string? FullName { get; set; }
        //Farklı bilgiler de mesela kayıt tarihi gibi eklenebilir
    }
}
