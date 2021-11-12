using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LPX2YCDProject2020.Areas.Coordinator.Controllers
{
    [Area("Coordinator")]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        public AppointmentsController(ApplicationDbContext context, IUserService userService)
        {
            _userService = userService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userService.GetUserId();

            var appts = await _context.Appointment.Include(c => c.appointmentTypes).
                 Where(a => a.userId == userId)
                 .ToListAsync();
            

            return View(appts);
        }
        [HttpGet]
        public IActionResult CreateAppointment()
        {
            ViewBag.AppointmentTypes = new SelectList(GetAppointmentTypeAsync(), "Id", "Description");
            return View();
        }

    }
}