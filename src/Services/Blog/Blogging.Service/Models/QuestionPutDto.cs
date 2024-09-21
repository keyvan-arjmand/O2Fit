namespace Blogging.Service.Models
{
    public class QuestionPutDto
    {
        public int Id { get; set; }
        public int FrequentlyQuestionCategoryId { get; set; }
        public string VideoUrl { get; set; }
        public TranslationNewDto QuestionTranslation { get; set; }
    }
}
