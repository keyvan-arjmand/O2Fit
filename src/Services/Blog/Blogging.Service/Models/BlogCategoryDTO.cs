namespace Blogging.Service.Models
{
    public class BlogCategoryDto
    {
        public int Id { get; set; }
        public TranslationNewDto Translation { get; set; }
        public string ImageUri { get; set; }
        public bool IsInPage { get; set; }
    }
}
