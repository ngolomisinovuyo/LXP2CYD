using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Models.AddressModels;
using LPX2YCDProject2020.Models.AdminModels;
using LPX2YCDProject2020.Models.EmailModels;
using LPX2YCDProject2020.Models.HomeModels;
using LPX2YCDProject2020.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rotativa.AspNetCore;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace LPX2YCDProject2020.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountRepository _accRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IAddressRepository _addressRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IConfiguration _config;
        IEmailService _emailService;

        public AdminController(IEmailService emailService, IConfiguration config, ICompositeViewEngine viewEngine, IUserService userService, IAccountRepository accRepository, ApplicationDbContext context, IAddressRepository addressRepository, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _emailService = emailService;
            _config = config;
            _viewEngine = viewEngine;
            _userService = userService;
            _userManager = userManager;
            _accRepository = accRepository;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _addressRepository = addressRepository;
            _context = context;
        }


        [HttpGet]
        public IActionResult ProgrammeAttendees(int Id, bool IsSuccess)
        {
            if (Id == 0)
                RedirectToAction("ErrorPage", "Admin");

            ProgrammeAttendees pa = new ProgrammeAttendees();

            pa.Programmes = _context.Programmes
                .Include(a => a.Rsvps)
                .ThenInclude(i => i.User)
                .FirstOrDefault(z => z.Id == Id);

            pa.er = from cv in _context.EventReservations
                    where cv.ProgramId == Id
                    select cv;

            pa.Users = _userManager.Users;
            
            return View(pa);
        }

        [HttpPost]
        public async Task<IActionResult> EditEnrolment(int ReservationId)
        {
            if (ReservationId == 0)
                RedirectToAction(nameof(ErrorPage));

            var results = _context.EventReservations
                .FirstOrDefault(o => o.ReservationId == ReservationId);

            EventReservations attendee = new EventReservations()
            {
                Feedback = results.Feedback,
                ReservationId = results.ReservationId,
                ProgramId = results.ProgramId,
                UserId = results.UserId,
                attended = true,
                Enrolled = results.Enrolled
            };

            _context.EventReservations.Update(attendee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ProgrammeAttendees), new { Id = attendee.ProgramId, IsSuccess = true });
        }
        
        //Certificate methods
        public IActionResult CertificateOfParticipation(int id)
        {

            if (id == 0)
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });

            CertificateViewModel model = new CertificateViewModel();

            model.centerDetails = _context.CenterDetails.Include(c => c.Suburb)
               .ThenInclude(v => v.City)
               .ThenInclude(z => z.Province)
              .SingleOrDefault();

            var userId = _userService.GetUserId();

            model.learner = _context.StudentProfiles.SingleOrDefault(w => w.UserId == userId);

            model.programme = _context.Programmes.SingleOrDefault(f => f.Id == id);

            CertificatePdf(model);
            return View(model);
        }

        public IActionResult CertificatePdf(CertificateViewModel model)
        {
            return View(model);
        }

        public IActionResult PrintCertificate(int id)
        {
            CertificateViewModel data = new CertificateViewModel();
            var userId = _userService.GetUserId();

            data.programme = _context.Programmes.SingleOrDefault(q => q.Id == id);
            data.learner = _context.StudentProfiles.SingleOrDefault(v => v.UserId == userId);
            data.centerDetails = _context.CenterDetails.Include(c => c.Suburb)
               .ThenInclude(v => v.City)
               .ThenInclude(z => z.Province)
               .SingleOrDefault();

            return new ViewAsPdf("CertificatePdf", data);
        }

        //Deregister for a programme
        public async Task<IActionResult> Deregister(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });
            }

            var record = await _context.EventReservations.SingleOrDefaultAsync(c => c.ReservationId == id);

            if (record == null)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });
            }

            try
            {
                _context.EventReservations.Remove(record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewPrograms), new { isSuccess = true });
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });
            }
        }

        public IActionResult PostFeedback(int id)
        {
            StudentProgramViewModel results = new StudentProgramViewModel();
            results.Programmes = _context.Programmes.SingleOrDefault(c => c.Id == id);

            var userId = _userService.GetUserId();

            results.rsvp = (from c in _context.EventReservations
                            where c.UserId == userId &&
                            c.ProgramId == id
                            select c)
                           .FirstOrDefault();

            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> PostFeedback(StudentProgramViewModel model)
        {
            var result = await _context.EventReservations.SingleOrDefaultAsync(v => v.ReservationId == model.rsvp.ReservationId);

            if (result == null)
                return RedirectToAction(nameof(ErrorPage));

            var record = new EventReservations()
            {
                ReservationId = model.rsvp.ReservationId,
                attended = result.attended,
                Feedback = model.rsvp.Feedback,
                ProgramId = model.rsvp.ProgramId,
                UserId = model.rsvp.UserId,
            };

            try
            {
                _context.EventReservations.Update(record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PostFeedback));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Enroll(int Id, string UserId)
        {
            if (Id == 0 || UserId == null)
                return RedirectToAction(nameof(ViewPrograms), new { isSuccess = false });

            var model = new EventReservations()
            {
                ProgramId = Id,
                UserId = UserId,

            };

            try
            {

                //var enrolled = _context.EventReservations.Where(v => v.ProgramId == Id).ToList();
                var enrolled = (from cv in _context.EventReservations
                               where cv.UserId == UserId &&
                               cv.ProgramId == Id
                               select cv).SingleOrDefault();
 
                //if (enrolled.Count() > 0)
                if(enrolled != null )
                {
                    return RedirectToAction(nameof(ViewPrograms), new { message = "You are already enrolled for this programme" });
                }
                model.Enrolled = true;
                _context.EventReservations.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewPrograms), new { isSuccess = true });
            }
            catch (Exception c)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = "The resource you are trying to access is currently unavailable" });
            }

        }

        public IActionResult ViewPrograms(string message, bool isSuccess)
        {
            StudentProgramViewModel model = new StudentProgramViewModel();
            var id = _userService.GetUserId();

            try
            {
                model.programmes = _context.Programmes
                     .Include(p => p.Rsvps)
                     .OrderBy(c => c.StartDate)
                     .ToList();
                model.eventRsvps = _context.EventReservations
                    .Where(c => c.UserId == id)
                    .ToList();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = e.ToString() });
            }

            ViewBag.error = message;
            ViewBag.isSuccess = isSuccess;

            return View(model);
        }

        public IActionResult Appointments()
        {
            return View(_context.Appointment.ToList());
        }

        //<------Error page ------>
        public IActionResult ErrorPage(string message) => View();

        //<-----Start of Province action methods ------>
        public IActionResult ViewProvinces() => View(_context.Provinces.ToList());

        [HttpGet]
        public IActionResult AddProvinces() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProvinces(Province model)
        {
            try
            {
                Province result = _context.Provinces.FirstOrDefault(a => a.ProvinceName == model.ProvinceName);

                if (result != null)
                {
                    ModelState.AddModelError("", "Province already exists");
                    return View(model);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        model.Country = "South Africa";
                        _context.Add(model);
                        await _context.SaveChangesAsync();
                        ModelState.Clear();
                        return RedirectToAction(nameof(ViewProvinces));
                    }
                }
                return View(model);
            }
            catch (Exception c)
            {
                return RedirectToAction(nameof(ErrorPage), new { message = c });
            }
        }

        public async Task<IActionResult> EditProvince(int? provinceId)
        {
            if (provinceId == 0)
            {
                return NotFound();
            }

            Province province = await _context.Provinces.SingleOrDefaultAsync(c => c.ProvinceId == provinceId);

            if (province == null)
                return NotFound();

            return View(province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProvince(Province model)
        {
            if (ModelState.IsValid)
            {
                var province = await _context.Provinces.SingleOrDefaultAsync(c => c.ProvinceId == model.ProvinceId);

                if (province == null)
                    return NotFound();
                else
                {
                    province.ProvinceName = model.ProvinceName;
                    province.Country = "South Africa";
                    _context.Update(province);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ViewProvinces));
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProvince(int? provinceId)
        {
            if (provinceId == 0)
                return NotFound();
            var province = await _context.Provinces.FirstOrDefaultAsync(i => i.ProvinceId == provinceId);

            _context.Remove(province);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewProvinces));
        }

        [HttpGet]
        public async Task<IActionResult> GetCitiesByProvince(int? provinceId)
        {

            if (provinceId == 0)
            {
                return NotFound();
            }

            var viewModel = new CascadingAddressClass();

            viewModel.Province = await _context.Provinces.FirstOrDefaultAsync(i => i.ProvinceId == provinceId);
            //viewModel.Province = (Province)_context.Provinces.Where(c => c.ProvinceId == provinceId);
            viewModel.cities = _context.Cities.Where(x => x.provinceId == provinceId).ToList();

            return View(viewModel);
        }

        //<-----End of Province action methods ------>

        //<-----start of City action methods ------>
        public async Task<IActionResult> AddCities(int? id)
        {
            if (id == 0)
                return NotFound();

            var ViewModel = new CascadingAddressClass();

            ViewModel.Province = await _context.Provinces.FirstOrDefaultAsync(i => i.ProvinceId == id);

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCities(CascadingAddressClass model)
        {
            City result = _context.Cities.FirstOrDefault(a => a.CityName == model.City.CityName);
            int id = model.Province.ProvinceId;

            if (result != null)
            {
                ModelState.AddModelError("", "City already exists");
                return View(model);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Cities.Add(model.City);
                    await _context.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction(nameof(GetCitiesByProvince), new { provinceId = id });
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditCity(int? cityId)
        {
            if (cityId == 0)
            {
                return NotFound();
            }

            City city = await _context.Cities.SingleOrDefaultAsync(c => c.CityId == cityId);

            if (city == null)
                return NotFound();

            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCity(City model)
        {
            if (ModelState.IsValid)
            {
                var city = await _context.Cities.SingleOrDefaultAsync(c => c.CityId == model.CityId);

                if (city == null)
                    return NotFound();
                else
                {
                    city.CityName = model.CityName;
                    city.provinceId = model.provinceId;
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GetCitiesByProvince), new { provinceId = model.provinceId });
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCity(int? cityId)
        {
            if (cityId == 0)
                return NotFound();
            var city = await _context.Cities.FirstOrDefaultAsync(i => i.CityId == cityId);
            var provinceId = city.provinceId;
            _context.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetCitiesByProvince), new { provinceId = provinceId });
        }

        [HttpGet]
        public async Task<IActionResult> GetSuburbsByCity(int? cityId)
        {
            if (cityId == 0)
            {
                return NotFound();
            }

            var viewModel = new CascadingAddressClass();

            viewModel.City = await _context.Cities.FirstOrDefaultAsync(i => i.CityId == cityId);
            //viewModel.Province = (Province)_context.Provinces.Where(c => c.ProvinceId == provinceId);
            viewModel.suburbs = _context.Suburbs.Where(x => x.CityId == cityId).ToList();

            return View(viewModel);
        }
        //<-----End of City action methods ------>

        //<-----start of Suburb action methods ------>
        public async Task<IActionResult> AddSuburb(int? id)
        {
            if (id == 0)
                return NotFound();

            var ViewModel = new CascadingAddressClass();

            ViewModel.City = await _context.Cities.FirstOrDefaultAsync(i => i.CityId == id);

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSuburb(CascadingAddressClass model)
        {
            Suburb result = _context.Suburbs.FirstOrDefault(a => a.SuburbName == model.Suburb.SuburbName);

            if (result != null)
            {
                ModelState.AddModelError("", "Suburb already exists");
                return View(model);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    model.Suburb.CityId = model.City.CityId;
                    _context.Suburbs.Add(model.Suburb);
                    await _context.SaveChangesAsync();
                    ModelState.Clear();
                    return RedirectToAction(nameof(GetSuburbsByCity), new { cityId = model.Suburb.CityId });
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditSuburb(int? suburbId)
        {
            if (suburbId == 0)
            {
                return NotFound();
            }

            Suburb suburb = await _context.Suburbs.SingleOrDefaultAsync(c => c.SuburbId == suburbId);

            if (suburb == null)
                return NotFound();

            return View(suburb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSuburb(Suburb model)
        {
            if (ModelState.IsValid)
            {
                var suburb = await _context.Suburbs.SingleOrDefaultAsync(c => c.SuburbId == model.SuburbId);

                if (suburb == null)
                    return NotFound();

                else
                {
                    suburb.SuburbName = model.SuburbName;
                    suburb.PostalCode = model.PostalCode;
                    suburb.CityId = model.CityId;
                    _context.Update(suburb);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GetSuburbsByCity), new { cityId = model.CityId });
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSuburb(int? suburbId)
        {
            if (suburbId == 0)
                return NotFound();
            var suburb = await _context.Suburbs.FirstOrDefaultAsync(i => i.SuburbId == suburbId);
            var cityId = suburb.CityId;
            _context.Remove(suburb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetSuburbsByCity), new { cityId = cityId });
        }
        //<-----End of suburb action methods ------>

        //<-----About us page action methods------>
        public IActionResult AddAboutInfo()
        {
            ViewBag.ProvinceList = new SelectList(_addressRepository.GetProvinceListAsync(), "ProvinceId", "ProvinceName");
            var details = _context.CenterDetails.ToList();

            Models.HomeModels.CenterDetails model = new Models.HomeModels.CenterDetails();
            foreach (var a in details)
            {
                model.ProfilePhoto = a.ProfilePhoto;
                model.ImageUrl = a.ImageUrl;
                model.AddressLine1 = a.AddressLine1;
                model.AddressLine2 = a.AddressLine2;
                model.BusinessName = a.BusinessName;
                model.CenterId = a.CenterId;
                model.ContactNumber = a.ContactNumber;
                model.EmailAddress = a.EmailAddress;
                model.Saved = false;
                model.SuburbId = a.SuburbId;
                model.Url = a.Url;
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAboutInfo(Models.HomeModels.CenterDetails model)
        {
            var results = _context.CenterDetails.FirstOrDefault();
            if (results == null)
            {
                return RedirectToAction(nameof(ErrorPage));
            }
            else
            {
                if (model.ProfilePhoto != null)
                {
                    string folder = "Images/ProfilePhotos/";
                    folder += Guid.NewGuid().ToString() + "_" + model.ProfilePhoto.FileName;
                    model.ImageUrl = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await model.ProfilePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                results.CenterId = model.CenterId;
                results.ImageUrl = model.ImageUrl;
                results.ProfilePhoto = model.ProfilePhoto;
                results.AddressLine1 = model.AddressLine1;
                results.AddressLine2 = model.AddressLine2;
                results.EmailAddress = model.EmailAddress;
                results.BusinessName = model.BusinessName;
                results.ContactNumber = model.ContactNumber;
                results.SuburbId = model.SuburbId;
                results.Url = model.Url;

                try
                {
                    _context.CenterDetails.Update(results);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return RedirectToAction(nameof(ErrorPage), new { message = e.Message });
                }
                model.Saved = true;
                ViewBag.ProvinceList = new SelectList(_addressRepository.GetProvinceListAsync(), "ProvinceId", "ProvinceName");
                return RedirectToAction(nameof(AddAboutInfo));

            }
        }
        //<-----End About us action methods------->

        public JsonResult GetCityList(int ProvinceId)
        {
            var cityList = _context.Cities.Where(p => p.provinceId == ProvinceId).ToList();
            return Json(new SelectList(cityList, "CityId", "CityName"));
        }
        public JsonResult GetSuburbList(int CityId)
        {
            var suburbList = _context.Suburbs.Where(p => p.CityId == CityId).ToList();

            return Json(new SelectList(suburbList, "SuburbId", "SuburbName"));
        }

        public JsonResult GetSuburbPCode(int SuburbId)
        {
            var code = _context.Suburbs.Where(p => p.SuburbId == SuburbId).ToList();
            return Json(new SelectList(code, "SuburbId", "PostalCode"));
        }

        //Admin Program methods
        public IActionResult CreateProgram() => View();


        [HttpPost]
        public async Task<IActionResult> CreateProgram(Programme model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Programmes.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ListAllPrograms), new { IsSuccess = true });
                }
                catch (Exception c)
                {
                    return RedirectToAction("ErrorPage", "Admin");
                }
            }
            return View(model);
        }

        public IActionResult ListAllPrograms(bool IsSuccess)
        {
            ViewBag.IsSuccess = IsSuccess;
            var AllPrograms = _context.Programmes.ToList();
            return View(AllPrograms);
        }

        public IActionResult EditProgram(int id)
        {
            if (id == 0)
                return RedirectToAction(nameof(ErrorPage));

            var results = _context.Programmes.FirstOrDefault(v => v.Id == id);
            if (results == null)
                return RedirectToAction(nameof(ErrorPage));

            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> EditProgram(Programme model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Programmes.Update(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ListAllPrograms), new { IsSuccess = true });
                }
                catch (Exception c)
                {
                    RedirectToAction(nameof(ErrorPage));
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            if (id == 0)
                return RedirectToAction(nameof(ErrorPage));

            var results = _context.Programmes.FirstOrDefault(i => i.Id == id);
            if (results == null)
                return RedirectToAction(nameof(ErrorPage));

            try
            {
                _context.Programmes.Remove(results);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListAllPrograms), new { IsSuccess = true });
            }
            catch (Exception c)
            {
                RedirectToAction(nameof(ErrorPage));
            }

            return RedirectToAction(nameof(ListAllPrograms));
        }

        public IActionResult EndUserFeedback(string message)
        {
            ViewBag.Message = message;
            var results = _context.Enquiries.ToList();
            return View(results);
        }

        public IActionResult SendUserFeedback(int id, string message)
        {
            if (id == 0)
                return RedirectToAction(nameof(ErrorPage));

            var model = _context.Enquiries.FirstOrDefault(w => w.Id == id);

            if (model == null)
                return RedirectToAction(nameof(ErrorPage));
            ViewBag.message = message;
            EmailEnquiryResponse response = new EmailEnquiryResponse
            {
                Name = model.FirstName + " " + model.LastName,
                userEmail = model.EmailAddress
            };
            return View(response);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SendUserFeedback(EmailEnquiryResponse model)
        {

            if (ModelState.IsValid)
            {
                var results = SendResponseEmail(model);
                string message = "Response has been sent Successfully";
                return RedirectToAction(nameof(EndUserFeedback), new { message = message });
            }
            return View(model);
        }




        private async Task SendResponseEmail(EmailEnquiryResponse model)
        {
            string appDomain = _config.GetSection("Application:AppDomain").Value;
            string confirmationLink = _config.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string> { model.userEmail },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", model.Name),
                     new KeyValuePair<string, string>("{{message}}", model.body),
                }
            };
            await _emailService.SendEqnuiryResponseEmail(options);
        }
    }
}
