namespace Blogging.Service.Models
{
    public class FAQResponseDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public TranslationNewDto ResponseTranslation { get; set; }
        public string ImageUrl { get; set; }
    }
}