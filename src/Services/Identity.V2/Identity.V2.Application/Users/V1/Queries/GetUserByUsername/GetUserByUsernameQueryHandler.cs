namespace Identity.V2.Application.Users.V1.Queries.GetUserByUsername;

public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserDto?>
{
    private readonly IUnitOfWork _uow;

    public GetUserByUsernameQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<UserDto?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<User>.Filter.Eq(x => x.NormalizedUserName, request.Username.ToUpper());
        var user = await _uow.UserGenericRepository<User>().GetSingleDocumentByFilterAsync(filter, cancellationToken)
            .ConfigureAwait(false);
        if (user == null)
        {
            return null;
        }

        return user.ToDto<UserDto>();
    }
}