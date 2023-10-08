using System.ComponentModel.DataAnnotations;

namespace VirtualProject.Models.Identity
{
    public class ProfileVM
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter Your Email")]
        [EmailAddress(ErrorMessage = "Enter real Email")]
        public string Email { get; set; }
        [MaxLength(13)]
        [MinLength(11)]
        public string PhoneNumber { get; set; }
        
        public string? ProfilePhotoName { get; set; }

        public IFormFile? ProfilePhotoUrl { get; set; }
    }
}
