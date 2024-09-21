using Payment.Domain.Aggregates.SequenceAggregate;

namespace Payment.Infrastructure.Persistence.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly IMongoClient _mongoClient;
    private readonly ICurrentUserService _currentUserService;
    private IClientSessionHandle Session { get; set; }
    private Hashtable? _repositories;
    public List<Func<Task>> Commands { get; set; } = new List<Func<Task>>();


    public UnitOfWork(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _configuration = configuration;
        _currentUserService = currentUserService;

        var settings = MongoClientSettings.FromConnectionString(_configuration["MongoSettings:ConnectionString"]);

        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        _mongoClient = new MongoClient(settings);
    }

    public IGenericRepository<T> GenericRepository<T>() where T : AggregateRoot
    {
        if (_repositories is null)
            _repositories = new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _mediator, _configuration, _currentUserService);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }
    public ISequenceRepository SequenceRepository() 
    {
        if (_repositories is null)
            _repositories = new Hashtable();

        var type = typeof(Sequence).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(SequenceRepository);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType,  _mediator , _configuration,
                    _currentUserService);
            _repositories.Add(type, repositoryInstance);
        }
        return (SequenceRepository)_repositories[type];
    }
    public void AddCommand(Func<Task> func)
    {
        Commands.Add(func);
    }

    public async Task TransactionAsync()
    {
        using (Session = await _mongoClient.StartSessionAsync())
        {
            Session.StartTransaction();

            var commandTasks = Commands.Select(c => c());

            await Task.WhenAll(commandTasks);

            await Session.CommitTransactionAsync();

            Commands.Clear();
        }
    }


    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}