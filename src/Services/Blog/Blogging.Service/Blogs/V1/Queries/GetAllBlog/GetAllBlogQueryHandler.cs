using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blogging.Domain.Entities.Blogs;
using Blogging.Domain.Enum;
using Blogging.Service.Dtos;
using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Service.Blogs.V1.Queries.GetAllBlog
{
    public class GetAllBlogQueryHandler : IRequestHandler<GetAllBlogQuery, List<BlogDto>>, ITransientDependency
    {
        private readonly IRepository<Blog> _repository;
        private readonly IMapper _mapper;

          
        public GetAllBlogQueryHandler(IRepository<Blog> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BlogDto>> Handle(GetAllBlogQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository
                .TableNoTracking
                .Include(x=>x.Description)
                .Include(x=>x.Title)
                .Include(x=>x.ShortDescription)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<Blog>, List<BlogDto>>(result);
        }
    }
}