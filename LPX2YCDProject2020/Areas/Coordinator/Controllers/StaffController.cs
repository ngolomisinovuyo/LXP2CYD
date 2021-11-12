using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LPX2YCDProject2020.Areas.Coordinator.Dtos;
using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LPX2YCDProject2020.Areas.Coordinator.Controllers
{
    [Area("Coordinator")]
    public class StaffController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountRepository _accountRepository;
        public StaffController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, IAccountRepository accountRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _accountRepository = accountRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmplyeeInputDto input)
        {
            if(ModelState.IsValid)
            {
              
                var user = new ApplicationUser()
                {
                    Email = input.Email,
                    UserName = input.Email,
                    DateJoined = DateTime.Now.ToString("dd/MM/yyyy"),
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, input.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                    return View();
                }
                await _accountRepository.GenerateNewEmailTokenAsync(user);
                var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == input.RoleId);
                if (role == null)
                {
                    ModelState.AddModelError("", "RoleId is required");
                    return View(ModelState);
                }


                await _userManager.AddToRoleAsync(user, role.Name);
                return RedirectToAction("Index");
            }
            return View(ModelState);
        }
    }
}