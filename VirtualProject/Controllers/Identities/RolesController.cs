using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualProject.Models.Identity;
using VirtualProject.Models.Identity.ApplicationsIdentity;

namespace VirtualProject.Controllers.Identities
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RolesController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Show()
        {
            var data = await roleManager.Roles.ToListAsync();

            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(NewRoleVM newRoleVM)
        {
            if (ModelState.IsValid)
            {
                var exits = await roleManager.RoleExistsAsync(newRoleVM.Name);

                if (exits)
                {
                    ModelState.AddModelError("Name", "Aleady Role is Exist");
                    return View("Show");
                }
                else
                {
                    var data = new ApplicationRole { Name = newRoleVM.Name };
                    var role = await roleManager.CreateAsync(data);
                    return RedirectToAction("Show");
                }
            }

            return View("Show");
        }
    }
}
