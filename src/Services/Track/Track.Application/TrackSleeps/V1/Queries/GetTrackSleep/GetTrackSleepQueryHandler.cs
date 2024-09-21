using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackSleepAggregate;

namespace Track.Application.TrackSleeps.V1.Queries.GetTrackSleep;

public class GetTrackSleepQueryHandler : IRequestHandler<GetTrackSleepQuery, UserTrackSleepAverageDto>
{
    private readonly IUnitOfWork _work;

    public GetTrackSleepQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<UserTrackSleepAverageDto> Handle(GetTrackSleepQuery request, CancellationToken cancellationToken)
    {
        var listSleepDetail = new List<SleepValueDto>();
        var durationSum = new TimeSpan(0, 0, 0);
        var filter = Builders<TrackSleep>.Filter.Where(x => x.InsertDate.Date >= DateTime.Now.AddDays(-request.Days) &&
                                                            x.CreatedById ==
                                                      request.UserId.StringToInt());
        var result = await _work.GenericRepository<TrackSleep>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        double rateSum = 0;
        decimal burnedCalorieSum = 0;

        for (int d = 0; d <= request.Days; d++)
        {
            var dateTime = DateTime.Now.AddDays(-d);
            var sleeps = result.Where(x => x.InsertDate.Date == dateTime.Date).ToList();
            int n = 0;
            double rate = 0;

            foreach (var item in sleeps)
            {
                var startDate = item.InsertDate - item.Duration;
                burnedCalorieSum += item.BurnedCalories.GetNotNegativeForDecimalType();
                rateSum += item.Rate;
                rate += item.Rate;
                durationSum += item.Duration;
                n++;
                listSleepDetail.Add(new SleepValueDto
                {
                    AppId = item.AppId,
                    Id = item.Id,
                    Date = dateTime,
                    Value = sleeps.Sum(s => s.BurnedCalories.Value),
                    StartDate = startDate,
                    EndDate = item.InsertDate,
                    Duration = item.Duration,
                    DailyAverageRate = Math.Round(rate / n, 2)
                });
            }
        }

        return new UserTrackSleepAverageDto()
        {
            UserId = request.UserId,
            AverageBurnedCalories = Math.Round(burnedCalorieSum / request.Days, 2),
            AverageRate = Math.Round(rateSum / request.Days, 2),
            AverageSleepDuration = TimeSpan.FromMinutes(durationSum.TotalMinutes / request.Days),
            SleepDetails = listSleepDetail
        };
    }
}