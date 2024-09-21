using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Contracts;
using Data.Repositories;
using FoodStuff.API.Models;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Data.Repositories;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Service.v1.Query;
using FoodStuff.Service.v1.Query.GetFoodFavorite;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.v1.Query;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    public class UserFoodFavoriteController : BaseController
    {
        private readonly IUserFoodFavoriteRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserFoodFavoriteController(IUserFoodFavoriteRepository repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("Get")]
        public async Task<PageResult<GetFoodFavoriteQueryResult>> Get(int UserId, int? Page, int PageSize, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetFoodFavoriteQuery { Page = Page, PageSize = PageSize, UserId = UserId, LanguageName = LanguageName!=null?LanguageName:"Persian" });
        }


        [HttpPost("Create")]
        public async Task<ApiResult<UserFoodFavoriteSelectDTO>> Create(UserFoodFavoriteDTO userFoodFavoriteDTO, CancellationToken cancellationToken)
        {
            UserFoodFavorite userFoodFavorite = userFoodFavoriteDTO.ToEntity(_mapper);
            userFoodFavorite._id = userFoodFavoriteDTO._id;
            userFoodFavorite = await _repository.AddAsync(userFoodFavorite, cancellationToken);
            UserFoodFavoriteSelectDTO result = new UserFoodFavoriteSelectDTO()
            {
                FoodId = userFoodFavorite.FoodId,
                UserId = userFoodFavorite.UserId,
                _id = userFoodFavorite._id
            };
            return result;
        }


        [HttpDelete]
        public async Task<ApiResult> Delete(int foodId,int userId, CancellationToken cancellationToken)
        {
            UserFoodFavorite userFoodFavorite = await _repository.GetByFoodAndUserId(foodId,userId,cancellationToken);
            await _repository.DeleteAsync(userFoodFavorite, cancellationToken);
            return Ok();
        }

    }
}
