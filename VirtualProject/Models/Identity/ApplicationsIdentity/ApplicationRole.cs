using Microsoft.AspNetCore.Identity;

namespace VirtualProject.Models.Identity.ApplicationsIdentity
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
