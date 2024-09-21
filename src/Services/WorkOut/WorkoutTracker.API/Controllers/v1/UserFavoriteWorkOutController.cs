using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using WorkoutTracker.API.Models;
using WorkoutTracker.Common.Utilities;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;
using Service.v1.Query;

namespace WorkoutTracker.API.Controllers.v1
{
    [ApiVersion("1")]
    public class UserFavoriteWorkOutController : BaseController
    {
        private readonly IUserFavoriteWorkOutRepository _userfaveritrepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserFavoriteWorkOutController(IMapper mapper, IMediator mediator
            , IUserFavoriteWorkOutRepository userfaveritrepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userfaveritrepository = userfaveritrepository;
        }


        [HttpGet("Get")]
        public async Task<PageResult<UserFavoriteWorkOutSelectDTO>> Get(int UserId, int? Page, int PageSize, CancellationToken cancellationToken)
        {
            var listfoodId = _userfaveritrepository.GetUserFavorites(UserId, cancellationToken);
            List<int> nameids = new List<int>();
            foreach (var item in listfoodId)
            {
                nameids.Add(item.WorkOut.NameId);
            }
            //translations
            var translations = await _mediator.Send(new GetTranslationQuery
            {
                Ids = nameids,
                Language = LanguageName
            });

            var countDetails = _userfaveritrepository.TableNoTracking.Count();

            List<UserFavoriteWorkOut> _foods = await listfoodId
                                           .Skip((Page - 1 ?? 0) * PageSize)
                                           .Take(PageSize)
                                           .ToListAsync(cancellationToken);

            List<UserFavoriteWorkOutSelectDTO> _paging = new List<UserFavoriteWorkOutSelectDTO>();

            if (_foods.Count > 0)
            {
                foreach (var item in _foods)
                {
                    UserFavoriteWorkOutSelectDTO invoicePaging = new UserFavoriteWorkOutSelectDTO()
                    {
                        Name = translations.Find(n => n.Value == item.WorkOut.NameId).Text,
                        _id = item._id,
                        UserId = item.UserId,
                        WorkOutId = item.WorkOutId,
                        clasification=item.WorkOut.Classification
                    };
                    _paging.Add(invoicePaging);
                }
            }
            var result = new PageResult<UserFavoriteWorkOutSelectDTO>
            {
                Count = countDetails,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = _paging
            };
            return result;
        }


        [HttpPost("Create")]
        public async Task<ApiResult> Create(UserFavoriteWorkOutDTO userFavoriteWorkOutDTO, CancellationToken cancellationToken)
        {
            UserFavoriteWorkOut userFavoriteWorkOut = userFavoriteWorkOutDTO.ToEntity(_mapper);
            userFavoriteWorkOut._id = userFavoriteWorkOutDTO._id;
            await _userfaveritrepository.AddAsync(userFavoriteWorkOut, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            UserFavoriteWorkOut userFavoriteWorkOut = await _userfaveritrepository.GetByIdAsync(cancellationToken, id);
            await _userfaveritrepository.DeleteAsync(userFavoriteWorkOut, cancellationToken);
            return Ok();
        }
    }
}
