using System.ComponentModel.DataAnnotations;

namespace VirtualProject.Models.Hr
{
    public class ShowHrVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string RoleName { get; set; }

    }
}
