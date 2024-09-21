using Blogging.Domain.Entities.FrequentlyQuestion;
using Microsoft.AspNetCore.Http;
using WebFramework.Api;

namespace Blogging.API.Models
{
    public class ResponseDto : BaseDto<ResponseDto, FrequentlyResponse>
    {
        public int nameId { get; set; }
        public Models.TranslationDto ResponseTranslation { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
    }
}
