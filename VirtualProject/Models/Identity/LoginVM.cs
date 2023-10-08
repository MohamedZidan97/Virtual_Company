using System.ComponentModel.DataAnnotations;

namespace VirtualProject.Models.Identity
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Enter Your Email")]
        [EmailAddress(ErrorMessage = "Enter real Email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Password")]
        [MinLength(6)]
        public string Passwrod { get; set; }

        public bool RememberMe { get; set; }
    }
}
