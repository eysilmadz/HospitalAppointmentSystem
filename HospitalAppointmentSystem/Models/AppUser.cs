using Microsoft.AspNetCore.Identity;

namespace HospitalAppointmentSystem.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }

        public string? Surname {get; set;}

        public string? IdentityNo { get; set; }

        public string? Gender { get; set; }

        public string? Birthday { get; set; }
    }
} 
