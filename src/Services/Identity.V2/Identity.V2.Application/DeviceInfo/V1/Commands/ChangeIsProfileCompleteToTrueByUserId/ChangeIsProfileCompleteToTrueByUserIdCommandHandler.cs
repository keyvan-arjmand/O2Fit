namespace Identity.V2.Application.DeviceInfo.V1.Commands.ChangeIsProfileCompleteToTrueByUserId;

public class
    ChangeIsProfileCompleteToTrueByUserIdCommandHandler : IRequestHandler<ChangeIsProfileCompleteToTrueByUserIdCommand>
{
    private readonly IUnitOfWork _uow;

    public ChangeIsProfileCompleteToTrueByUserIdCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(ChangeIsProfileCompleteToTrueByUserIdCommand request, CancellationToken cancellationToken)
    {
        var filter1 = Builders<User>.Filter.Eq(x => x.Id, ObjectId.Parse(request.UserId));
        var filter2 = Builders<User>.Filter.ElemMatch(x => x.DeviceInformation, e => e.IsProfileComplete == false);
        var sort = Builders<User>.Sort.Descending(x => x.DeviceInformation);
        var user = await _uow.UserGenericRepository<User>()
            .GetSingleDocumentByFilterWithSortAsync(filter1 & filter2, x=>x.DeviceInformation, cancellationToken);
        
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId); 
       user.DeviceInformation = user.DeviceInformation.OrderByDescending(o => o.CreateDate).ToList();

       user.DeviceInformation[0].IsProfileComplete = true;
       await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId), user,
           new Expression<Func<User, object>>[]
           {
               x => x.DeviceInformation
           });
       //var update = Builders<User>.Update.Set(x => x.DeviceInformation[0].IsProfileComplete, true);
       //
       //await _uow.UserGenericRepository<User>()
       //    .UpdateManyAsync(filter1 & filter2, user, update, null, cancellationToken);

       // foreach (var deviceInfo in user.DeviceInformation)
       // {
       //     deviceInfo.IsProfileComplete = true;
       //     await _uow.GenericRepository<DeviceInformation>().UpdateOneAsync(x => x.Id == deviceInfo.Id, deviceInfo,
       //         new Expression<Func<DeviceInformation, object>>[]
       //         {
       //             u => u.IsProfileComplete
       //         }, null, cancellationToken);
       // }
    }
}