namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateUserProfileTargetNutrient;

public class UpdateUserProfileTargetNutrientCommandHandler : IRequestHandler<UpdateUserProfileTargetNutrientCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateUserProfileTargetNutrientCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateUserProfileTargetNutrientCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>()
            .GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);

        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId);

        var listOfTargetNutrient = new List<NotNegativeForDoubleTypes>();
        foreach (var targetNutrient in request.TargetNutrient)
        {
            var result = new NotNegativeForDoubleTypes(targetNutrient);
            listOfTargetNutrient.Add(result);
        }

        user.UserProfile.Target.TargetNutrient = listOfTargetNutrient;

        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId), user,
            new Expression<Func<User, object>>[]
            {
                u => u.UserProfile.Target.TargetNutrient
            }, null, cancellationToken);
    }
}