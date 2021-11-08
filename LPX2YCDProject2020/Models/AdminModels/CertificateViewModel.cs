using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Models.HomeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.AdminModels
{
    public class CertificateViewModel
    {
        public StudentProfileModel learner { get; set; }
        public CenterDetails centerDetails { get; set; }

        public Programme programme { get; set; } 
    }
}
