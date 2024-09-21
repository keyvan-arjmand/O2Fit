using MediatR;

namespace Identity.Service.v1.Query.GetUserInformation
{
   public class GetUsersInfomationQuery: IRequest<GetUsersInformationQueryResult>
    {
        public int UserId { get; set; }
    }
}
