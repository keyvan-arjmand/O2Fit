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
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>, IScopedDependency
    {
        private readonly IRepository<User> _userRepository;

        public GetUserQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        // private readonly IDatabaseConnectionFactory _connectionFactory;
       //
       // public GetUserQueryHandler(IDatabaseConnectionFactory connectionFactory)
       // {
       //     _connectionFactory = connectionFactory;
       // }

        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            //using var conn = await _connectionFactory.CreateConnectionAsync();
            //string query = $"SELECT \"UserName\" from \"AspNetUsers\" WHERE \"UserName\"='{request.UserName.ToLower()}'";
            //
            //UserDTO userDto = await conn.QuerySingleOrDefaultAsync<UserDTO>(query);
            var user = await _userRepository.TableNoTracking.FirstOrDefaultAsync(
                x => x.NormalizedUserName == request.UserName.ToUpper(), cancellationToken);

            if (user == null)
                return null;
            UserDTO dto = new UserDTO
            {
                UserName = user.UserName
            };
            return dto;

        }
    }
}
