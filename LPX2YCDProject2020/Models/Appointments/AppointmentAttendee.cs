using System;
using System.ComponentModel.DataAnnotations.Schema;
using LPX2YCDProject2020.Models.Account;

namespace LPX2YCDProject2020.Models.Appointments
{
    public class AppointmentAttendee
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string AttendeeId { get; set; }
        public DateTime Time { get; set; }
        public DateTime ArrivedAt { get; set; }
        public bool Arrived { get; set; }
        [ForeignKey(nameof(AppointmentId))]
        public Appointment Appointment { get; set; }
        [ForeignKey(nameof(AttendeeId))]
        public ApplicationUser Attendee { get; set; }
    }
}
