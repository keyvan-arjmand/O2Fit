using Common;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.v1.Query.GetFoodFavorite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.v1.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query
{
    public class GetFoodFavoriteQueryHandler : IRequestHandler<GetFoodFavoriteQuery, PageResult<GetFoodFavoriteQueryResult>>, IScopedDependency
    {
        private readonly IUserFoodFavoriteRepository _repository;
        private readonly IMediator _mediator;
        public GetFoodFavoriteQueryHandler(IUserFoodFavoriteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<PageResult<GetFoodFavoriteQueryResult>> Handle(GetFoodFavoriteQuery request, CancellationToken cancellationToken)
        {
            List<GetFoodFavoriteQueryResult> _paging = new List<GetFoodFavoriteQueryResult>();

            if (request.LanguageName == "Persian")
            {
                _paging = await _repository.TableNoTracking
                    .Where(u => u.UserId == request.UserId)
                    .Include(u => u.Food)
                    .ThenInclude(u => u.TranslationName).Select(s =>
                        new GetFoodFavoriteQueryResult
                        {
                            FoodId = s.FoodId,
                            Name = s.Food.TranslationName.Persian,
                            UserId = s.UserId,
                            _id = s._id
                        }).ToListAsync(cancellationToken);
            }
            else
            {
                if (request.LanguageName=="English")
                {
                    _paging = await _repository.TableNoTracking
                        .Where(u => u.UserId == request.UserId)
                        .Include(u => u.Food)
                        .ThenInclude(u => u.TranslationName).Select(s =>
                            new GetFoodFavoriteQueryResult
                            {
                                FoodId = s.FoodId,
                                Name = s.Food.TranslationName.English,
                                UserId = s.UserId,
                                _id = s._id
                            }).ToListAsync(cancellationToken);
                }
                else
                {
                    _paging = await _repository.TableNoTracking
                        .Where(u => u.UserId == request.UserId)
                        .Include(u => u.Food)
                        .ThenInclude(u => u.TranslationName).Select(s =>
                            new GetFoodFavoriteQueryResult
                            {
                                FoodId = s.FoodId,
                                Name = s.Food.TranslationName.Arabic,
                                UserId = s.UserId,
                                _id = s._id
                            }).ToListAsync(cancellationToken);
                }
            }
           

            var result = new PageResult<GetFoodFavoriteQueryResult>
            {
                Count = _paging.Count,
                PageIndex = request.Page ?? 1,
                PageSize = request.PageSize,
                Items = _paging
            };

            return result;
        }
    }
}
