using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.Account
{
    public class ProLiaisonProfileViewModel
    {
        public ExternalManagement Management { get; set; }
        public ApplicationUser UserProfile { get; set; }

        public IEnumerable<ExternalManagement> management { get; set; }
        public IEnumerable<ApplicationUser> userProfile { get; set; }
    }
}
