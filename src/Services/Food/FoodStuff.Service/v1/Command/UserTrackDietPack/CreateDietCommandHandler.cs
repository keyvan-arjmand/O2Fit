// using Common;
// using Data.Contracts;
// using FoodStuff.Domain.Entities.Diet;
// using MediatR;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using FoodStuff.Common.Utilities;
//
// namespace FoodStuff.Service.v1.Command.Diet
// {
//     public class CreateDietCommandHandler : IRequestHandler<CreateDietCommand, int>, ITransientDependency
//     {
//         
//         private readonly IRepository<DietPack> _dietPackrepository;
//         private readonly IRepository<UserTrackDietPack> _userTrackDietrepository;
//         private readonly IRepositoryRedis<List<UserTrackDietPack>> _repositoryRedis;
//
//         public CreateDietCommandHandler(IRepositoryRedis<List<UserTrackDietPack>> repositoryRedis,
//             IRepository<DietPack> dietPackrepository,
//             IRepository<UserTrackDietPack> userTrackDietrepository)
//         {
//             _repositoryRedis = repositoryRedis;
//             _dietPackrepository = dietPackrepository;
//             _userTrackDietrepository = userTrackDietrepository;
//         }
//
//         public async Task<int> Handle(CreateDietCommand request, CancellationToken cancellationToken)
//         {
//                 List<DateTime> dateLst = new List<DateTime>();
//                 List<DateTime> dateLstZigzagi = new List<DateTime>();
//                 for (int i = 1; i <= 30; i++)
//                 {
//                     var dateTime = request.StartDate.AddDays(i);
//                     string day = DateConvetor.Getday(dateTime);
//                     if (day != "Fri" && day != "Thu")
//                     {
//                         dateLst.Add(dateTime);
//                     }
//                     else
//                     {
//                         dateLstZigzagi.Add(dateTime);
//                     }
//                 }
//             
//                 var breakfast_dinnerCalori = request.Calori * (25 / 100);
//                 var breakfast_dinnerCalori_per = breakfast_dinnerCalori * (3 / 100);
//                
//                 
//                 var lunchCalori = request.Calori * (35 / 100);
//                 var lunchCalori_per = lunchCalori * (3 / 100);
//                 
//                 
//                 var SnackCalori = request.Calori * (5 / 100);
//                 var SnackCalori_per = SnackCalori * (3 / 100);
//                 
//                 //reaKfast and dinner
//                  List<DietPack> breakfastdinnerLst = await _dietPackrepository.Table
//                      .Where(a => a.CaloriValue <= breakfast_dinnerCalori + breakfast_dinnerCalori_per
//                   && a.CaloriValue >= breakfast_dinnerCalori - breakfast_dinnerCalori_per
//                   && a.DietCatego.Id == request.Category
//                   && a.PackCountries.Select(a => a.CountryId).ToList().Contains(request.country)
//                  && a.DietPackAlerges.Any(
//                       item => request.Alergies.Contains(item.IngredientId)))
//                      .ToListAsync(cancellationToken);
//                 
//                  List<DietPack> ZigbreakfastdinnerLst = await _dietPackrepository.Table.Where(a => a.CaloriValue <= Zbreakfast_dinnerCalori + Zbreakfast_dinnerCalori_per
//                        && a.CaloriValue >= Zbreakfast_dinnerCalori - Zbreakfast_dinnerCalori_per
//                         && (int)a.BodyType == request.BodyType
//                        && a.DietCategory.Id == request.Category
//                        && a.PackCountries.Select(a => a.CountryId).ToList().Contains(request.country)
//                       && a.DietPackAlerges.Any(item => request.Alergies.Contains(item.IngredientId))).ToListAsync(cancellationToken);
//                 
//                 
//                  //Snacks
//                  List<DietPack> SnackLst =await _dietPackrepository.Table.Where(a => a.CaloriValue <= SnackCalori + SnackCalori_per
//                     && a.CaloriValue >= SnackCalori - SnackCalori_per
//                     && (int)a.BodyType == request.BodyType
//                     && a.DietCategory.Id == request.Category
//                     && a.PackCountries.Select(a => a.CountryId).ToList().Contains(request.country)
//                    && a.DietPackAlerges.Any(item => request.Alergies.Contains(item.IngredientId))).ToListAsync(cancellationToken);
//                 
//                  List<DietPack> ZigSnackLst =await _dietPackrepository.Table.Where(a => a.CaloriValue <= ZSnackCalori + ZSnackCalori_per
//                       && a.CaloriValue >= ZSnackCalori - ZSnackCalori_per
//                        && (int)a.BodyType == request.BodyType
//                       && a.DietCategory.Id == request.Category
//                       && a.PackCountries.Select(a => a.CountryId).ToList().Contains(request.country)
//                      && a.DietPackAlerges.Any(item => request.Alergies.Contains(item.IngredientId))).ToListAsync(cancellationToken);
//                 
//                  Random random = new Random();
//                 
//                 
//                  //breakfast
//                  List<DietPack> breakfastLst = breakfastdinnerLst.Where(a => (int)a.FoodMeal == 1)
//                      .OrderBy(x => random.Next()).Take(22).ToList();
//                  List<DietPack> ZigbreakfastLst = ZigbreakfastdinnerLst.Where(a => (int)a.FoodMeal == 1)
//                      .OrderBy(x => random.Next()).Take(8).ToList();
//                 
//                 
//                  //lunch
//                  List<DietPack> lunchLst = await _dietPackrepository.Table.Where(a => a.CaloriValue <= lunchCalori + lunchCalori_per
//                       && a.CaloriValue >= lunchCalori - lunchCalori_per
//                       && (int)a.BodyType == request.BodyType
//                       && (int)a.FoodMeal == 2
//                       && a.DietCategory.Id == request.Category
//                       && a.PackCountries.Select(a => a.CountryId).ToList().Contains(request.country)
//                      && a.DietPackAlerges.Any(item => request.Alergies.Contains(item.IngredientId)))
//                      .OrderBy(x => random.Next()).Take(22).ToListAsync();
//                 
//                 
//             
//             
//                 //dinner
//                 List<DietPack> dinnerLst = breakfastdinnerLst.Where(a => (int)a.FoodMeal == 4)
//                     .OrderBy(x => random.Next()).Take(22).ToList();
//
//                 List<DietPack> ZigdinnerLst = ZigbreakfastdinnerLst
//                     .Where(a => (int)a.FoodMeal == 4)
//                     .OrderBy(x => random.Next()).Take(8).ToList();
//             
//             
//             
//             
//                 //MorningSnack
//                 List<DietPack> morningSnackLst = SnackLst.Where(a => (int)a.FoodMeal == 5)
//                     .OrderBy(x => random.Next()).Take(22).ToList().ToList();
//                 List<DietPack> ZigmorningSnackLst = ZigSnackLst.Where(a => (int)a.FoodMeal == 5)
//                     .OrderBy(x => random.Next()).Take(8).ToList();
//             
//             
//             
//                 //NoonSnack
//                 List<DietPack> noonSnackLst = SnackLst.Where(a => (int)a.FoodMeal == 6)
//                     .OrderBy(x => random.Next()).Take(22).ToList();
//                 List<DietPack> ZignoonSnackLst = ZigSnackLst.Where(a => (int)a.FoodMeal == 6)
//                     .OrderBy(x => random.Next()).Take(8).ToList();
//             
//             
//                 //NightSnack
//                 List<DietPack> nightSnackSnackLst = SnackLst.Where(a => (int)a.FoodMeal == 7)
//                     .OrderBy(x => random.Next()).Take(22).ToList().ToList();
//                 List<DietPack> ZignightSnackLst = ZigSnackLst.Where(a => (int)a.FoodMeal == 7)
//                     .OrderBy(x => random.Next()).Take(8).ToList();
//             
//             
//                 List<UserTrackDietPack> userTrackDietPackList = new List<UserTrackDietPack>();
//                 int n = 0;
//                 foreach (var date in dateLst)
//                 {
//                     AddUsertrackDietPack(date, request.UserId, breakfastLst[n], userTrackDietPackList);
//                     AddUsertrackDietPack(date, request.UserId, lunchLst[n], userTrackDietPackList);
//                     AddUsertrackDietPack(date, request.UserId, dinnerLst[n], userTrackDietPackList);
//                     AddUsertrackDietPack(date, request.UserId, morningSnackLst[n], userTrackDietPackList);
//                     AddUsertrackDietPack(date, request.UserId, noonSnackLst[n], userTrackDietPackList);
//                     AddUsertrackDietPack(date, request.UserId, nightSnackSnackLst[n], userTrackDietPackList);
//                     n++;
//                 }
//             
//                 int z = 0;
//                 foreach (var date in dateLst)
//                 {
//                     AddUsertrackDietPack(date, request.UserId, ZigbreakfastLst[n], userTrackDietPackList);
//                     AddUsertrackDietPack(date, request.UserId, ZiglunchLst[n], userTrackDietPackList);
//                     AddUsertrackDietPack(date, request.UserId, ZigdinnerLst[n], userTrackDietPackList);
//                     AddUsertrackDietPack(date, request.UserId, ZigmorningSnackLst[n], userTrackDietPackList);
//                     AddUsertrackDietPack(date, request.UserId, ZignoonSnackLst[n], userTrackDietPackList);
//                     AddUsertrackDietPack(date, request.UserId, ZignightSnackLst[n], userTrackDietPackList);
//                     z++;
//                 }
//             
//             
//                 await _userTrackDietrepository.AddRangeAsync(userTrackDietPackList,cancellationToken);
//                 await _repositoryRedis.UpdateAsync($"Diet_{request.UserId}", userTrackDietPackList);
//                 return 0;
//                 
//             }
//
//             
//         
//
//         public List<UserTrackDietPack> AddUsertrackDietPack(DateTime date, int userId, DietPack dietPacks,
//                 List<UserTrackDietPack> userTrackDietPackList)
//             {
//
//
//
//                 UserTrackDietPack userTrackDietPack = new UserTrackDietPack
//                 {
//                     Date = date,
//                     UserId = userId,
//                     DietPack = dietPacks
//                 };
//                 userTrackDietPackList.Add(userTrackDietPack);
//
//
//                 return null;
//             }
//         }
//     }
//
