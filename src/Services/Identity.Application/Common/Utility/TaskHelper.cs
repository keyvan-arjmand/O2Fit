using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Domain.Aggregates;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Application.Common.Utility;

public class TaskHelper
{
   
    public static async Task<string> GetToken(string username, UserManager<User> userManager)
    {
        var user = await userManager.FindByNameAsync(username);
        // return null if user not found
        if (user == null)
            return null;
        var roles = await userManager.GetRolesAsync(user);
        // authentication successful so generate jwt token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("AccessKeyAroosaneJwTForSeccretKey");
        var claims = new ClaimsIdentity();
        foreach (var role in roles)
        {
            claims.AddClaims(new[]
            {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            });
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddYears(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
   
}