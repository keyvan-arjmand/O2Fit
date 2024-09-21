using Market.Domain.Enums;

namespace Market.Application.Faqs.V1.Commands.UpdateUsefulFaq;

public class UpdateUsefulFaqCommand:IRequest
{
    public string Id { get; set; } = string.Empty;
    public UseFulStatus UseFulStatus { get; set; }
}