using SocialMessaging.Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialMessaging.API.Models
{
    public class CreateAppVersionDTO
    {
        [Required]
        public string Version { get; set; }

        [Required]
        public bool IsForced { get; set; }
        [Required]
        public TranslationDTO Description { get; set; }

        [Required]
        public List<CreateMarketTypeDTO> MarketTypesDTO { get; set; }


    }

    public class CreateMarketTypeDTO
    {
        public MarketType MarketType { get; set; }

        public string Link { get; set; }
    }
}
