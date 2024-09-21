namespace Blogging.Service.Models
{
    public class SubBlogDto
    {
        public int Id { get; set; }
        public TranslationNewDto Translation { get; set; }
        public TranslationNewDto Description { get; set; }
        public string ImageUri { get; set; }
        public int BlogCategoryId { get; set; }

    }
}