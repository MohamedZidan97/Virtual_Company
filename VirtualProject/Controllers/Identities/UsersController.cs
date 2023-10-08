using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirtualProject.Models.Identity;
using VirtualProject.Models.Identity.ApplicationsIdentity;
using VirtualProject.Models.Identity.UsersContr;

namespace VirtualProject.Controllers.Identities
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersController(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> ShowUsers()
        {
            var data = await userManager.Users.ToListAsync();
            
            var data2=data.Select(user => new UsersVM
            {
                Id=user.Id,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email,
                Roles= userManager.GetRolesAsync(user).Result
            });
          
            return View(data2);
        }
        public async Task<IActionResult> AddUser()
        {
            var roles = await roleManager.Roles.Select(role => new RolesVM
            {
                RoleName = role.Name
            }).ToListAsync();

            var newUser = new AddUserVM
            {
                 Roles= roles
            };
           

            return View(newUser);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserVM addUserVM)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    FirstName=addUserVM.FirstName,
                    LastName= addUserVM.LastName,
                    Email=addUserVM.Email,
                    UserName=addUserVM.Email
                    
                };
                var result = await userManager.CreateAsync(user, addUserVM.Passwrod);
                if (result.Succeeded)
                {
                    foreach(var role in addUserVM.Roles)
                    {
                        if (role.IsSelected)
                        {
                            await userManager.AddToRoleAsync(user, role.RoleName);
                        }

                    }
                   
                    return RedirectToAction("ShowUsers");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }


            return View(addUserVM);
        }

        public async Task<IActionResult> ManagerRoles(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            if(user == null)
            {
                return NotFound();
            }

            var roles= await roleManager.Roles.ToListAsync();

            var userRoles = new UserRolesVM
            {
                UserId = user.Id,
                UserName = user.Email,
                Roles = roles.Select(role => new RolesVM
                {

                    RoleName = role.Name,
                    IsSelected = userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()

            };
            return View(userRoles);
        }

        [HttpPost]
        public async Task<IActionResult> ManagerRoles(UserRolesVM userRolesVM)
        {
            var user = await userManager.FindByIdAsync(userRolesVM.UserId);

            if(user==null) { return NotFound(); }
            var userRoles = await userManager.GetRolesAsync(user);

            foreach(var role in userRolesVM.Roles)
            {
                if(userRoles.Any(r=>r==role.RoleName) && !role.IsSelected)
                {
                    await userManager.RemoveFromRoleAsync(user,role.RoleName);
                }
                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                {
                    await userManager.AddToRoleAsync(user, role.RoleName);
                }
            }

            return RedirectToAction("ShowUsers");
            
        }

        public async Task<IActionResult> ModifyUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            var data = new ModifyUserVM
            {
                FirstName= user.FirstName,
                LastName= user.LastName,
                Email=user.Email
            };
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyUser(ModifyUserVM modify)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(modify.Id);
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                user.Email = modify.Email;
                user.UserName = modify.Email;
                user.FirstName = modify.FirstName;
                user.LastName = modify.LastName;
                

                var email = await userManager.FindByEmailAsync(modify.Email);

                if(email!=null && user.Id != email.Id)
                {
                    ModelState.AddModelError("Email", "Already this the Email is Exist, Choose one Other ");
                    return View(modify);
                }

                var result1 = await userManager.UpdateAsync(user);
                var result2 = await userManager.ResetPasswordAsync(user, token, modify.Passwrod);

                if (result1.Succeeded && result2.Succeeded)
                {
                    return RedirectToAction("ShowUsers");
                }

                foreach (var error in result1.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                foreach (var error in result2.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(modify);
            }
            return View(modify);
        }

    }
}
