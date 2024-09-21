

using SocialMessaging.Domain.Entities.Translation;

namespace SocialMessaging.Service.ViewModels
{
    public class AppVersionViewModel
    {
        public string Link { get; set; }
        public bool IsForced { get; set; }
        public TranslationDto Description { get; set; }

    }
}
