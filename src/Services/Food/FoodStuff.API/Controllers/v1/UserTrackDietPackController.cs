using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.API.Models.DTOs;
using FoodStuff.API.Models.ViewModels;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Service.v1.Command.UserTrackDietPack;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    public class UserTrackDietPackController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IRepository<UserTrackDietPack> _repository;

        public UserTrackDietPackController(IMediator mediator, IRepository<UserTrackDietPack> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResult> Post(
            CreateUserTrackDietPackDTO createUserTrackDietPackDto)
        {
            await _mediator.Send(new CreateUserTrackDietPackCommand
            {
                NutritionistId = createUserTrackDietPackDto.NutritionistId,
                UserId = createUserTrackDietPackDto.UserId,
                DailyCalorie = createUserTrackDietPackDto.DailyCalorie,
                UserTrackDietPacks = createUserTrackDietPackDto.UserTrackDietPacks,
                StartDate = createUserTrackDietPackDto.StartDate,
                EndDate = createUserTrackDietPackDto.EndDate,
                PackageName = createUserTrackDietPackDto.PackageName

            });

            return new ApiResult(
                true, ApiResultStatusCode.Success
            );
        }

        [HttpGet("GetDietPackUser")]
        public async Task<DietPackUserViewModel> GetDietPackUser(int userId, CancellationToken cancellationToken)
        {
            var result = await _repository.Table
                .Include(d => d.UserTrackDietPackDetails)
                .Where(w => w.UserId == userId)
                .Select(s => new DietPackUserViewModel
                {
                    PackageName = s.PackageName,
                    DailyCalorie = s.DailyCalorie,
                    UserTrackDietPackDetails = s.UserTrackDietPackDetails
                        .Where(x => x.UserTrackDietPackId == s.Id)
                        .Select(u => new UserTrackDietPackDetailViewModel
                        {
                            DietPackId = u.DietPackId,
                            Meal = u.Meal,
                            Repeat = u.Repeat
                        }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
            if(result != null)
                return result;

            throw new AppException(ApiResultStatusCode.NotFound,"Not Found");
        }

        // [HttpGet("GetAllAdmin")]
        // [Authorize (Roles = "Admin")]
        // public async Task<List<UserTrackDietPack>> GetAllAdmin()
        // {
        //     var result = await _repository.TableNoTracking.Select(s=>).ToListAsync();
        //
        //     return result;
        //
        // }
    }
}
