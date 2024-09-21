using Blogging.Domain.Entities.FrequentlyQuestion;
using System.Collections.Generic;
using WebFramework.Api;

namespace Blogging.API.Models
{
    public class QuestionDto : BaseDto<QuestionDto, FrequentlyQuestion>
    {
        public int nameId { get; set; }
        public TranslationDto QuestionTranslation { get; set; }
        public string VideoUrl { get; set; }
        public List<ResponseDto> Responses { get; set; }

    }
}
