namespace Identity.V2.Application.DeviceInfo.V1.Commands.AddDeviceInfo;

public class AddDeviceInfoCommandHandler : IRequestHandler<AddDeviceInfoCommand>
{
    private readonly IUnitOfWork _uow;

    public AddDeviceInfoCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(AddDeviceInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>()
            .GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId);
        
        //var deviceInfo = new DeviceInformation((OsType)request.Os, request.Brand, request.PhoneModel,
        //    request.Brightness, request.IpAddress, request.FontScale,
        //    request.Display, request.ApiLevelSdk, request.AndroidId, request.IsTablet, request.IsEmulator,
        //    request.AppVersion, request.Market, DateTime.UtcNow, false);

        var deviceInfo = new DeviceInformation
        {
            
            Os = (OsType)request.Os,
            Brand = request.Brand,
            PhoneModel = request.PhoneModel,
            Brightness = request.Brightness,
            IpAddress = request.IpAddress,
            FontScale = request.FontScale,
            Display = request.Display,
            ApiLevelSdk = request.ApiLevelSdk,
            AndroidId = request.AndroidId,
            IsTablet = request.IsTablet,
            IsEmulator = request.IsEmulator,
            AppVersion = request.AppVersion,
            Market = request.Market,
            CreateDate = DateTime.UtcNow,
            IsProfileComplete = false,
            Id = ObjectId.GenerateNewId().ToString()
        };
        
        var filter = Builders<User>.Filter.Eq(x => x.Id, ObjectId.Parse(request.UserId));
        var update = Builders<User>.Update.Push(x => x.DeviceInformation, deviceInfo);

        await _uow.UserGenericRepository<User>().UpdateOneAsync(filter,user,update,null,cancellationToken);
        //await _uow.GenericRepository<DeviceInformation>().InsertOneAsync(deviceInfo, null, cancellationToken);
    }
}