using LPX2YCDProject2020.Models.AddressModels;
using LPX2YCDProject2020.Models.HomeModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.ContactUs
{
    public class ContactUsModel
    {
        public ContactUsFormModel ContactUsFormModel { get; set; }
        public  CenterDetails SystemDetailsModel { get; set; }

        public IEnumerable<ContactUsFormModel> contactUs { get; set; }
        public IEnumerable<CenterDetails> systemDetails { get; set; }
    }
}
