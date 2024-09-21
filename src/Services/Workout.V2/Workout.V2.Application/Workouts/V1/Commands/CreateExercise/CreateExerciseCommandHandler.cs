namespace Workout.V2.Application.Workouts.V1.Commands.CreateExercise;

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;
    public CreateExerciseCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var iconName = string.Empty;
        var imageName = string.Empty;

        if (!string.IsNullOrEmpty(request.Icon))
        {
            iconName = _fileService.AddImage(request.Icon, PathConstants.WorkoutIconPath, Guid.NewGuid().ToString());
        }
        
        if (!string.IsNullOrEmpty(request.Image))
        {
            imageName = _fileService.AddImage(request.Image, PathConstants.WorkoutImagePath, Guid.NewGuid().ToString());
        }

        var attributes = new List<WorkoutAttribute>();
        if (request.Attributes != null && request.Attributes.Count != 0)
        {
            attributes.AddRange(request.Attributes.Select(dto => new WorkoutAttribute(dto.Name.ToEntity<WorkoutAttributeTranslation>(),
            dto.WorkoutAttributeValue.ToEntity<WorkoutAttributeValue>().ToList())));
        }
        
        var workout = new Domain.Aggregates.WorkoutsAggregate.Workout(request.Name.ToEntity<WorkoutNameTranslation>(),
            new RecommendationTranslation(), new HowToDoTranslation(),
            iconName, imageName, string.Empty, request.BurnedCalories,
            request.Tag, Classification.Exercise, Gender.Both , Level.NoLevel, TargetMuscles.NonOfThese,
            CardioCategory.NoCategory, new List<ObjectId>(), attributes);

        await _uow.GenericRepository<Domain.Aggregates.WorkoutsAggregate.Workout>()
            .InsertOneAsync(workout, null, cancellationToken);
    }
}