using Track.Application.Dtos;

namespace Track.Application.TrackWaters.V1.Commands.InsertTrackWater;

public class InsertTrackWaterCommand : IRequest<UserTrackWaterDto>
{
    public string UserId { get; set; }= string.Empty;
    public DateTime InsertDate { get; set; }
    public decimal Value { get; set; }
    public string AppId { get; set; } = string.Empty;
}