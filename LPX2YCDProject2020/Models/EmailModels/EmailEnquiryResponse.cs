using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.EmailModels
{
    public class EmailEnquiryResponse
    {
        public int Id { get; set; }

        [Display(Name = "Recipient")]
        public string userEmail { get; set; }

        [Display(Name = "Message")]
        public string body { get; set; }
        public string Subject { get; set; }

        public string Name { get; set; }
    }
}
