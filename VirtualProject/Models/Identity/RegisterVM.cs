using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.Completion;
namespace VirtualProject.Models.Identity
{
    public class RegisterVM
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
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Password")]
        [MinLength(6)]
        public string Passwrod { get; set; }

        //[DataType(DataType.Password)]
        //[Required]
        //[MinLength(6)]
        //[Compare("Password", ErrorMessage = "Not Equal Password")]
        //public string ConfirmPassword { get; set; }


    }
}
