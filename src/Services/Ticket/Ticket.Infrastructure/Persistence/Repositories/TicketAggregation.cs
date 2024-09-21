using Ticket.Domain.Aggregates.TicketAggregate;

namespace Ticket.Infrastructure.Persistence.Repositories;

public class TicketAggregation : GenericRepository<Domain.Aggregates.TicketAggregate.Ticket>, ITicketAggregation
{
    private readonly IMongoClient _mongoClient;
    private IMongoCollection<TicketMessage> TicketMessage { get; }


    public TicketAggregation(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService) :
        base(mediator, configuration, currentUserService)
    {
        var settings = MongoClientSettings.FromConnectionString(configuration["MongoSettings:ConnectionString"]);

        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        _mongoClient = new MongoClient(settings);
        TicketMessage = _mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"])
            .GetCollection<TicketMessage>(ToLowerFirsWord(nameof(TicketMessage)) + "s");
    }

    // public async Task<UserTicketDto> GetUserTicketById(string id, CancellationToken cancellationToken)
    // {
    //     
    //     
    // }

    private string ToLowerFirsWord(string word)
    {
        word = word.Replace(word.Substring(0, 1), word.Substring(0, 1).ToLower());
        return word;
    }
}