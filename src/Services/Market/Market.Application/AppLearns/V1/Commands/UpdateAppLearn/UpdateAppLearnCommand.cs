using Market.Application.Dtos;

namespace Market.Application.AppLearns.V1.Commands.UpdateAppLearn;

public class UpdateAppLearnCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public string SubCategoryId { get; set; } = string.Empty;
    public TranslationDto VideoUrl { get; set; } = new();
    public TranslationDto Question { get; set; } = new();

    public string ImageName { get; set; } = string.Empty;
    public List<TranslationDto> Answer { get; set; } = new();
}