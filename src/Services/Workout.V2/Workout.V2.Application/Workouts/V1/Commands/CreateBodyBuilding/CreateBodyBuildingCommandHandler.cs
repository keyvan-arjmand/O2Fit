namespace Workout.V2.Application.Workouts.V1.Commands.CreateBodyBuilding;

public class CreateBodyBuildingCommandHandler : IRequestHandler<CreateBodyBuildingCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateBodyBuildingCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CreateBodyBuildingCommand request, CancellationToken cancellationToken)
    {
        var newImageName = string.Empty;
        var newIconName = string.Empty;
        var newVideoName = string.Empty;
        if (request.Image.Length > 0)
        {
            var folderName = Path.Combine("wwwroot", PathConstants.WorkoutImagePath);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            CreatePath(pathToSave);
            newImageName = Guid.NewGuid() + Path.GetExtension(request.Image.FileName);
            var fullPath = Path.Combine(pathToSave, newImageName);
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await request.Image.CopyToAsync(stream, cancellationToken);
        }
        
        if (request.Icon.Length > 0)
        {
            var folderName = Path.Combine("wwwroot", PathConstants.WorkoutIconPath);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            CreatePath(pathToSave);
            newIconName = Guid.NewGuid() + Path.GetExtension(request.Icon.FileName);
            var fullPath = Path.Combine(pathToSave, newIconName);
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await request.Icon.CopyToAsync(stream, cancellationToken);
        }
        
        if (request.Video.Length > 0)
        {
            var folderName = Path.Combine("wwwroot", PathConstants.WorkoutVideoPath);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            CreatePath(pathToSave);
            newVideoName = Guid.NewGuid() + Path.GetExtension(request.Video.FileName);
            var fullPath = Path.Combine(pathToSave, newVideoName);
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await request.Video.CopyToAsync(stream, cancellationToken);
        }

        var workout = new Domain.Aggregates.WorkoutsAggregate.Workout(request.Name.ToEntity<WorkoutNameTranslation>(),
            request.Recommendation.ToEntity<RecommendationTranslation>(),
            request.HowToDo.ToEntity<HowToDoTranslation>(),
            newIconName, newImageName, newVideoName, request.BurnedCalories, request.Tag, Classification.BodyBuilding,
            request.Gender, request.Level, request.TargetMuscle, CardioCategory.NoCategory,
            request.BodyMuscleIds.Select(ObjectId.Parse).ToList(), new List<WorkoutAttribute>());

        await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .InsertOneAsync(workout, null, cancellationToken);
    }
    
    
    private void CreatePath(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}