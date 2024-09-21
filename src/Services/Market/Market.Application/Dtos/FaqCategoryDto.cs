namespace Market.Application.Dtos;

public class FaqCategoryDto
{
    public string Id { get; set; } = string.Empty;
    public TranslationDto Title { get; set; } = new();
}