using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blogging.Domain.Entities.Blogs;
using Blogging.Service.Dtos;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Service.Blogs.V1.Queries.GetSelected
{
    public class GetSelectedQueryHandler:IRequestHandler<GetSelectedQuery,List<BlogDto>>
    {
        private readonly IRepository<Blog> _repository;

        public GetSelectedQueryHandler(IRepository<Blog> repository)
        {
            _repository = repository;
        } 

        public async Task<List<BlogDto>> Handle(GetSelectedQuery request, CancellationToken cancellationToken)
        {
            // return await _repository.TableNoTracking
            //     .Include(x=>x.Title)
            //     .Include(x=>x.ShortDescription)
            return new List<BlogDto>();
        }
    }
}