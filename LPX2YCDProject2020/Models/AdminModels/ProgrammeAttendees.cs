using LPX2YCDProject2020.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.AdminModels
{
    public class ProgrammeAttendees
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public Programme Programmes { get; set; }
        public IEnumerable<Programme> programmes { get; set; }

        public IEnumerable<EventReservations> er { get; set; }

        public EventReservations ER { get; set; }

        [Key]
        public int ReservationId { get; set; }

        [Required]
        public int ProgramId { get; set; }
        public Programme programme { get; set; }

        [Required]
        public string UserId { get; set; }
        public StudentProfileModel User { get; set; }

        public string Feedback { get; set; }

        public bool attended { get; set; }

        public bool Enrolled { get; set; }

    }
}
