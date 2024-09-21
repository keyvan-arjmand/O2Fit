using Blogging.Service.Models;
using System.Collections.Generic;

namespace Blogging.API.Models
{
    public class QuestionViewModel
    {
        public int FrequentlyQuestionCategoryId { get; set; }
        public string VideoUrl { get; set; }
        public TranslationNewDto QuestionTranslation { get; set; }
        public List<ResponseViewModel> Responses { get; set; }
    }
}
