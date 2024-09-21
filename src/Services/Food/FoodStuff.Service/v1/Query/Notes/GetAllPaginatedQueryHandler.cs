using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Query.Notes
{
    public class GetAllPaginatedQueryHandler: IRequestHandler<GetAllPaginatedWithUserIdQuery, PageResult<NoteDto>>, IScopedDependency
    {
        private readonly IRepository<Note> _repository;
        private readonly IMapper _mapper;
        public GetAllPaginatedQueryHandler(IRepository<Note> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PageResult<NoteDto>> Handle(GetAllPaginatedWithUserIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.TableNoTracking.Where(x=>x.UserId == request.UserId).Skip((request.PageIndex -1) * request.PageSize).Take(request.PageSize)
                .ToListAsync(cancellationToken);
            var result = _mapper.Map<List<NoteDto>>(data);

            var paginateResult = new PageResult<NoteDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Count = data.Count,
                Items = result
            };
            return paginateResult;
        }
    }
}