using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.NationalityAggregate;

namespace Food.V2.Application.Nationalities.V1.Commands.UpdateNationality;

public class UpdateNationalityCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public TranslationDto Translation { get; set; } = new();
    public string? ParentId { get; set; } 
}