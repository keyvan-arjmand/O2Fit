using FoodStuff.Domain.Entities.Translation;
using MediatR;

namespace FoodStuff.Service.v2.Command.Translations
{
    public class UpdateNewTranslationCommand : IRequest<Translation>
    {
        public int Id { get; set; }
        public string Persian { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }

    }
}