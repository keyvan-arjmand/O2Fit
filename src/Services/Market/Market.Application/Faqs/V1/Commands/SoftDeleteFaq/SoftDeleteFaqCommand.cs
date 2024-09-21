namespace Market.Application.Faqs.V1.Commands.SoftDeleteFaq;

public record SoftDeleteFaqCommand(string Id) : IRequest;