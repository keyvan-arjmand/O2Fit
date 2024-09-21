using Identity.Common.Utilities;
using MediatR;
using System;

namespace Identity.Service.v1.Query.GetUserInformation
{
    public class GetUsersQuery : IRequest<PageResult<GetUsersInformationQueryResult>>
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PageSize { get; set; }
        public int? Page { get; set; }
    }

    public class GetAllDeviceInfo
    {
        public int UserId { get; set; }
        public int OS { get; set; }
        public string Market { get; set; }

    }
}
