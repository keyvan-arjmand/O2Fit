using SocialMessaging.Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialMessaging.API.Models
{
    public class UpdateAppVersionDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Version { get; set; }

        [Required]
        public bool IsForced { get; set; }

        [Required]
        public TranslationDTO Description { get; set; }

        [Required]
        public List<UpdateMarketTypeDTO> MarketTypesDTO { get; set; }
    }

    public class UpdateMarketTypeDTO
    {
        public MarketType MarketType { get; set; }

        public string Link { get; set; }
    }
}
