using Microsoft.AspNetCore.Mvc;

namespace VirtualProject.Controllers
{
    public class MailingController : Controller
    {
        public IActionResult SendMail()
        {
            return View();
        }
    }
}
