using SocialMessaging.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialMessaging.API.Models
{
    public class CreateMarketMessageDTO
    {
        [Required]
        public TranslationDTO Title { get; set; }
        [Required]
        public TranslationDTO Description { get; set; }
        [Required]
        public TranslationDTO ButtonName { get; set; }
        [Required]
        public string Link { get; set; }

        [Required]
        public TargetMarketMessage Target { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int Postpone { get; set; }


    }
}
