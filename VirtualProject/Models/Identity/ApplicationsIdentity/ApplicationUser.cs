using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace VirtualProject.Models.Identity.ApplicationsIdentity
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public string? ProfilePhoroUrl { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } 
    }
}
