using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneCore.Demo.Extensions;
using OneCore.Demo.Models;
using OneCore.Demo.Models.ManageViewModels;

namespace OneCore.Demo.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;


        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _urlEncoder = urlEncoder;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{id}'.");
            }

            var model = new IndexViewModel
            {
                User = user.UserName,
                Email = user.Email,
                Gender = user.Gender,                
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{model.Id}'.");
            }

            if (user.Gender != model.Gender)
            {
                user.Gender = model.Gender;
                var setGenderResult = await _userManager.UpdateAsync(user);
                if (!setGenderResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting gender for user with ID '{user.Id}'.");
                }
            }

            var email = user.Email;
            if (model.Email != email)
            {   
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {                    
                    AddErrors(setEmailResult);
                    return View(model);
                }
            }

            var userName = user.UserName;
            if (model.User != userName)
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user, model.User);
                if (!setUserNameResult.Succeeded)
                {                    
                    AddErrors(setUserNameResult);
                    return View(model);
                }
            }
                        
            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                var passwordHash = user.PasswordHash;
                var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                if (!removePasswordResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred removing the password for user with ID '{user.Id}'.");                    
                }

                var setPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (!setPasswordResult.Succeeded)
                {
                    user.PasswordHash = passwordHash;
                    await _userManager.UpdateAsync(user);
                    AddErrors(setPasswordResult);
                    return View(model);
                }
            }

            StatusMessage = "El perfil ha sido actualizado.";
            return RedirectToAction(nameof(Index), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return View(new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Gender = user.GetGender(),
                Status = user.GetStatus()
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.Status = false;

            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred deleting user with ID '{user.Id}'.");
            }

            return RedirectToAction("Index", "Home");
        }
        

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        
        #endregion
    }
}
