using LPX2YCDProject2020.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.PortalModels
{
    public class RequiredSubjects
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Bursary))]
        [Display(Name = "Bursary")]
        public int BursaryId { get; set; }
        Bursary Bursary { get; set; }

        [ForeignKey(nameof(SubjectDetails))]
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }
        public SubjectDetails SubjectDetails { get; set; }

        [Required]
        [Display(Name = "Percentage")]
        public int Percentage { get; set; }
    }
}
