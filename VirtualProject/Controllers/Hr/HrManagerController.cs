using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VirtualProject.BL.Helper;
using VirtualProject.BL.Interfaces;
using VirtualProject.DAL;
using VirtualProject.DAL.Entities.Hr;
using VirtualProject.Models.Hr;
using VirtualProject.Models.Identity.ApplicationsIdentity;

namespace VirtualProject.Controllers.Hr
{
    [Authorize(Roles = "HrManager")]
   
    public class HrManagerController : Controller
    {
        private readonly IHrRep hrRep;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;
        private readonly RoleManager<ApplicationRole> roleManager;

        public HrManagerController(IHrRep hrRep,SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext db, RoleManager<ApplicationRole> roleManager)
        {
            this.hrRep = hrRep;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.db = db;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> ShowHrs()
        {
            var users = await userManager.Users.ToListAsync();
            if (users == null) { return NotFound(); }

            var roles = from user in db.ApplicationUsers
                        join userRole in db.ApplicationUserRoles
                        on user.Id equals userRole.UserId
                        join role in db.ApplicationRoles
                        on userRole.RoleId equals role.Id
                        select new
                        {
                            user.Email,
                            user.FirstName,
                            user.LastName,
                            role.Name
                        };
            var roles1 = await roles.Select(e => new ShowHrVM
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                RoleName = e.Name

            }).Where(e => e.RoleName == "Hr").ToListAsync();

            return View(roles1);
        }

        public async Task<IActionResult> RemoveHr(string Email)
        {
            var hr = await userManager.FindByEmailAsync(Email);          
            if (hr == null) { return NotFound(); }
            var del = await userManager.DeleteAsync(hr);
            return RedirectToAction("ShowHrs");
        }
        public async Task<IActionResult> AddTasks(string Email)
        {
            var hr = await userManager.FindByEmailAsync(Email);
            if (hr == null) { return NotFound(); }
            var data = new AddTasksVM 
            {
                HrId = hr.Id,
                Email = hr.Email
            };

            return View(data);

        }
        [HttpPost]
        public async Task<IActionResult> AddTasks(AddTasksVM addTasksVM)
        {
            await hrRep.AddTask(addTasksVM);
            return View(addTasksVM);
        }


      
    }
}
