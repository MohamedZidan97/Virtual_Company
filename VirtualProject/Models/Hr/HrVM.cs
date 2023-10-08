using System.ComponentModel.DataAnnotations;

namespace VirtualProject.Models.Hr
{
    public class HrVM
    {
        [Required]
        public string IdHr { get; set; }
        [Required]
        public List<string> Tasks { get; set; }

    }
}
