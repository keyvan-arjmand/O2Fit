using Data.Contracts;
using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using User.API.Models;
using User.Common;
using User.Domain.Entities.User;
using WebFramework.Api;

namespace User.API.Controllers.v1
{
    [ApiVersion("1")]
    public class DeviceInfoController : BaseController
    {
        private readonly IRepository<DeviceInformation> _deviceInfoRepository;
        private readonly IRepository<UserProfile> _userProfilerepository;

        public DeviceInfoController(IRepository<DeviceInformation> deviceInfoRepository,
            IRepository<UserProfile> userProfilerepository)
        {
            _deviceInfoRepository = deviceInfoRepository;
            _userProfilerepository = userProfilerepository;
        }


        [Route("MarketInfoByDate")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<DeviceInfoInLimitDateViewModel> MarketInfoByDateAsync(DateTime date)
        {

            DeviceInfoInLimitDateViewModel deviceInfoInLimitDateViewModel = new DeviceInfoInLimitDateViewModel();

            deviceInfoInLimitDateViewModel.googlePlay = _deviceInfoRepository.TableNoTracking
                .Count(u => u.CreateDate.Date == date.Date
                && (u.Market == "googlePlay"));

            deviceInfoInLimitDateViewModel.googlePlayProfileCompleted = _deviceInfoRepository.TableNoTracking
                .Count(u => u.CreateDate.Date == date.Date
                && (u.Market == "googlePlay") && u.IsProfileComplete);

            //deviceInfoInLimitDateViewModel.googlePlayPurchase = _deviceInfoRepository.TableNoTracking
            //    .Count(u => u.CreateDate.Date == date.Date
            //    && (u.Market == "googlePlay") && u.IsPurchase);

            deviceInfoInLimitDateViewModel.web = _deviceInfoRepository.TableNoTracking
                .Count(u => u.CreateDate.Date == date.Date && u.Market == "web");

            deviceInfoInLimitDateViewModel.webProfileCompleted = _deviceInfoRepository.TableNoTracking
                .Count(u => u.CreateDate.Date == date.Date && u.Market == "web" && u.IsProfileComplete);

            //deviceInfoInLimitDateViewModel.webPurchase = _deviceInfoRepository.TableNoTracking
            //    .Count(u => u.CreateDate.Date == date.Date && u.Market == "web" && u.IsPurchase);

            deviceInfoInLimitDateViewModel.myket = _deviceInfoRepository.TableNoTracking
                 .Count(u => u.CreateDate.Date == date.Date && u.Market == "myket");

            deviceInfoInLimitDateViewModel.myketProfileCompleted = _deviceInfoRepository.TableNoTracking
                .Count(u => u.CreateDate.Date == date.Date && u.Market == "myket" && u.IsProfileComplete);

            //deviceInfoInLimitDateViewModel.myketPurchase = _deviceInfoRepository.TableNoTracking
            //    .Count(u => u.CreateDate.Date == date.Date && u.Market == "myket" && u.IsPurchase);

            deviceInfoInLimitDateViewModel.appStore = _deviceInfoRepository.TableNoTracking
                .Count(u => u.CreateDate.Date == date.Date && u.Market == "app store");

            deviceInfoInLimitDateViewModel.appStoreProfileCompleted = _deviceInfoRepository.TableNoTracking
                .Count(u => u.CreateDate.Date == date.Date && u.Market == "app store" && u.IsProfileComplete);

            //deviceInfoInLimitDateViewModel.appStorePurchase = _deviceInfoRepository.TableNoTracking
            //    .Count(u => u.CreateDate.Date == date.Date && u.Market == "app store" && u.IsPurchase);

            deviceInfoInLimitDateViewModel.cafeBazar = _deviceInfoRepository.TableNoTracking
                .Count(u => u.CreateDate.Date == date.Date && u.Market == "cafe bazar");


            deviceInfoInLimitDateViewModel.cafeBazarProfileCompleted = _deviceInfoRepository.TableNoTracking
                .Count(u => u.CreateDate.Date == date.Date && u.Market == "cafe bazar" && u.IsProfileComplete);

            //deviceInfoInLimitDateViewModel.cafeBazarPurchase = _deviceInfoRepository.TableNoTracking
            //    .Count(u => u.CreateDate.Date == date.Date && u.Market == "cafe bazar" && u.IsPurchase);

            deviceInfoInLimitDateViewModel.AllProfileCompleted = _userProfilerepository.TableNoTracking
                .Count(u => u.CreateDate.Date == date.Date);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://ordertest.o2fitt.com/");


            var token = await HttpContext.GetTokenAsync("access_token");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = httpClient.GetAsync($"/api/v1/Order/GetOrdersCountByDate?datetime={date}").Result;
            var json = await response.Content.ReadAsStringAsync();
            GlobalResult res = JsonConvert.DeserializeObject<GlobalResult>(json);
            var orderCount = JsonConvert.DeserializeObject<OrderCountDto>(res.data.ToString());

            var AmountResponse = httpClient.GetAsync($"/api/v1/Order/GetDailySalesAmount?datetime={date}").Result;
            var Amountjson = await AmountResponse.Content.ReadAsStringAsync();
            GlobalResult Amountres = JsonConvert.DeserializeObject<GlobalResult>(Amountjson);
            var Amount = JsonConvert.DeserializeObject<OrderAmountDTO>(Amountres.data.ToString());


            var UserIdsResponse = httpClient.GetAsync($"/api/v1/Order/GetUserIdByDate?datetime={date}").Result;
            var UserIdsjson = await UserIdsResponse.Content.ReadAsStringAsync();
            GlobalResult UserIdsres = JsonConvert.DeserializeObject<GlobalResult>(UserIdsjson);
            var UserIds = JsonConvert.DeserializeObject<List<int>>(UserIdsres.data.ToString());

            foreach (var item in UserIds)
            {
                var market = await _deviceInfoRepository.TableNoTracking.Where(d => d.UserId == item).OrderByDescending(d => d.Id).Select(s => s.Market).FirstOrDefaultAsync();

                switch (market)
                {
                    case "googlePlay":
                        deviceInfoInLimitDateViewModel.googlePlayPurchase++;
                        break;
                    case "web":
                        deviceInfoInLimitDateViewModel.webPurchase++;
                        break;
                    case "myket":
                        deviceInfoInLimitDateViewModel.myketPurchase++;
                        break;
                    case "app store":
                        deviceInfoInLimitDateViewModel.appStorePurchase++;
                        break;
                    case "cafe bazar":
                        deviceInfoInLimitDateViewModel.cafeBazarPurchase++;
                        break;
                    default:
                        break;
                }
            }

            deviceInfoInLimitDateViewModel.AllPurchase = orderCount.OrderCount;
            deviceInfoInLimitDateViewModel.AllSuccessPurchase = orderCount.OrderSuccessCount;
            deviceInfoInLimitDateViewModel.OrderAmountDTO = Amount;
            return deviceInfoInLimitDateViewModel;

        }
    }
}
