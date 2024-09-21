using Identity.V2.Domain.Common.Role;

namespace Identity.V2.Infrastructure.Persistence.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly IMongoClient _mongoClient;
    private IClientSessionHandle Session { get; set; }
    public List<Func<Task>> Commands { get; set; } = new List<Func<Task>>();
    private Hashtable? _repositories;

    public UnitOfWork(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;

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
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _mediator, _configuration);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    public IUserGenericRepository<T> UserGenericRepository<T>() where T : UserAggregateRoot
    {
        if (_repositories is null)
            _repositories = new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(Repositories.UserGenericRepository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _mediator, _configuration);

            _repositories.Add(type, repositoryInstance);
        }

        return (IUserGenericRepository<T>)_repositories[type];
    }

    public IRoleGenericRepository<T> RoleGenericRepository<T>() where T : RoleAggregateRoot
    {
        if (_repositories is null)
            _repositories = new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(Repositories.RoleGenericRepository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _mediator, _configuration);

            _repositories.Add(type, repositoryInstance);
        }

        return (IRoleGenericRepository<T>)_repositories[type];
    }
    public ICountryRepository CountryRepository()
    {
        if (_repositories is null)
            _repositories = new Hashtable();

        var type = typeof(Country);

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(Repositories.CountryRepository);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType, _mediator, _configuration);

            _repositories.Add(type, repositoryInstance);
        }

        return (CountryRepository)_repositories[type];
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