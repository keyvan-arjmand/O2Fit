namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateBodyShapes;

public class UpdateBodyShapesCommandHandler : IRequestHandler<UpdateBodyShapesCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateBodyShapesCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateBodyShapesCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>()
            .GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId);
        
        user.UserProfile.Target.TargetArm = new NotNegativeForDoubleTypes(request.TargetArm);
        user.UserProfile.Target.TargetWaist = new NotNegativeForDoubleTypes(request.TargetWaist);
        user.UserProfile.Target.TargetHighHip = new NotNegativeForDoubleTypes(request.TargetHighHip);
        user.UserProfile.Target.TargetHip = new NotNegativeForDoubleTypes(request.TargetHip);
        user.UserProfile.Target.TargetShoulder = new NotNegativeForDoubleTypes(request.TargetShoulder);
        user.UserProfile.Target.TargetWrist = new NotNegativeForDoubleTypes(request.TargetWrist);
        user.UserProfile.Target.TargetThighSize = new NotNegativeForDoubleTypes(request.TargetThighSize);
        user.UserProfile.Target.TargetNeckSize = new NotNegativeForDoubleTypes(request.TargetNeckSize);
        user.UserProfile.Target.TargetChest = new NotNegativeForDoubleTypes(request.TargetChest);
        user.UserProfile.HeightSize = new HeightSize(request.HeightSize);

        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId), user,
            new Expression<Func<User, object>>[]
            {
                u => u.UserProfile.Target.TargetArm,
                u => u.UserProfile.Target.TargetWaist,
                u => u.UserProfile.Target.TargetHighHip,
                u => u.UserProfile.Target.TargetHip,
                u => u.UserProfile.Target.TargetShoulder,
                u => u.UserProfile.Target.TargetWrist,
                u => u.UserProfile.Target.TargetThighSize,
                u => u.UserProfile.Target.TargetNeckSize,
                u => u.UserProfile.Target.TargetChest,
                u => u.UserProfile.HeightSize
            }, null, cancellationToken);
        
    }
}