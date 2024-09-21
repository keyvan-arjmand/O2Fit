using System.ComponentModel.DataAnnotations;

namespace User.API.Models
{
    public class UpdateFastingModeDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool FastingMode { get; set; }
    }
}
