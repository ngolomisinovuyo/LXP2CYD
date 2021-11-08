using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.PortalModels
{
    public class BursaryCourses
    {
        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public int BursaryId { get; set; }
        public Bursary Bursary { get; set; }

       
    }
}
