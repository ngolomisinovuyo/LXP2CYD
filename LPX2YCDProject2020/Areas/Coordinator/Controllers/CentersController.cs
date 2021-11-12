using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LPX2YCDProject2020.Areas.Coordinator.Controllers
{
    [Area("Coordinator")]
    public class CentersController : Controller
    {
        public CentersController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}