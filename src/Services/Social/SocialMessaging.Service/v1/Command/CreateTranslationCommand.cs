using MediatR;
using SocialMessaging.Domain.Entities.Translation;

namespace SocialMessaging.Service.v1.Command
{
    public class CreateTranslationCommand : IRequest<TranslationDto>
    {
        public TranslationDto Translation { get; set; }
    }
}
