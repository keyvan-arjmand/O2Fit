using Microsoft.AspNetCore.Http;

namespace Blogging.API.Models
{
    public class ResponsePutDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string ResponseTranslation { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUri { get; set; }
    }
}
