using System.ComponentModel.DataAnnotations;

namespace SocialMessaging.API.Models
{
    public class UpdateInternalLinkDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Link { get; set; }
    }
}
