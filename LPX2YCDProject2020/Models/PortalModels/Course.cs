using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.PortalModels
{
    public class Course
    {
        public int CourseId { get; set; }

        [Display(Name = "Qualification")]
        public string CourseName { get; set; }

        public ICollection<BursaryCourses> Bursaries { get; set; } = new HashSet<BursaryCourses>();
    }
}
