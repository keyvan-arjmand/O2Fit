namespace Workout.V2.Application.Workouts.V1.Commands.EditCardio;

public class EditCardioCommandHandler : IRequestHandler<EditCardioCommand>
{
    private readonly IUnitOfWork _uow;

    public EditCardioCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(EditCardioCommand request, CancellationToken cancellationToken)
    {
        var workout = await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (workout == null)
            throw new NotFoundException(nameof(Domain.Aggregates.WorkoutsAggregate.Workout), request.Id);

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

        workout.Tag = request.Tag;
        workout.BurnedCalories = request.BurnedCalories;
        workout.Name = request.Name.ToEntity<WorkoutNameTranslation>();
        workout.Gender = request.Gender;
        workout.Level = request.Level;
        workout.CardioCategory = request.CardioCategory;

        if (!string.IsNullOrEmpty(newImageName))
            workout.ImageName = newImageName;

        if (!string.IsNullOrEmpty(newIconName))
            workout.IconName = newIconName;

        if (!string.IsNullOrEmpty(newVideoName))
            workout.VideoName = newVideoName;


        await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>().UpdateOneAsync(
            x => x.Id == request.Id, workout,
            new Expression<Func<Domain.Aggregates.WorkoutsAggregate.Workout, object>>[]
            {
                x => x.Tag,
                x => x.BurnedCalories,
                x => x.Name,
                x => x.ImageName,
                x => x.IconName,
                x => x.VideoName,
                x => x.Gender,
                x => x.CardioCategory,
                x => x.Level
            }, null, cancellationToken);
    }
    
    private void CreatePath(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}