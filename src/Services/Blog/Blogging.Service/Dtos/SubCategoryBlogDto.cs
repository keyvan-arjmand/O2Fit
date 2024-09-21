namespace Blogging.Service.Dtos
{
    public class SubCategoryBlogDto
    {
        public int Id { get; set; }
        public TranslationDto Title { get; set; }
        public string ImageName { get; set; }
        public string AltImage { get; set; }
        public int CategoryId { get; set; }
        public TranslationDto Category { get; set; }
    }
}