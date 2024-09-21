using FoodStuff.Domain.Entities.Translation;
using MediatR;

namespace Service.v1.Command
{
    public class UpdateTranslationCommand : IRequest<Translation>
    {
        public Translation Translation { get; set; }
    }
}
