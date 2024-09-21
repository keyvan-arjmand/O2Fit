using MediatR;
using SocialMessaging.Service.ViewModels.MarketMessage;

namespace SocialMessaging.Service.v1.Query.MarketMessage
{
    public class GetByDateUserQuery : IRequest<GetByDateUserViewModel>
    {
    }
}
