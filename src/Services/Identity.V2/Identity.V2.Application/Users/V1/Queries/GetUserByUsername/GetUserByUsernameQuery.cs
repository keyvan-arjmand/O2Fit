namespace Identity.V2.Application.Users.V1.Queries.GetUserByUsername;

public record GetUserByUsernameQuery(string Username) : IRequest<UserDto>;