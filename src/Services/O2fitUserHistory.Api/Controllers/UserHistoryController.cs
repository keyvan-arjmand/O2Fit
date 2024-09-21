using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using O2fitUserHistory.Api.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace O2fitUserHistory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserHistoryController : Controller
    {
        [HttpGet]
        public async Task<UserHistoryDTO> GetUserHistory(int userId, int days, string token)
        {
            UserHistoryDTO userhistoryDTO = new UserHistoryDTO();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            //usertrackfood
            var comp = client.GetAsync($"https://food.o2fitt.com/api/v1/UserTrackFood/UserHistory?userId={userId}&days={days}").Result;
            var json = await comp.Content.ReadAsStringAsync();
            GlobalResult res = JsonConvert.DeserializeObject<GlobalResult>(json);
            userhistoryDTO.userTrackFood = JsonConvert.DeserializeObject<List<UserTrackFoodModelDTO>>(res.data.ToString());


            //usertrackwater
            var comp1 = client.GetAsync($"https://food.o2fitt.com/api/v1/UserTrackWater/UserHistory?userId={userId}&days={days}").Result;
            var json1 = await comp1.Content.ReadAsStringAsync();
            GlobalResult res1 = JsonConvert.DeserializeObject<GlobalResult>(json1);
            userhistoryDTO.userTrackWater = JsonConvert.DeserializeObject<List<UserTrackWater>>(res1.data.ToString());


            //usertrackSpification
            var comp2 = client.GetAsync($"https://user.o2fitt.com/api/v1/UserProfiles/UserSpecificationHistory?userId={userId}&days={days}").Result;
            var json2 = await comp2.Content.ReadAsStringAsync();
            GlobalResult res2 = JsonConvert.DeserializeObject<GlobalResult>(json2);
            userhistoryDTO.userTrackSpecification = JsonConvert.DeserializeObject<List<UserTrackSpecification>>(res2.data.ToString());


            //usertracksleep
            var comp3 = client.GetAsync($"https://workout.o2fitt.com/api/v1/UserTrackSleep/UserHistory?userId={userId}&days={days}").Result;
            var json3 = await comp3.Content.ReadAsStringAsync();
            GlobalResult res3 = JsonConvert.DeserializeObject<GlobalResult>(json3);
            userhistoryDTO.userTrackSleep = JsonConvert.DeserializeObject<List<UserTrackSleepModelDTO>>(res3.data.ToString());


            //usertrackSteps
            var comp4 = client.GetAsync($"https://workout.o2fitt.com/api/v1/UserTrackSteps/UserHistory?userId={userId}&days={days}").Result;
            var json4 = await comp4.Content.ReadAsStringAsync();
            GlobalResult res4 = JsonConvert.DeserializeObject<GlobalResult>(json4);
            userhistoryDTO.userTrackSteps = JsonConvert.DeserializeObject<List<UserTrackSteps>>(res4.data.ToString());

            //usertrackworkouts
            var comp5 = client.GetAsync($"https://workout.o2fitt.com/api/v1/UserTrackWorkout/UserHistory?userId={userId}&days={days}").Result;
            var json5 = await comp5.Content.ReadAsStringAsync();
            GlobalResult res5 = JsonConvert.DeserializeObject<GlobalResult>(json5);
            userhistoryDTO.userTrackWorkout = JsonConvert.DeserializeObject<List<UserTrackWorkOutModelDTO>>(res5.data.ToString());
            //var ackWorkout = JsonConvert.DeserializeObject<List<UserTrackWorkOutModelDTO>>(userhistoryDTO.userTrackWorkout.ToString());

            return userhistoryDTO;
        }




    }
}
