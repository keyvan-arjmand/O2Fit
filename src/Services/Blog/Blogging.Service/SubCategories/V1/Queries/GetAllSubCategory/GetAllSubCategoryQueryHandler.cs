using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blogging.Domain.Entities.Blogs;
using Blogging.Service.Dtos;
using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Service.SubCategories.V1.Queries.GetAllSubCategory
{
    public class GetAllSubCategoryQueryHandler : IRequestHandler<GetAllSubCategoryQuery, List<SubCategoryBlogDto>>, ITransientDependency
    {
        private readonly IRepository<SubCategory> _repository;
        private readonly IMapper _mapper;

        public GetAllSubCategoryQueryHandler(IRepository<SubCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SubCategoryBlogDto>> Handle(GetAllSubCategoryQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.TableNoTracking
                .Include(x => x.Title)
                .Select(x => new SubCategoryBlogDto
                {
                    ImageName = x.ImageName,
                    CategoryId = x.CategoryId,
                    Title = new TranslationDto
                    {
                        Id = x.Title.Id,
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    AltImage = x.AltImage,
                    Id = x.Id
                })
                .ToListAsync(cancellationToken);
        }
    }
}