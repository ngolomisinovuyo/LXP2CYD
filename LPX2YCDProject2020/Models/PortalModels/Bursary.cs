using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.PortalModels
{
    public class Bursary
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a description")]
        public string Description { get; set; }

        [Display(Name = "Opens")]
        public DateTime openingDate { get; set; }

        [Display(Name = "Closes")]
        public DateTime ClosingDate { get; set; }

        [Display(Name = "Required documents")]
        public string RequiredDocuments { get; set; }

        [Display(Name = "Company website")]
        public string Url { get; set; }

        [Display(Name = "Requirements")]
        public string MinimumRequirements { get; set; }

        [Display(Name = "Sponsored study fields")]
        public ICollection<BursaryCourses> SponsoredFields { get; set; }

        [Display(Name = "Required subjects")]
        public ICollection<RequiredSubjects> RequiredSubjects { get; set; }

        public bool Active { get; set; }
    }
}
