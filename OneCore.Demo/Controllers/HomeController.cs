using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneCore.Demo.Extensions;
using OneCore.Demo.Models;

namespace OneCore.Demo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.Select(
                q => new UserViewModel()
                {
                    Id = q.Id,
                    UserName = q.UserName,
                    Email = q.Email,
                    Gender = q.GetGender(),
                    Status = q.GetStatus()
                })
                .ToListAsync());
        }
    }
}
