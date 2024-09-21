using Market.Application.Dtos;

namespace Market.Application.AppLearns.V1.Commands.InsertAppLearn;

public class InsertAppLearnCommand : IRequest
{
    public string SubCategoryId { get; set; } = string.Empty;
    public TranslationDto VideoUrl { get; set; } = new();
    public TranslationDto Question { get; set; } = new();

    public string ImageName { get; set; } = string.Empty;
    public List<TranslationDto> Answer { get; set; } = new();
}