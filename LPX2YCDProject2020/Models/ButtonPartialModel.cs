using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models
{
    public class ButtonPartialModel
    {
        public string ButtonType { get; set; }
        public string Action { get; set; }
        public string Glyph { get; set; }
        public string Text { get; set; }

        public int? ServiceID;
        public string ActionParameters
        {
            get
            {
                var param = new StringBuilder(@"/");
                if (ServiceID != 0 && ServiceID != null)
                    param.Append(String.Format($"{ServiceID}"));

                return param.ToString().Substring(0, param.Length);
            }
        }
    }
}
