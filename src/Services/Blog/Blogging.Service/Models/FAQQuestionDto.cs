using System.Collections.Generic;

namespace Blogging.Service.Models
{
    public class FAQQuestionDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public TranslationNewDto QuestionTranslation { get; set; }
        public string VideoUrl { get; set; }
        public List<FAQResponseDto> FrequentlyResponses { get; set; }
    }
}