using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query.DietPackQuery
{
    public class GetAllDietPackQueryHandler : IRequestHandler<GetAllDietPackQuery, PageResult<GetAllDietPackDTO>>, IScopedDependency
    {
        private readonly IRepository<DietPack> _repository;


        public GetAllDietPackQueryHandler(IRepository<DietPack> repository)
        {
            _repository = repository;

        }

        public async Task<PageResult<GetAllDietPackDTO>> Handle(GetAllDietPackQuery request, CancellationToken cancellationToken)
        {
            List<GetAllDietPackDTO> getAllDietPack = new List<GetAllDietPackDTO>();
            var minCalorie = request.DailyCalorie - (request.DailyCalorie * 3 / 100);
            var maxCalorie = request.DailyCalorie + (request.DailyCalorie * 3 / 100);

            if (string.IsNullOrEmpty(request.Name) && request.DailyCalorie == 0 && request.DietCategoryId == 0 && request.Meal == null)
            {
                getAllDietPack = await _repository.TableNoTracking.Include(n => n.Name)
                            .Include(dc => dc.DietPackDietCategories)
                            .OrderByDescending(b => b.Id)
                            .Skip((request.Page - 1 ?? 0) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(s => new GetAllDietPackDTO
                            {
                                CaloriValue = s.CaloriValue,
                                DailyCalorie = s.DailyCalorie,
                                FoodMeal = (int)s.FoodMeal,
                                Id = s.Id,
                                IsActive = s.IsActive,
                                PackageName = s.Name.Persian,
                                DietCategoryIds = s.DietPackDietCategories.Select(dc => dc.DietCategoryId).ToList(),
                                NationalityIds = s.DietPackNationalities.Select(s => s.NationalityId).ToList(),

                            })
                            .ToListAsync(cancellationToken);
            }
            else
            {
                if (request.DietCategoryId == 0)
                {
                    throw new AppException("DietCategoryId Is Required");
                }

                if (string.IsNullOrEmpty(request.Name) && request.DailyCalorie == 0 && request.Meal == null)
                {
                    getAllDietPack = await _repository.TableNoTracking
                    .Include(n => n.Name)
                    .Include(dc => dc.DietPackDietCategories)
                    .ThenInclude(dc => dc.DietCategory)
                    .Where(d => d.DietPackDietCategories.Any(dc => dc.DietCategoryId == request.DietCategoryId))
                   .Select(s => new GetAllDietPackDTO
                   {
                       CaloriValue = s.CaloriValue,
                       DailyCalorie = s.DailyCalorie,
                       FoodMeal = (int)s.FoodMeal,
                       Id = s.Id,
                       IsActive = s.IsActive,
                       PackageName = s.Name.Persian,
                       DietCategoryIds = s.DietPackDietCategories.Select(dc => dc.DietCategoryId).ToList(),
                       NationalityIds = s.DietPackNationalities.Select(n => n.NationalityId).ToList(),

                   })
                   .OrderByDescending(b => b.Id)
                   .Skip((request.Page - 1 ?? 0) * request.PageSize)
                   .Take(request.PageSize)
                   .ToListAsync(cancellationToken);
                }
                else
                {
                    if (!string.IsNullOrEmpty(request.Name) && request.DailyCalorie > 0 && request.Meal != null)
                    {

                        getAllDietPack = await _repository.TableNoTracking
                                            .Include(n => n.Name)
                                            .Include(dc => dc.DietPackDietCategories)
                                            .ThenInclude(dc => dc.DietCategory)
                                       .Where(d => d.FoodMeal == (FoodMeal)request.Meal && d.DailyCalorie >= minCalorie &&
                                       d.DailyCalorie <= maxCalorie && d.Name.Persian.Contains(request.Name) &&
                                       d.DietPackDietCategories.Any(dc => dc.DietCategoryId == request.DietCategoryId))
                                           .Select(s => new GetAllDietPackDTO
                                           {
                                               CaloriValue = s.CaloriValue,
                                               DailyCalorie = s.DailyCalorie,
                                               FoodMeal = (int)s.FoodMeal,
                                               Id = s.Id,
                                               IsActive = s.IsActive,
                                               PackageName = s.Name.Persian,
                                               DietCategoryIds = s.DietPackDietCategories.Select(dc => dc.DietCategoryId).ToList(),
                                               NationalityIds = s.DietPackNationalities.Select(n => n.NationalityId).ToList(),

                                           })
                                           .OrderByDescending(b => b.Id)
                                           .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                           .Take(request.PageSize)
                                           .ToListAsync(cancellationToken);


                    }
                    else
                    {
                        if (request.Meal != null && request.DailyCalorie > 0)
                        {

                            getAllDietPack = await _repository.TableNoTracking
                                                .Include(n => n.Name)
                                                .Include(dc => dc.DietPackDietCategories)
                                                .ThenInclude(dc => dc.DietCategory)
                                           .Where(d => d.DailyCalorie >= minCalorie &&
                                           d.DailyCalorie <= maxCalorie && d.FoodMeal == (FoodMeal)request.Meal &&
                                           d.DietPackDietCategories.Any(dc => dc.DietCategoryId == request.DietCategoryId))

                                               .OrderByDescending(b => b.Id)
                                               .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                               .Take(request.PageSize)
                                               .Select(s => new GetAllDietPackDTO
                                               {
                                                   CaloriValue = s.CaloriValue,
                                                   DailyCalorie = s.DailyCalorie,
                                                   FoodMeal = (int)s.FoodMeal,
                                                   Id = s.Id,
                                                   IsActive = s.IsActive,
                                                   PackageName = s.Name.Persian,
                                                   DietCategoryIds = s.DietPackDietCategories.Select(dc => dc.DietCategoryId).ToList(),
                                                   NationalityIds = s.DietPackNationalities.Select(n => n.NationalityId).ToList(),

                                               })
                                               .ToListAsync(cancellationToken);


                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(request.Name) && request.DailyCalorie > 0)
                            {

                                getAllDietPack = await _repository.TableNoTracking
                                                    .Include(n => n.Name)
                                                    .Include(dc => dc.DietPackDietCategories)
                                                    .ThenInclude(dc => dc.DietCategory)
                                               .Where(d => d.DailyCalorie >= minCalorie &&
                                               d.DailyCalorie <= maxCalorie && d.Name.Persian.Contains(request.Name) &&
                                               d.DietPackDietCategories.Any(dc => dc.DietCategoryId == request.DietCategoryId))

                                                   .OrderByDescending(b => b.Id)
                                                   .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                                   .Take(request.PageSize)
                                                   .Select(s => new GetAllDietPackDTO
                                                   {
                                                       CaloriValue = s.CaloriValue,
                                                       DailyCalorie = s.DailyCalorie,
                                                       FoodMeal = (int)s.FoodMeal,
                                                       Id = s.Id,
                                                       IsActive = s.IsActive,
                                                       PackageName = s.Name.Persian,
                                                       DietCategoryIds = s.DietPackDietCategories.Select(dc => dc.DietCategoryId).ToList(),
                                                       NationalityIds = s.DietPackNationalities.Select(n => n.NationalityId).ToList(),

                                                   })
                                                   .ToListAsync(cancellationToken);


                            }

                            else
                            {
                                if (!string.IsNullOrEmpty(request.Name) && request.Meal != null)
                                {

                                    getAllDietPack = await _repository.TableNoTracking
                                                        .Include(n => n.Name)
                                                        .Include(dc => dc.DietPackDietCategories)
                                                        .ThenInclude(dc => dc.DietCategory)
                                                   .Where(d => d.FoodMeal == (FoodMeal)request.Meal && d.Name.Persian.Contains(request.Name) &&
                                                   d.DietPackDietCategories.Any(dc => dc.DietCategoryId == request.DietCategoryId))

                                                       .OrderByDescending(b => b.Id)
                                                       .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                                       .Take(request.PageSize)
                                                       .Select(s => new GetAllDietPackDTO
                                                       {
                                                           CaloriValue = s.CaloriValue,
                                                           DailyCalorie = s.DailyCalorie,
                                                           FoodMeal = (int)s.FoodMeal,
                                                           Id = s.Id,
                                                           IsActive = s.IsActive,
                                                           PackageName = s.Name.Persian,
                                                           DietCategoryIds = s.DietPackDietCategories.Select(dc => dc.DietCategoryId).ToList(),
                                                           NationalityIds = s.DietPackNationalities.Select(n => n.NationalityId).ToList(),

                                                       })
                                                       .ToListAsync(cancellationToken);


                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(request.Name))
                                    {

                                        getAllDietPack = await _repository.TableNoTracking
                                                            .Include(n => n.Name)
                                                            .Include(dc => dc.DietPackDietCategories)
                                                            .ThenInclude(dc => dc.DietCategory)
                                                       .Where(d => d.Name.Persian.Contains(request.Name) &&
                                                       d.DietPackDietCategories.Any(dc => dc.DietCategoryId == request.DietCategoryId))

                                                           .OrderByDescending(b => b.Id)
                                                           .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                                           .Take(request.PageSize)
                                                           .Select(s => new GetAllDietPackDTO
                                                           {
                                                               CaloriValue = s.CaloriValue,
                                                               DailyCalorie = s.DailyCalorie,
                                                               FoodMeal = (int)s.FoodMeal,
                                                               Id = s.Id,
                                                               IsActive = s.IsActive,
                                                               PackageName = s.Name.Persian,
                                                               DietCategoryIds = s.DietPackDietCategories.Select(dc => dc.DietCategoryId).ToList(),
                                                               NationalityIds = s.DietPackNationalities.Select(n => n.NationalityId).ToList(),

                                                           })
                                                           .ToListAsync(cancellationToken);


                                    }
                                    else
                                    {
                                        if (request.DailyCalorie > 0)
                                        {

                                            getAllDietPack = await _repository.TableNoTracking
                                                                .Include(n => n.Name)
                                                                .Include(dc => dc.DietPackDietCategories)
                                                                .ThenInclude(dc => dc.DietCategory)
                                                           .Where(d => d.DailyCalorie >= minCalorie &&
                                                           d.DailyCalorie <= maxCalorie &&
                                                           d.DietPackDietCategories.Any(dc => dc.DietCategoryId == request.DietCategoryId))

                                                               .OrderByDescending(b => b.Id)
                                                               .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                                               .Take(request.PageSize)
                                                               .Select(s => new GetAllDietPackDTO
                                                               {
                                                                   CaloriValue = s.CaloriValue,
                                                                   DailyCalorie = s.DailyCalorie,
                                                                   FoodMeal = (int)s.FoodMeal,
                                                                   Id = s.Id,
                                                                   IsActive = s.IsActive,
                                                                   PackageName = s.Name.Persian,
                                                                   DietCategoryIds = s.DietPackDietCategories.Select(dc => dc.DietCategoryId).ToList(),
                                                                   NationalityIds = s.DietPackNationalities.Select(n => n.NationalityId).ToList(),

                                                               })
                                                               .ToListAsync(cancellationToken);


                                        }
                                        else
                                        {
                                            if (request.Meal != null)
                                            {

                                                getAllDietPack = await _repository.TableNoTracking
                                                                    .Include(n => n.Name)
                                                                    .Include(dc => dc.DietPackDietCategories)
                                                                    .ThenInclude(dc => dc.DietCategory)
                                                               .Where(d => d.FoodMeal == (FoodMeal)request.Meal &&
                                                               d.DietPackDietCategories.Any(dc => dc.DietCategoryId == request.DietCategoryId))
                                                                   .Select(s => new GetAllDietPackDTO
                                                                   {
                                                                       CaloriValue = s.CaloriValue,
                                                                       DailyCalorie = s.DailyCalorie,
                                                                       FoodMeal = (int)s.FoodMeal,
                                                                       Id = s.Id,
                                                                       IsActive = s.IsActive,
                                                                       PackageName = s.Name.Persian,
                                                                       DietCategoryIds = s.DietPackDietCategories.Select(dc => dc.DietCategoryId).ToList(),
                                                                       NationalityIds = s.DietPackNationalities.Select(n => n.NationalityId).ToList(),

                                                                   })
                                                                   .OrderByDescending(b => b.Id)
                                                                   .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                                                   .Take(request.PageSize)
                                                                   .ToListAsync(cancellationToken);


                                            }
                                        }
                                    }
                                }


                            }
                        }




                    }
                }

            }




            PageResult<GetAllDietPackDTO> result = new PageResult<GetAllDietPackDTO>
            {
                Items = getAllDietPack,
                Count = getAllDietPack.Count,
                PageSize = request.PageSize,
                PageIndex = (int)(request.Page != null ? request.Page : 0)

            };

            return result;
        }
    }
}
