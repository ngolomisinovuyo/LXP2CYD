using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Models.Appointments;
using LPX2YCDProject2020.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public AppointmentController(ApplicationDbContext context, IUserService userService)
        {
            _userService = userService;
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateAppointment()
        {
            ViewBag.AppointmentTypes = new SelectList(GetAppointmentTypeAsync(), "Id", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAppointment(UserAppointments model)
        {
            var userId = _userService.GetUserId();
            var outPut = _context.Users.FirstOrDefault(w => w.Id == userId);

            if (outPut == null)
                return RedirectToAction("ErrorPage", "Admin");

            if (ModelState.IsValid)
            {
                model.FullName = outPut?.FirstName + " " + outPut?.LastName;
                model.Status = "Pending";
                try
                {
                    _context.Appointment.Add(model);
                    await _context.SaveChangesAsync();
                }
                catch (Exception c)
                {
                    return RedirectToAction("ErrorPage", "Admin");
                }

                return RedirectToAction(nameof(ViewAppointments), new { IsSuccess = true });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAppointment(int Id)
        {
            if (Id == 0)
                return RedirectToAction("ErrorPage", "Admin");

            var appointment = await _context.Appointment.FindAsync(Id);

            if (appointment == null)
                return RedirectToAction("ErrorPage", "Admin");

            ViewBag.AppointmentTypes = new SelectList(GetAppointmentTypeAsync(), "Id", "Description");
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAppointment(UserAppointments model)
        {
            var appointmet = await _context.Appointment.FindAsync(model.Id);
            if (appointmet == null)
                return RedirectToAction("ErrorPage", "Admin");

            if (ModelState.IsValid)
            {
                appointmet.AppointmentTypeId = model.AppointmentTypeId;
                appointmet.assignedEmployee = model.assignedEmployee;
                appointmet.CenterNotes = model.CenterNotes;
                appointmet.Date = model.Date;
                appointmet.Message = model.Message;
                appointmet.Status = model.Status;
                appointmet.Time = model.Time;
                appointmet.userId = model.userId;

                try
                {
                    _context.Appointment.Update(appointmet);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return RedirectToAction("ErrorPage", "Admin");
                }
                return RedirectToAction(nameof(ViewAppointments), new { IsSuccess = true });
            }

            ViewBag.AppointmentTypes = new SelectList(GetAppointmentTypeAsync(), "Id", "Description");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CancellAppointments(int Id)
        {
            if (Id == 0)
                return RedirectToAction("ErrorPage", "Admin");

            var appointment = await _context.Appointment.SingleOrDefaultAsync(c => c.Id == Id);

            if (appointment == null)
                return RedirectToAction("ErrorPage", "Admin");

            try
            {
                appointment.Status = "Cancelled";
                _context.Appointment.Update(appointment);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorPage", "Admin");
            }
            return RedirectToAction(nameof(ViewAppointments), new { IsSuccess = true });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewAppointments(bool IsSuccess)
        {
            var userId = _userService.GetUserId();

            var appts = await _context.Appointment.Include(c => c.appointmentTypes).
                 Where(a => a.userId == userId)
                 .ToListAsync();
            ViewBag.IsSuccess = IsSuccess;

            return View(appts);
        }

        public List<AppointmentType> GetAppointmentTypeAsync()
        {
            List<AppointmentType> appointments = _context.AppointmentType.ToList();
            return appointments;
        }
    }
}
