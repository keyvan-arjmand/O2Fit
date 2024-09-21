using Domain;

namespace SocialMessaging.Domain.Entities.Translation
{
    public class TranslationDto : BaseEntity<int>
    {
        public string Persian { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
    }
}
