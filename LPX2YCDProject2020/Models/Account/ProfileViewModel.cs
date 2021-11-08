using LPX2YCDProject2020.Models.AddressModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class ProfileViewModel
    {
       
        public string UserId { get; set; }
        public StudentProfileModel Student { get; set; }


        public int SubjectId { get; set; }
        public SubjectDetails Subjects { get; set; }

        [Display(Name = "Tell us more")]
        public string Comments { get; set; }

        [Display(Name = "Term 1 Mark")]
        public string FirstTermMark { get; set; }

        [Display(Name = "Term 2 Mark")]
        public string SecondTermMark { get; set; }

        [Display(Name = "Term 3 Mark")]
        public string ThirdTermMark { get; set; }

        [Display(Name = "Target final mark")]
        public string Target { get; set; }


        public ICollection<StudentSubjects> Enrolments { get; set; } /*= new HashSet<StudentSubjects>();*/

        [Display(Name = "Suburb")]
        public int SuburbId { get; set; }
        public Suburb suburb { get; set; }

        [Display(Name = "Street address")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Building/Complex")]
        public string AddressLine2 { get; set; }

        [Display(Name = "School name")]
        public string NameOfSchool { get; set; }

        [Display(Name = "Grade")]
        public string Grade { get; set; }

        //public object SubjectDetails { get; }

        [Display(Name = "Subject")]
        public int subjectId { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        [Display(Name = "Province")]
        public int ProvinceId { get; set; }
    }
}
