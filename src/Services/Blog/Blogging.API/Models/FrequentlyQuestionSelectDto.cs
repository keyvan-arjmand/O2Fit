using System.Collections.Generic;

namespace Blogging.API.Models
{
    public class FrequentlyQuestionSelectDto
    {
        public int Id { get; set; }
        public TranslationDto QuestionText { get; set; }
        public List<TranslationDto> ResponseText { get; set; }
        public int CategoryId { get; set; }
    }
}
