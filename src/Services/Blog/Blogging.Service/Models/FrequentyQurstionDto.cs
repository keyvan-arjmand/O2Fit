using System.Collections.Generic;

namespace Blogging.Service.Models
{
    public class FrequentlyQuestionDto
    {
        public TranslationNewDto QuestionText { get; set; }
        public List<TranslationNewDto> ResponseTexts { get; set; }
        public int FrequentlyQuestionCategoryId { get; set; }
    }
}
