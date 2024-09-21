using AutoMapper;
using Grpc.Core;
using MediatR;
using System.Diagnostics;
using Google.Protobuf.WellKnownTypes;
using Track.Application.TrackSpecifications.V1.Query;
using MongoDB.Bson;
using System.Threading;
using MongoDB.Driver;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Utilities;
using Track.Domain.Aggregates.TrackSpecificationAggregate;

namespace Track.Grpc.Services;

public class TrackSpecificationService : UserTracks.UserTracksBase
{
    private readonly IUnitOfWork _uow;

    public TrackSpecificationService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public override async Task<UserTrackSpecificationReply> GetUserTrackSpecification(
        UserTrackSpecificationRequest request, ServerCallContext context)
    {
        var filter = Builders<TrackSpecification>.Filter.Eq(x => x.CreatedById, request.Id.StringToInt());
        var userTracks = await _uow.TrackSpecificationRepository()
            .GetLastTrackSpecification(filter);
        UserTrackSpecificationReply track = new UserTrackSpecificationReply();
        foreach (var x in userTracks)
        {
            //track.Tracks.Add(new UserTrack
            //{
            //    Id = x.Id,
            //    AppId = x.AppId,
            //    Arm = x.Arm,
            //    Chest = x.Chest,
            //    Hip = x.Hip,
            //    Image = x.Image,
            //    Neck = x.Neck,
            //    Note = x.Note,
            //    Shoulder = x.Shoulder,
            //    Thigh = x.Thigh,
            //    Waist = x.Waist,
            //    Weight = x.Weight,
            //    Wrist = x.Wrist,
            //    HighHip = x.HighHip,
            //    DateTime = Timestamp.FromDateTime(x.InsertDate),
            //    UserId = x.UserId.ToString()
            //});
        }

        return track;
    }
}