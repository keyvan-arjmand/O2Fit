using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.NationalityAggregate;

namespace Food.V2.Application.Nationalities.V1.Commands.CreateNationality;

public  class CreateNationalityCommand : IRequest<string>
{
    public TranslationDto Translation { get; set; } = new();
    public string ParentId { get; set; } = string.Empty;
}