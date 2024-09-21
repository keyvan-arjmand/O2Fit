namespace Identity.V2.Application.UserProfiles.V1.Commands.PartialUpdateUserProfile;

public class PartialUpdateUserProfileCommandHandler : IRequestHandler<PartialUpdateUserProfileCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IWebHostEnvironment _environment;
    
    public PartialUpdateUserProfileCommandHandler(IUnitOfWork uow, IWebHostEnvironment environment)
    {
        _uow = uow;
        _environment = environment;
    }

    public async Task Handle(PartialUpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>().GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId);
       
        if (!string.IsNullOrEmpty(request.Image))
        {
            var base64EncodedBytes = Convert.FromBase64String(request.Image);
       
            string path = Path.Combine(_environment.WebRootPath, "UserProfile");
       
            //DirectoryInfo destination;
       
            if (!Directory.Exists(path))
            { 
                Directory.CreateDirectory(path);
            }
            // else
            // {
            //     destination = new DirectoryInfo(path);
            // }
       
            string fileName = Guid.NewGuid() + ".jpg";
       
            var address = Path.Combine(path, fileName);
       
            if (user.UserProfile.ImageUri!=null)
            {
                var oldAddress = Path.Combine(path, user.UserProfile.ImageUri);
                if (File.Exists(address))
                {
                    File.Delete(oldAddress);
                }
            }
            await File.WriteAllBytesAsync(address, base64EncodedBytes, cancellationToken);
       
            user.UserProfile.ImageUri = fileName;
        }
        
        user.UserProfile.FullName = request.FullName;
        user.UserProfile.FoodHabit = request.FoodHabit;
        user.UserProfile.DailyActivityRate = request.DailyActivityRate;
        user.UserProfile.Gender = request.Gender;
        user.UserProfile.BirthDate = request.BirthDate;
        user.UserProfile.HeightSize = new HeightSize(request.HeightSize);
       
        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId), user,
            new Expression<Func<User, object>>[]
            {
                x => x.UserProfile.FullName,
                x => x.UserProfile.FoodHabit,
                x => x.UserProfile.DailyActivityRate,
                x => x.UserProfile.Gender,
                x => x.UserProfile.BirthDate,
                x => x.UserProfile.HeightSize,
                x => x.UserProfile.ImageUri
            }, null, cancellationToken);
    }
}