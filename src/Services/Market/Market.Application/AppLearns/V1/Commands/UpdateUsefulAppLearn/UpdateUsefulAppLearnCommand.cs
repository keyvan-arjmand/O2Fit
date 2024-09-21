using Market.Domain.Enums;

namespace Market.Application.AppLearns.V1.Commands.UpdateUsefulAppLearn;

public class UpdateUsefulAppLearnCommand:IRequest
{
    public string Id { get; set; } = string.Empty;
    public UseFulStatus UseFulStatus { get; set; }
}