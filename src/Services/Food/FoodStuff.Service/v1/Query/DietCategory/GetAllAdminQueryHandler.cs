using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Query.DietCategory
{
    public class GetAllAdminQueryHandler : IRequestHandler<GetAllAdminQuery, PageResult<DietCategoryResultDto>>
        , IScopedDependency
    {

        private readonly IRepository<Domain.Entities.Diet.DietCategory> _repository;
        private readonly IMapper _mapper;
        public GetAllAdminQueryHandler(IRepository<Domain.Entities.Diet.DietCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PageResult<DietCategoryResultDto>> Handle(GetAllAdminQuery request, CancellationToken cancellationToken)
        {
            var listCategories = await _repository.Table.Include(
                    m => m.DescriptionTranslation)
                .Include(a => a.NameTranslation)
                .OrderBy(i => i.Id)
                .Take(request.PageSize).Skip((request.Page - 1 ?? 0) * request.PageSize)
                .ToListAsync(cancellationToken);

            var countDetails = listCategories.Count();
            List<DietCategoryResultDto> DietCategorys =
                _mapper.Map<List<Domain.Entities.Diet.DietCategory>, List<DietCategoryResultDto>>(listCategories);


            foreach (var dietCategory in DietCategorys.Where(x=>x.ParentId!=null))
            {
                if (dietCategory.ParentId > 0 || dietCategory.ParentId != null)
                {
                    dietCategory.ParentCategory = _mapper.Map<Domain.Entities.Diet.DietCategory, DietCategoryResultDto>(
                        await
                            _repository.TableNoTracking
                                .Include(t => t.NameTranslation)
                                .Include(a => a.DescriptionTranslation)
                                .FirstOrDefaultAsync(x => x.Id == dietCategory.ParentId,
                                cancellationToken));
                }
            }
            return new PageResult<DietCategoryResultDto>
            {
                Count = countDetails,
                PageIndex = request.Page ?? 1,
                PageSize = request.PageSize,
                Items = DietCategorys
            };
        }
    }
}