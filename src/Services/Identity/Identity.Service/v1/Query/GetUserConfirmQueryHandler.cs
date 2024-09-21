using System.Threading;
using System.Threading.Tasks;
using Common;
using Dapper;
using Data.Contracts;
using Domain;
using Identity.Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Service.v1.Query
{
    public class GetUserConfirmQueryHandler :IRequestHandler<GetUserConfirmQuery, User>, IScopedDependency
    {
        //private readonly IDatabaseConnectionFactory _connectionFactory;
        //
        //public GetUserConfirmQueryHandler(IDatabaseConnectionFactory connectionFactory)
        //{
        //    _connectionFactory = connectionFactory;
        //}
         private readonly IRepository<User> _userRepository;

         public GetUserConfirmQueryHandler(IRepository<User> userRepository)
         {
             _userRepository = userRepository;
         }

         public async Task<User> Handle(GetUserConfirmQuery request, CancellationToken cancellationToken)
        {
            //using var conn = await _connectionFactory.CreateConnectionAsync();
            //string query =
            //    "SELECT * " +
            //    "from \"AspNetUsers\" " +
            //    $"WHERE \"UserName\"='{request.UserName}'";
            //
            //var userDTO = await conn.QueryFirstOrDefaultAsync<User>(query);

            var user = await _userRepository.TableNoTracking.FirstOrDefaultAsync(
                x => x.NormalizedUserName == request.UserName.ToUpper(), cancellationToken).ConfigureAwait(false);
            if (user == null)
                return null;

            return user;
        }
    }
}
