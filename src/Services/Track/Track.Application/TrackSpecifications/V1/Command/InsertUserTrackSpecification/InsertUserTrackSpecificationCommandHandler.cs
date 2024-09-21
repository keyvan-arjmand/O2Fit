using System.Runtime.InteropServices.JavaScript;
using Common.Constants.Track;
using MongoDB.Bson;
using Newtonsoft.Json.Converters;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Interfaces.Services;
using Track.Domain.Aggregates.TrackSpecificationAggregate;
using Track.Domain.ValueObjects;
using static System.DateTime;

namespace Track.Application.TrackSpecifications.V1.Command.InsertUserTrackSpecification;

public class InsertUserTrackSpecificationCommandHandler : IRequestHandler<InsertUserTrackSpecificationCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;

    public InsertUserTrackSpecificationCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task<string> Handle(InsertUserTrackSpecificationCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId.StringToInt();
        var filter = Builders<TrackSpecification>.Filter.Eq(x => x.CreatedById, userId);
        var userTracks = await _uow.TrackSpecificationRepository()
            .GetSingleLastTrackUserSpecification(filter, cancellationToken);
        TrackSpecification track;
        if (userTracks != null && Compare(request.InsertDate, userTracks.InsertDate) == 0)
        {
            userTracks.Arm = request.Arm;
            userTracks.Chest = request.Chest;
            userTracks.HighHip = request.HighHip;
            userTracks.Hip = request.Hip;
            userTracks.InsertDate = request.InsertDate.ToUtcType();
            userTracks.Neck = request.Neck;
            userTracks.Shoulder = request.Shoulder;
            userTracks.Wrist = request.Wrist;
            userTracks.Weight = request.Weight;
            userTracks.Waist = request.Waist;
            userTracks.Thigh = request.Thigh;
            userTracks.Note = request.Note;
            userTracks.Image = request.Image;
            await _uow.GenericRepository<TrackSpecification>().UpdateOneAsync(x => x.Id == userTracks.Id,
                userTracks, new Expression<Func<TrackSpecification, object>>[]
                {
                    x => x.Arm,
                    x => x.Chest,
                    x => x.HighHip,
                    x => x.Hip,
                    x => x.InsertDate,
                    x => x.Neck,
                    x => x.Shoulder,
                    x => x.Wrist,
                    x => x.Weight,
                    x => x.Waist,
                    x => x.Thigh,
                    x => x.Note,
                    x => x.Image,
                }, null, cancellationToken);

            return string.IsNullOrWhiteSpace(userTracks.Image)
                ? string.Empty
                : TrackKeys.PatchUserTrackSpecification + userTracks.Image;
        }
        else
        {
            track = new TrackSpecification
            {
                Arm = request.Arm,
                Chest = request.Chest,
                HighHip = request.HighHip,
                Hip = request.Hip,
                InsertDate = request.InsertDate.ToUtcType(),
                Neck = request.Neck,
                Shoulder = request.Shoulder,
                Wrist = request.Wrist,
                Weight = request.Weight,
                Waist = request.Waist,
                Thigh = request.Thigh,
                AppId = request.AppId,
                Note = request.Note,
                Image = request.Image,
            };

            await _uow.GenericRepository<TrackSpecification>()
                .InsertOneAsync(track, null, cancellationToken);
            return string.IsNullOrWhiteSpace(track.Image)
                ? string.Empty
                : TrackKeys.PatchUserTrackSpecification + track.Image;
        }
    }
}