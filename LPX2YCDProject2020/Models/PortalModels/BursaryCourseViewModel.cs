using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.PortalModels
{
    public class BursaryCourseViewModel
    {
        public BursaryCourses BursaryCourses { get; set; }
        public Course Course { get; set; }
        public Bursary Bursary { get; set; }

        public IEnumerable<BursaryCourses> bursaryCourses { get; set; }
        public IEnumerable<Course> course { get; set; }
        public IEnumerable<Bursary> bursary { get; set; }
    }
}
