using System.Threading;
using System.Threading.Tasks;
using Common;
using Dapper;
using Data.Contracts;
using Domain;
using Identity.Data.Contracts;
using Identity.Domain.UserDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Service.v1.Query
{
    public class GetReferreralCodeQueryHandler : IRequestHandler<GetReferreralCodeQuery ,bool> , IScopedDependency
    {
        //private readonly IDatabaseConnectionFactory _connectionFactory;
        //public GetReferreralCodeQueryHandler(IDatabaseConnectionFactory connectionFactory)
        //{
        //    _connectionFactory = connectionFactory;
        //}

        private readonly IRepository<User> _userRepository;

        public GetReferreralCodeQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(GetReferreralCodeQuery request, CancellationToken cancellationToken)
        {
            //string query = $"SELECT EXISTS (SELECT FROM \"AspNetUsers\" WHERE \"ReferreralCode\"='{request.code.ToUpper()}')";
            //using var conn = await _connectionFactory.CreateConnectionAsync();

            //var result = await conn.QuerySingleAsync<ReferreralCodeDTO>(query);
            var user = await _userRepository.TableNoTracking.FirstOrDefaultAsync(
                x => x.ReferreralCode == request.code.ToUpper(), cancellationToken);

            return user != null;
        }
    }
}
