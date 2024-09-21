using System;
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

namespace Blogging.Service.BlogCategories.V1.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>, ITransientDependency
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;


        public GetCategoryByIdQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _repository.TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.SubCategories).ThenInclude(x => x.Title)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return new CategoryDto
            {
                Id = result.Id,
                Title = new TranslationDto
                {
                    Id = result.Title.Id,
                    Arabic = result.Title.Arabic,
                    English = result.Title.English,
                    Persian = result.Title.Persian
                },
                Type = result.Type,
                AltImage = result.AltImage,
                ImageName = result.ImageName,
                SubCategories = result.SubCategories.Select(x => new SubCategoryBlogDto
                {
                    AltImage = x.AltImage,
                    ImageName = x.ImageName,
                    Title = new TranslationDto
                    {
                        Id = x.Title.Id,
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                }).ToList()
            };
        }
    }
}