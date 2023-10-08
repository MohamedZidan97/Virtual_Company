using System.ComponentModel.DataAnnotations;

namespace VirtualProject.Models.Identity
{
    public class ForgetPasswordByEmailVM
    {
        [Required]
        [EmailAddress(ErrorMessage = "Enter real Email")]
        public string Email { get; set; }
    }
}
