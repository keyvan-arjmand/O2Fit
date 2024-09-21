using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.NationalityAggregate;

namespace Food.V2.Application.Dtos.Nationality;
[BsonIgnoreExtraElements]

public class NationalityDto
{
    public string Id { get; set; } = string.Empty;
    public TranslationDto Translation { get; set; } = new();
    public TranslationDto ParentTranslation { get; set; } =  new();
    public string ParentId { get; set; } = string.Empty;
}