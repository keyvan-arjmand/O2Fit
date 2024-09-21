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

namespace Blogging.Service.Blogs.V1.Queries.GetBlogById
{
    public class GetBlogByIdQueryHandler:IRequestHandler<GetBlogByIdQuery,BlogDto>, ITransientDependency
    {
        private readonly IRepository<Blog> _repository;
        private readonly IMapper _mapper;
        public GetBlogByIdQueryHandler(IRepository<Blog> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BlogDto> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.ShortDescription)
                .Include(x => x.Description)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (result == null)
                throw new NotFoundException("Category not found");
            return _mapper.Map<Blog, BlogDto>(result);
        }
    }
}