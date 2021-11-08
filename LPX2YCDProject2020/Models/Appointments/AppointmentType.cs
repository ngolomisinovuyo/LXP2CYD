using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Appointments
{
    public class AppointmentType
    {
        public int Id { get; set; }

        public string  Description { get; set; }

        public IList<UserAppointments> appointments { get; set; }
    }
}
