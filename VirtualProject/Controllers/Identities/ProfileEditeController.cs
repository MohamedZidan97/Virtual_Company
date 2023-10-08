using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using VirtualProject.BL.Helper;
using VirtualProject.Models.Identity;
using VirtualProject.Models.Identity.ApplicationsIdentity;

namespace VirtualProject.Controllers.Identities
{
    [Authorize]
    public class ProfileEditeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileEditeController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Profile()
        {
            ApplicationUser user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            ProfileVM profile = new ProfileVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber=user.PhoneNumber,
                ProfilePhotoName=user.ProfilePhoroUrl
            };

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileVM profileVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = profileVM.FirstName;
                user.LastName = profileVM.LastName;
                user.Email = profileVM.Email;
                user.UserName = profileVM.Email;
                user.PhoneNumber = profileVM.PhoneNumber;
                user.ProfilePhoroUrl = UploadFiles.SaveFile(profileVM.ProfilePhotoUrl,"Photos");
                // Update other profile properties

                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }

            return View(profileVM);
        }


       
      
    }
}
