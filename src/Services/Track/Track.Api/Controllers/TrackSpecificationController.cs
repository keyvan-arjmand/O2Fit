using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Buffers.Text;
using System.Threading;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Track.Specification;
using Track.Application.Common.Utilities;
using Track.Application.Dtos;
using Track.Application.TrackSpecifications.V1.Command.InsertUserTrackSpecification;
using Track.Application.TrackSpecifications.V1.Query;
using Track.Application.TrackSpecifications.V1.Query.GetLastUserSpecificationByIndex;
using Track.Application.TrackSpecifications.V1.Query.GetLastUserTrackSpecification;
using Track.Application.TrackSpecifications.V1.Query.GetUserSpecificationHistory;
using Track.Domain.Aggregates.TrackSpecificationAggregate;
using static MassTransit.ValidationResultExtensions;

namespace Track.Api.Controllers
{
    [ApiVersion("1")]
    public class TrackSpecificationController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _environment;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileService _fileService;

        public TrackSpecificationController(IMediator mediator, IWebHostEnvironment environment,
            ICurrentUserService currentUserService, IFileService fileService)
        {
            _mediator = mediator;
            _environment = environment;
            _currentUserService = currentUserService;
            _fileService = fileService;
        }

        [AllowAnonymous]
        [HttpGet("get-last-track")]
        public async Task<List<UserTrackSpecificationDto>> GetLastUserSpecificationTrack([FromQuery] string id,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetLastUserTrackSpecificationQuery(id),
                cancellationToken);
        }

        [PermissionAuthorize(PermissionsConstants.FullAccess)]

        [HttpGet("get-user-specification-history")]
        public async Task<List<UserTrackSpecificationDto>> GetUserSpecificationHistory(int days,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new GetUserSpecificationHistoryQuery(_currentUserService.UserId!, DateTime.Now.AddDays(-days)),
                cancellationToken);
        }

        [PermissionAuthorize(PermissionsConstants.FullAccess)]

        [HttpGet("get-last-user-specification-by-index")]
        public async Task<UserTrackSpecificationDto> GetLastUserSpecificationByIndex(int index,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetLastUserSpecificationByIndexQuery(_currentUserService.UserId!, index),
                cancellationToken);
        }

        [HttpPost("user-track-specification")]
        [PermissionAuthorize(PermissionsConstants.FullAccess)]

        public async Task<ApiResult> Post(InsertUserTrackSpecificationCommand request,
            CancellationToken cancellationToken)
        {
            request.UserId = _currentUserService.UserId!;
            if (!string.IsNullOrWhiteSpace(request.Image))
            {
                request.Image = _fileService.AddImage(request.Image, "UserBodySize",
                    Guid.NewGuid().ToString().Substring(0, 6));
            }

            var imageName = await _mediator.Send(request, cancellationToken);
            return new ApiResult<ImageUrlResult>(new ImageUrlResult
            {
                ImageUrl = imageName
            }, string.Empty, ApiResultStatusCode.Success, true);
        }
    }
}