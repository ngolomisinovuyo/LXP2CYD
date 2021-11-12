using System;
namespace LPX2YCDProject2020.Models.Appointments
{
    public class Appointment
    {
        public int Id { get; set; }
        public string HostId { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Notes { get; set; }
        public bool IsVirtual { get; set; }
        public string MeetingLink { get; set; }
        public int? AddressId { get; set; }
        public string Location { get; set; }
    }
}
