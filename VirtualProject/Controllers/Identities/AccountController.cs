using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using VirtualProject.BL.Interfaces;
using VirtualProject.BL.Repositories;
using VirtualProject.Models.Identity;
using VirtualProject.Models.Identity.ApplicationsIdentity;
using static VirtualProject.DAL.ApplicationDbContext;

namespace VirtualProject.Controllers.Identities
{
    public class AccountController : Controller
    {
        private readonly IMailingRep mailingRep;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AccountController(IMailingRep mailingRep ,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,RoleManager<ApplicationRole> roleManager)
        {
            this.mailingRep = mailingRep;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {


            if (ModelState.IsValid)
            {
                var User = new ApplicationUser
                {
                    FirstName= registerVM.FirstName,
                    LastName= registerVM.LastName,
                    UserName = registerVM.Email,
                    Email = registerVM.Email

                };
                var result = await userManager.CreateAsync(User, registerVM.Passwrod);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(User, "User");
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }

            return View(registerVM);
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Passwrod, loginVM.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    ModelState.AddModelError("", "Check Your Email Or Password");

                }

            }
            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #region ForgetPassword
        public async Task<IActionResult> ForgetPassword()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordByEmailVM model)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(model.Email);


                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { model.Email, Token = token }, protocol: Request.Scheme);

                    await mailingRep.SendingMail(model.Email, "Confirm Reset Password", $"Please reset your password by clicking here: {passwordResetLink}");

                    return RedirectToAction("ConfirmSendMAil");
                }
            }

            return View(model);

        }
        // after send Mail Show already The Mail Was sent.
        public IActionResult ConfirmSendMAil()
        {

            return View();
        }

        public async Task<IActionResult> ResetPassword(string Email, string Token)
        {
            if (Email == null || Token == null)
            {
                ModelState.AddModelError("", "Not Found the Email in Website");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM reset)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(reset.Email);

                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, reset.Token, reset.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ConfirmResetPassword");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(reset);
                }

                return RedirectToAction("ResetPassword");
            }

            return View(reset);
        }
        public IActionResult ConfirmResetPassword()
        {

            return View();
        }


        #endregion

    }
}
