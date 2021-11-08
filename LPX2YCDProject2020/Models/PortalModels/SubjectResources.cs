using LPX2YCDProject2020.Models.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.PortalModels
{
    public class SubjectResources
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a title")]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Category")]
        public string Type { get; set; }

        [ForeignKey(nameof(Subject))]
        [Display(Name ="Subject ")]
        public int SubjectsId { get; set; }
        public SubjectDetails Subject { get; set; }

        [NotMapped]
        [Display(Name ="Upload Pdf")]
        public IFormFile Pdf { get; set; }

        public string PdfUrl { get; set; }

      
        public string Description { get; set; }
    }
}
