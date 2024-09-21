namespace Blogging.Service.Models
{
    public class ResponseViewModel
    {
        public int Id { get; set; }
        public TranslationNewDto ResponseTranslation { get; set; }
        public string ImageUrl { get; set; }
    }
}
