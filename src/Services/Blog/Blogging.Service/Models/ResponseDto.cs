using MediatR;
using Microsoft.AspNetCore.Http;

namespace Blogging.Service.Models
{
    public class ResponseDto : IRequest<Unit>
    {
        public int QuestionId { get; set; }
        //public TranslationNewDto ResponseTranslation { get; set; }
        public string ResponseTranslation { get; set; }
        public IFormFile Image { get; set; }

    }
}
