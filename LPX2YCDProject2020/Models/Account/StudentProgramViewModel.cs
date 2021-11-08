using LPX2YCDProject2020.Models.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class StudentProgramViewModel
    {
        //public int Id { get; set; }
        public IEnumerable<Programme> programmes { get; set; }
        public IEnumerable<EventReservations> eventRsvps { get; set; }
        public EventReservations rsvp { get; set; }
        public Programme Programmes { get; set; }
    }
}
