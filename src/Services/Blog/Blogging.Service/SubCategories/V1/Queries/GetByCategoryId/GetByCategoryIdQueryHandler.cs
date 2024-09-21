using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blogging.Domain.Entities.Blogs;
using Blogging.Service.Dtos;
using Common;
using Common.Exceptions;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Service.SubCategories.V1.Queries.GetByCategoryId
{
    public class GetByCategoryIdQueryHandler : IRequestHandler<GetByCategoryIdQuery, List<SubCategoryBlogDto>>, ITransientDependency
    {
        private readonly IRepository<SubCategory> _repository;
        private readonly IMapper _mapper;

        public GetByCategoryIdQueryHandler(IRepository<SubCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SubCategoryBlogDto>> Handle(GetByCategoryIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _repository.TableNoTracking
                .Include(x=>x.Title)
                .Where(x => x.CategoryId == request.Id)
                .ToListAsync(cancellationToken);

            return result.Select(x => new SubCategoryBlogDto
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
            }).ToList();
        }
    }
}