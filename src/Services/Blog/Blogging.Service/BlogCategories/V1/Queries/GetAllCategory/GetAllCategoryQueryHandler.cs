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

namespace Blogging.Service.BlogCategories.V1.Queries.GetAllCategory
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<CategoryDto>>, ITransientDependency
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoryQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.TableNoTracking
                .Include(x => x.Title)
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Title = new TranslationDto
                    {
                        Id = x.Title.Id,
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    AltImage = x.AltImage,
                    ImageName = x.ImageName,
                    Type = x.Type
                })
                .ToListAsync(cancellationToken);
        }
    }
}