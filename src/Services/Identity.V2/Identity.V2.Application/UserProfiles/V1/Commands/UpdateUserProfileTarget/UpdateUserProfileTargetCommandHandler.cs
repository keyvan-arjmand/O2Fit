namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateUserProfileTarget;

public class UpdateUserProfileTargetCommandHandler : IRequestHandler<UpdateUserProfileTargetCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateUserProfileTargetCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateUserProfileTargetCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>()
            .GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId);
        //if (request.TargetStep.HasValue)
        //{
            user.UserProfile.Target.TargetStep = new NonNegativeForIntegerTypes(request.TargetStep);
        //}

        //if (request.TargetWeight.HasValue)
        //{
            user.UserProfile.Target.TargetWeight = new NotNegativeForDoubleTypes(request.TargetWeight);
        //}

        user.UserProfile.WeightChangeRate = request.WeightChangeRate;
        //if (request.TargetChest.HasValue)
        //{
            user.UserProfile.Target.TargetChest = new NotNegativeForDoubleTypes((double)request.TargetChest);
        //}

        //if (request.TargetArm.HasValue)
        //{
            user.UserProfile.Target.TargetArm = new NotNegativeForDoubleTypes((double)request.TargetArm);
        //}

        //if (request.TargetWaist.HasValue)
        //{
            user.UserProfile.Target.TargetWaist = new NotNegativeForDoubleTypes((double)request.TargetWaist);
        //}

        //if (request.TargetHighHip.HasValue)
        //{
            user.UserProfile.Target.TargetHighHip = new NotNegativeForDoubleTypes((double)request.TargetHighHip);
        //}

        //if (request.TargetThighSize.HasValue)
        //{
            user.UserProfile.Target.TargetThighSize = new NotNegativeForDoubleTypes((double)request.TargetThighSize);
        //}

        //if (request.TargetNeckSize.HasValue)
        //{
            user.UserProfile.Target.TargetNeckSize = new NotNegativeForDoubleTypes((double)request.TargetNeckSize);
        //}

        //if (request.TargetHip.HasValue)
        //{
            user.UserProfile.Target.TargetHip = new NotNegativeForDoubleTypes((double)request.TargetHip);
        //}

        //if (request.TargetShoulder.HasValue)
        //{
            user.UserProfile.Target.TargetShoulder = new NotNegativeForDoubleTypes((double)request.TargetShoulder);
        //}

        //if (request.TargetWrist.HasValue)
        //{
            user.UserProfile.Target.TargetWrist = new NotNegativeForDoubleTypes((double)request.TargetWrist);
        //}

        //if (request.TargetWater.HasValue)
        //{
            user.UserProfile.Target.TargetWater = new NotNegativeForDoubleTypes((double)request.TargetWater);
        //}

        var listOfTargetNutrient = new List<NotNegativeForDoubleTypes>();
        //if (request.TargetNutrient != null)
        //{
            foreach (var targetNutrient in request.TargetNutrient)
            {
                var result = new NotNegativeForDoubleTypes(targetNutrient);
                listOfTargetNutrient.Add(result);
            }

            user.UserProfile.Target.TargetNutrient = listOfTargetNutrient;
        //}
        //user.UserProfile.DailyActivityRate = request.DailyActivityRate;

        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId),
            user, new Expression<Func<User, object>>[]
            {
                x => x.UserProfile.Target.TargetStep,
                x => x.UserProfile.Target.TargetWeight,
                x => x.UserProfile.WeightChangeRate,
                x => x.UserProfile.Target.TargetChest,
                x => x.UserProfile.Target.TargetArm,
                x => x.UserProfile.Target.TargetWaist,
                x => x.UserProfile.Target.TargetHighHip,
                x => x.UserProfile.Target.TargetThighSize,
                x => x.UserProfile.Target.TargetNeckSize,
                x => x.UserProfile.Target.TargetHip,
                x => x.UserProfile.Target.TargetShoulder,
                x => x.UserProfile.Target.TargetWrist,
                x => x.UserProfile.Target.TargetNutrient,
                x => x.UserProfile.Target.TargetWater
                //x => x.UserProfile.DailyActivityRate
            }, null, cancellationToken);
    }
}