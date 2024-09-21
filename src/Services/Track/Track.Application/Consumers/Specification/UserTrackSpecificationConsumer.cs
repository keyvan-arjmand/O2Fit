using EventBus.Messages.Contracts.Services.Payments.Transaction;
using MassTransit;
using System.Collections.Generic;
using EventBus.Messages.Contracts.Services.Track.Specification;
using Track.Application.Dtos;
using Track.Application.TrackSpecifications.V1.Query;
using Track.Application.TrackSpecifications.V1.Query.GetLastUserTrackSpecification;

namespace Track.Application.Consumers.Specification;

public class UserTrackSpecificationConsumer : IConsumer<UserTrackSpecificationRequest>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public UserTrackSpecificationConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<UserTrackSpecificationRequest> context)
    {
        var lastTrack = await _mediator.Send(new GetLastUserTrackSpecificationQuery(context.Message.Id));
        var result = _mapper.Map<IEnumerable<UserTrackSpecificationDto>, IEnumerable<UserTrackSpecificationResult>>(lastTrack);
        await context.RespondAsync(result);
    }
}