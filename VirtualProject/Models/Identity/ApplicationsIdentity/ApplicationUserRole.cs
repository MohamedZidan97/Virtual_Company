using Microsoft.AspNetCore.Identity;

namespace VirtualProject.Models.Identity.ApplicationsIdentity
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
