using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Identity.Data.Contracts;
using Identity.Common.Utilities;
using MediatR;
using Dapper;

namespace Identity.Service.v1.Query.GetUserInformation
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PageResult<GetAllUsersResult>>, ITransientDependency
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public GetAllUsersQueryHandler(IDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<PageResult<GetAllUsersResult>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<GetAllUsersResult> getAllUsersResults = new List<GetAllUsersResult>();

            using var conn = await _connectionFactory.CreateConnectionAsync();


            string userQuerySerach = "SELECT" +
                               " u.\"Id\", u.\"UserName\", u.\"Email\", u.\"PhoneNumber\", " + 
                               "u.\"PhoneNumberConfirmed\", u.\"Language\",u.\"RegisterDate\"," +
                               " u.\"ImageUri\", u.\"FirstName\", u.\"LastName\", u.\"CountryId\"," +
                               " u.\"IsActive\", u.\"ReferreralCode\",u.\"ReferreralInviter\"," +
                               " u.\"ReferreralCountBuy\"" +
                               " FROM \"AspNetUsers\" AS u" +
                               $" WHERE u.\"UserName\" = {request.UserName} ORDER BY u.\"Id\" DESC " +
                               $"OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}";

            string userQuery = "SELECT" +
                           " u.\"Id\", u.\"UserName\", u.\"Email\", u.\"PhoneNumber\", " +
                           "u.\"PhoneNumberConfirmed\", u.\"Language\",u.\"RegisterDate\"," +
                           " u.\"ImageUri\", u.\"FirstName\", u.\"LastName\", u.\"CountryId\"," +
                           " u.\"IsActive\", u.\"ReferreralCode\",u.\"ReferreralInviter\"," +
                           " u.\"ReferreralCountBuy\"" +
                           " FROM \"AspNetUsers\" AS u" +
                           " ORDER BY u.\"Id\" DESC " +
                           $"OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}";

            string deviceInfoQuery = "SELECT d.\"UserId\", d.\"OS\" , d.\"Market\" FROM \"DeviceInformations\" AS d ";
            
            List<Users> users = new List<Users>();

            users = string.IsNullOrEmpty(request.UserName) ? conn.Query<Users>(userQuery).ToList() : conn.Query<Users>(userQuerySerach).ToList();
            
            var deviceInfos = conn.Query<GetAllDeviceInfo>(deviceInfoQuery).ToList();

            foreach (var user in users)
            {
                foreach (var deviceInfo in deviceInfos)
                {
                    if (deviceInfo.UserId == user.Id)
                    {
                        GetAllUsersResult usersResult = new GetAllUsersResult
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            RegisterDate=user.RegisterDate,
                            ImageUri = user.ImageUri,
                            IsActive = user.IsActive,
                            CountryId = user.CountryId,
                            ReferreralCode = user.ReferreralCode,
                            ReferreralInviter = user.ReferreralInviter,
                            ReferreralCountBuy = user.ReferreralCountBuy,
                            Os = deviceInfo.OS,
                            FullName = user.FirstName+" "+user.LastName
                            
                        };
                        getAllUsersResults.Add(usersResult);
                    }
                }
            }


            PageResult<GetAllUsersResult> result = new PageResult<GetAllUsersResult>
            {
                Items = getAllUsersResults,
                PageSize = request.PageSize,
                PageIndex = request.Page ?? 1
            };

            return result;

        }
    }
}
