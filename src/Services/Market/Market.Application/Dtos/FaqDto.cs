namespace Market.Application.Dtos;

public class FaqDto
{
    public TranslationDto Category { get; set; } = new();
    public TranslationDto Question { get; set; } = new();
    public List<TranslationDto> Answers { get; set; } = new();
}