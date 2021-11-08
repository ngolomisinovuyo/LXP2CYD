using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Models.AddressModels;
using LPX2YCDProject2020.Models.ContactUs;
using LPX2YCDProject2020.Models.HomeModels;
using LPX2YCDProject2020.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IAddressRepository _addressRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, IUserService userService, IEmailService emailService, IAddressRepository addressRepository, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _userService = userService;
            _emailService = emailService;
            _addressRepository = addressRepository;
            _context = context;
        }

        public IActionResult Home()
        {
            HomePageViewModel viewModel = new HomePageViewModel();

            var allUsers = _userManager.Users.AsQueryable();

            viewModel.NoOfAccounts = (int)allUsers.Count();
            viewModel.programmes = _context.Programmes.ToList();
            return View(viewModel); 
        }

        public IActionResult AboutUs()
        {
            HomePageViewModel viewModel = new HomePageViewModel();
            var allUsers = _userManager.Users.AsQueryable();

            viewModel.NoOfAccounts = (int)allUsers.Count();
            viewModel.programmes = _context.Programmes.ToList();
            return View(viewModel);
        }

        [HttpGet]
        //Get Method for contact us form
        public async Task<IActionResult> ContactUs(string messages) 
        {
            ViewBag.message = messages;

            ContactUsModel model = new ContactUsModel();
            //string centerName; 
            model.systemDetails = await _context.CenterDetails.Include(c => c.Suburb)
                .ThenInclude(v => v.City)
                .ThenInclude(z => z.Province)
                .ToListAsync();

            return View(model);
        }

        //Post Method for contact us form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEnquiry(ContactUsFormModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Enquiries.Add(model);
                await _context.SaveChangesAsync();
                string message = "Thank you for reaching out.. We will be in touch with you soon.";
                return RedirectToAction(nameof(ContactUs), new { messages = message });
            }
            return View(model);
        }

        public IActionResult Programmes() => 
            View(_context.Programmes.ToList());
    }
}
