using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VirtualProject.BL.Interfaces;
using VirtualProject.Models.Hr;
using VirtualProject.Models.Identity.ApplicationsIdentity;

namespace VirtualProject.Controllers.Hr
{
    public class HrEmployeeController : Controller
    {
        private readonly IHrRep hrRep;
        private readonly UserManager<ApplicationUser> userManager;

        public HrEmployeeController(IHrRep hrRep, UserManager<ApplicationUser> userManager)
        {
            this.hrRep = hrRep;
            this.userManager = userManager;
        }
      
        public async Task<IActionResult> ShowTasks()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null) { return NotFound(); }

            var data = await hrRep.GetTasks(user.Id);

            return View(data);
        }
        public async Task<IActionResult> DoneTask(int id)
        {
            await hrRep.DoneToTask(id);
            return RedirectToAction("ShowTasks");
        }
    }
}
