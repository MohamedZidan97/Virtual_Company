using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VirtualProject.BL.Interfaces;
using VirtualProject.BL.Repositories;
using VirtualProject.Models;

namespace VirtualProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeesRep employeesRep;

        public EmployeeController(IEmployeesRep employeesRep)
        {
            this.employeesRep = employeesRep;
        }

        public async  Task<IActionResult> Show()
        {
            var date = await employeesRep.Get();
            return View(date);
        }

        public async Task<IActionResult> Create()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM employeeVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await employeesRep.Add(employeeVM);
                    return RedirectToAction("Show");
                }

                return View(employeeVM);

            }
            catch (Exception ex)
            {
                EventLog log = new EventLog();
                log.Source = "Admin page";
                log.WriteEntry(ex.Message, EventLogEntryType.Error);
                return View(employeeVM);
            }
        }

    }
}
