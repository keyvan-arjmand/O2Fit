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
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Service.v1.Query.GetFoodAlergy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.v1.Query;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    public class UserFoodAlergyController : BaseController
    {
        private readonly IUserFoodAlergyRepository _alergyrepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserFoodAlergyController( IMapper mapper, IMediator mediator
            , IUserFoodAlergyRepository alergyrepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _alergyrepository = alergyrepository;
        }

        [HttpGet]
        public async Task<ApiResult<List<GetFoodAlergyQueryResult>>> Get(int UserId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetFoodAlergyQuery { UserId = UserId, LanguageName = LanguageName });

            //var IngList =await _alergyrepository.GetlistUserAlergy(UserId,cancellationToken).Include(a=>a.Ingredient).ToListAsync(cancellationToken);
            //var result = new List<UserFoodAlergySelectDTO>();
            //if (IngList.Count()>0)
            //{
            //    List<int> nameIds = new List<int>();
            //    foreach (var item in IngList)
            //    {
            //        nameIds.Add(item.Ingredient.NameId);
            //    }
            //    var translations = await _mediator.Send(new GetTranslationQuery
            //    {
            //        Ids = nameIds,
            //        Language = LanguageName
            //    });
            //    result = IngList.Select(ing => new UserFoodAlergySelectDTO()
            //    {
            //        Id = ing.Id,
            //        IngredientId = ing.IngredientId,
            //        UserId = ing.UserId,
            //        _id = ing._id,
            //        Name = translations.Find(n => n.Value == ing.Ingredient.NameId).Text
            //    }).ToList() ;
            //}
            //return result;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<UserFoodAlergy>> Create(UserFoodAlergyDTO userFoodAlergydto, CancellationToken cancellationToken)
        {
            UserFoodAlergy userFoodAlergy = userFoodAlergydto.ToEntity(_mapper);
            userFoodAlergy._id = userFoodAlergydto._id;
            userFoodAlergy = await _alergyrepository.AddAsync(userFoodAlergy, cancellationToken);
            return userFoodAlergy;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            UserFoodAlergy userFoodAlergy = await _alergyrepository.GetByIdAsync(cancellationToken, id);
            await _alergyrepository.DeleteAsync(userFoodAlergy, cancellationToken);
            return Ok();
        }

    }
    }
