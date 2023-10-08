using System.ComponentModel.DataAnnotations;

namespace VirtualProject.Models.Identity
{
    public class NewRoleVM
    {
        [Required]
        public string Name { get; set; }
    }
}
