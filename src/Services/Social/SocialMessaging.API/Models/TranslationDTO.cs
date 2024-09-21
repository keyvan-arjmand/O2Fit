using SocialMessaging.Domain.Entities.Translation;
using WebFramework.Api;

namespace SocialMessaging.API.Models
{
    public class TranslationDTO : BaseDto<TranslationDTO, TranslationDto>
    {
        public string Persian { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
    }
}
