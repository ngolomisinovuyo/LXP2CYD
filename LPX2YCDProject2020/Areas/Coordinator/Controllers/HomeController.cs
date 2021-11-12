using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Models.Appointments;
using LPX2YCDProject2020.Services;

namespace LPX2YCDProject2020.Areas.Coordinator.Controllers
{
    [Area("Coordinator")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
