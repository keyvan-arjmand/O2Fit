using EventBus.Messages.Events.Services.Identity;

namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand,string>
{
    private readonly IUnitOfWork _uow;
    private readonly IWebHostEnvironment _environment;

    public UpdateUserProfileCommandHandler(IUnitOfWork uow, IWebHostEnvironment environment)
    {
        _uow = uow;
        _environment = environment;
    }

    public async Task<string> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await FindUserByIdAsync(request, cancellationToken);
        if (user != null)
        {
            string? fileName = null;
            if (!string.IsNullOrEmpty(request.ImageUri))
            {
                var base64 = Convert.FromBase64String(request.ImageUri);
                var path = Path.Combine(_environment.WebRootPath, "UserProfile");
                DirectoryInfo destination;

                if (!Directory.Exists(path))
                {
                    destination = Directory.CreateDirectory(path);
                }
                else
                {
                    destination = new DirectoryInfo(path);
                }
                
                fileName = Guid.NewGuid() + ".jpg";

                var address = Path.Combine(path, fileName);

                await File.WriteAllBytesAsync(address, base64, cancellationToken);
            }
            var age = DateTime.Now.Subtract(request.BirthDate).Days / 365;

            var targetNutrient = DefaultNutrients.DailyNutrients(age, request.Gender);
            
             user.UserProfile.Gender = request.Gender;
             user.UserProfile.BirthDate = request.BirthDate;
             user.UserProfile.ImageUri = fileName;
             user.UserProfile.FullName = request.FullName;
             user.UserProfile.FoodHabit = request.FoodHabit;
             user.UserProfile.WeightTimeRange = new WeightTimeRange(request.WeightChangeRate);
             user.UserProfile.DailyActivityRate = request.DailyActivityRate;
             user.UserProfile.IsPurchase = false;
             user.UserProfile.HeightSize = new HeightSize(request.HeightSize);
             user.UserProfile.Target.TargetWeight = new NotNegativeForDoubleTypes(request.TargetWeight);
             user.UserProfile.Target.TargetStep = new NonNegativeForIntegerTypes(request.TargetStep);
             user.UserProfile.Target.TargetThighSize = new NotNegativeForDoubleTypes(request.TargetThighSize);
             user.UserProfile.Target.TargetNeckSize = new NotNegativeForDoubleTypes(request.TargetNeckSize);
             user.UserProfile.Target.TargetNutrient = targetNutrient;

            AddDomainEvent(request, user);
            await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId), user,
                new Expression<Func<User, object>>[]
                {
                    x => x.UserProfile.Gender,
                    x => x.UserProfile.BirthDate,
                    x => x.UserProfile.ImageUri,
                    x => x.UserProfile.FullName,
                    x => x.UserProfile.FoodHabit,
                    x => x.UserProfile.WeightTimeRange,
                    x => x.UserProfile.DailyActivityRate,
                    x => x.UserProfile.IsPurchase,
                    x => x.UserProfile.HeightSize,
                    x => x.UserProfile.Target.TargetWeight,
                    x => x.UserProfile.Target.TargetStep,
                    x => x.UserProfile.Target.TargetThighSize,
                    x => x.UserProfile.Target.TargetNeckSize,
                    x => x.UserProfile.Target.TargetNutrient
                },null, cancellationToken);

            return user.Id.ToString();
        }

        return string.Empty;
    }

    private async Task<User?> FindUserByIdAsync(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>()
            .GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);
        return user;
    }

    private void AddDomainEvent(UpdateUserProfileCommand request, User user)
    {
        var @event = new UserProfileCreated
        {
            UserId = request.UserId
        };

        user.AddDomainEvent(@event);
    }
}