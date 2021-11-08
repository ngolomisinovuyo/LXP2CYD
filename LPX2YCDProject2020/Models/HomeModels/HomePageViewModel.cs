using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Models.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.HomeModels
{
    public class HomePageViewModel
    {
        public IEnumerable<StudentProfileModel> learners { get; set; }
        public IEnumerable<Programme> programmes { get; set; }
        public int NoOfAccounts { get; set; }

    }
}
