using System.ComponentModel.DataAnnotations;

namespace VirtualProject.Models.Identity.UsersContr
{
    public class ModifyUserVM : RegisterVM
    {
        [Required]
        public string Id { get; set; }
    }
}
