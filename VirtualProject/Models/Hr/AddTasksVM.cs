using System.ComponentModel.DataAnnotations;

namespace VirtualProject.Models.Hr
{
    public class AddTasksVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string HrId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Done { get; set; }
        public string? CvName { get; set; }

        public IFormFile? CvUrl { get; set; }
    }
}
