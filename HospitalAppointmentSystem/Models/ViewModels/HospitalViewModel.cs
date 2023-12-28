using HospitalAppointmentSystem.Models.Entities;

namespace HospitalAppointmentSystem.Models.ViewModels
{
    public class HospitalViewModel
    {
        public int HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public string Province { get; set; }
        public string District { get; set; }

    }
}
