using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
   
    public class StudentSubjects
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("StudentProfileModel")]
        [Required]
        public string UserId { get; set; }
        public virtual StudentProfileModel Student { get; set; }

        [Display(Name ="Year")]
        public string Year { get; set; }

        [ForeignKey("SubjectDetails")]
        [Required(ErrorMessage ="Please select")]
        public int SubjectId { get; set; }
        public virtual SubjectDetails Subjects { get; set; }

        [Display(Name = "Tell us more")]
        public string Comments { get; set; }

        [Display(Name = "Term 1 Mark")]
        public int FirstTermMark { get; set; }

        [Display(Name = "Term 2 Mark")]
        public int SecondTermMark { get; set; }

        [Display(Name = "Term 3 Mark")]
        public int ThirdTermMark { get; set; }

        [Display(Name = "Target final mark")]
        public int Target { get; set; }

    }
}
