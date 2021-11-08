using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Models.PortalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.PortalModels
{
    public class MaterialListViewModel
    {
        public IEnumerable<SubjectResources> Material { get; set; }
        public IEnumerable<SubjectDetails> Subjects { get; set; }
    }
}
