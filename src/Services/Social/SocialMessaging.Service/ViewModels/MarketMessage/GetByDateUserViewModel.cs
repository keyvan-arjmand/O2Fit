using SocialMessaging.Domain.Entities.Translation;

namespace SocialMessaging.Service.ViewModels.MarketMessage
{
    public class GetByDateUserViewModel
    {
        public int Id { get; set; }
        public TranslationDto Title { get; set; }
        public TranslationDto Description { get; set; }
        public TranslationDto ButtonName { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public int Postpone { get; set; }
    }
}