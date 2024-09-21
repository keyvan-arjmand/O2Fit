using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O2fitUserHistory.Api.Models
{
    public class UserHistoryDTO
    {
        public List<UserTrackFoodModelDTO> userTrackFood { get; set; }
        public List<UserTrackWorkOutModelDTO> userTrackWorkout { get; set; }
        public List<UserTrackSleepModelDTO> userTrackSleep { get; set; }
        public List<UserTrackSpecification> userTrackSpecification { get; set; }
        public List<UserTrackSteps> userTrackSteps { get; set; }
        public List<UserTrackWater> userTrackWater { get; set; }
    }
}
