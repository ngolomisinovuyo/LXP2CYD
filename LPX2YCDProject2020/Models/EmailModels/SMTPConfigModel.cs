using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Models.EmailModels
{
    public class SMTPConfigModel
    {
        public string SenderAddress { get; set; }  //  no-reply @LPX2.com",

        public string SenderDisplayName { get; set; }//LPX management team",
        public string UserName { get; set; }//f4e7b6f9b00c43",

        public string Password { get; set; } //5930de6e7bd705",
        public string Host { get; set; }//smtp.mailtrap.io",
        public int Port { get; set; }//": 587,
        public bool EnableSSL { get; set; }//": true,
        public bool UseDefaultCredentials { get; set; }//": true,
        public bool IsBodyHTML { get; set; }//": true


    }
}
