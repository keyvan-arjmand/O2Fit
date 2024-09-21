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

namespace Blogging.Service.SubCategories.V1.Queries.GetSubCategoryById
{
    public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQuery, SubCategoryBlogDto>, ITransientDependency
    {
        private readonly IRepository<SubCategory> _repository;
        private readonly IMapper _mapper;


        public GetSubCategoryByIdQueryHandler(IRepository<SubCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SubCategoryBlogDto> Handle(GetSubCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _repository.TableNoTracking.Include(x => x.Title)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (result == null)
                throw new NotFoundException("SubCategory not found");
            return new SubCategoryBlogDto
            {
                ImageName = result.ImageName,
                CategoryId = result.CategoryId,
                Title = new TranslationDto
                {
                    Id = result.Title.Id,
                    Arabic = result.Title.Arabic,
                    English = result.Title.English,
                    Persian = result.Title.Persian
                },
                AltImage = result.AltImage,
                Id = result.Id
            };
        }
    }
}