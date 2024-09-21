namespace Workout.V2.Application.Workouts.V1.Commands.EditExercise;

public class EditExerciseCommandHandler : IRequestHandler<EditExerciseCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;

    public EditExerciseCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task Handle(EditExerciseCommand request, CancellationToken cancellationToken)
    {
        var workout = await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (workout == null)
            throw new NotFoundException(nameof(Domain.Aggregates.WorkoutsAggregate.Workout), request.Id);

        var imageName = string.Empty;
        var iconName = string.Empty;

        if (!string.IsNullOrEmpty(request.Image))
        {
            _fileService.RemoveImage(workout.ImageName, PathConstants.WorkoutImagePath);

            imageName = _fileService.AddImage(request.Image, PathConstants.WorkoutImagePath, Guid.NewGuid().ToString());
        }
        
        if (!string.IsNullOrEmpty(request.Icon))
        {
            _fileService.RemoveImage(workout.IconName, PathConstants.WorkoutIconPath);

            iconName = _fileService.AddImage(request.Icon, PathConstants.WorkoutIconPath, Guid.NewGuid().ToString());
        }

        var attributes = new List<WorkoutAttribute>();
        if (request.Attributes != null && request.Attributes.Count != 0)
        {
            attributes.AddRange(request.Attributes.Select(dto => new WorkoutAttribute(dto.Name.ToEntity<WorkoutAttributeTranslation>(), 
                dto.WorkoutAttributeValue.ToEntity<WorkoutAttributeValue>().ToList())));
        }
        workout.Tag = request.Tag;
        workout.BurnedCalories = request.BurnedCalories;
        workout.Name = request.Name.ToEntity<WorkoutNameTranslation>();
        workout.WorkoutAttribute = attributes;
        
        if (!string.IsNullOrEmpty(imageName))
            workout.ImageName = imageName;
        
        if (!string.IsNullOrEmpty(iconName))
            workout.IconName = iconName;

        await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>().UpdateOneAsync(
            x => x.Id == request.Id, workout,
            new Expression<Func<Domain.Aggregates.WorkoutsAggregate.Workout, object>>[]
            {
                x => x.Tag,
                x => x.BurnedCalories,
                x => x.Name,
                x => x.WorkoutAttribute,
                x => x.ImageName,
                x => x.IconName
            }, null, cancellationToken);
    }
}