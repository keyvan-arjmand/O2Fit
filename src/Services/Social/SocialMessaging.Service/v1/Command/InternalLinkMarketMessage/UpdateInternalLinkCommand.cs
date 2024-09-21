using MediatR;

namespace SocialMessaging.Service.v1.Command.InternalLinkMarketMessage
{
    public class UpdateInternalLinkCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int AdminId { get; set; }

    }
}
