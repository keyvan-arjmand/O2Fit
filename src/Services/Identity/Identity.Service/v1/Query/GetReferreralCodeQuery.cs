using MediatR;

namespace Identity.Service.v1.Query
{
    public class GetReferreralCodeQuery : IRequest<bool>
    {
        public string code { get; set; }
    }
}
