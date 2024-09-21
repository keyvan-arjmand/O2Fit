using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user,List<string> roles);
    }
}