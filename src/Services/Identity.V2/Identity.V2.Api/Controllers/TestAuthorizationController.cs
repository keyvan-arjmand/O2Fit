using Grpc.Core;

namespace Identity.V2.Api.Controllers;

[ApiVersion("1")]
public class TestAuthorizationController : BaseApiController
{
        
    [HttpGet] 
    [HasPermission(PermissionsConstants.CreateUserProfile)]
    public string Get()
    {
        
        return "Fine!";
    }
    //public string DoAuthenticatedCall(
    //    
    //   Greeter.GreeterClient client, string token)
    //{
    //    var headers = new Metadata();
    //    headers.Add("Authorization", $"Bearer {token}");
    //
    //    var response = client.SayHello(null,headers);
    //
    //    return response.Message;
    //}
}