using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.PortalModels
{
    public class RegCenters
    {
        private string CenterID;
        public string centerID
        {
            get { return centerID; }
            set { centerID = " "; }
        }
        private string CenterName;
        private string name
        {
            get { return name; }
            set { name = " "; }
        }
        private string Region;
        public string region
        {
            get { return region; }
            set { region = " "; }
        
        }
         
       public string CenterRegion
        { get; set; }


    }
}
