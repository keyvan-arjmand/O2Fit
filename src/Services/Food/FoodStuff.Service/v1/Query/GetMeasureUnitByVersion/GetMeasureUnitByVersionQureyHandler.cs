using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query.GetMeasureUnitByVersion
{
    public class GetMeasureUnitByVersionQureyHandler : IRequestHandler<GetMeasureUnitByVersionQurey, MeasureUnitsAndVersionSelectDTO>, IScopedDependency
    {
        private readonly IRepositoryRedis<List<MeasureUnitModelDTO>> _repositoryRedis;
        private readonly IRepository<MeasureUnit> _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetMeasureUnitByVersionQureyHandler(IRepository<MeasureUnit> repository,
            IRepositoryRedis<List<MeasureUnitModelDTO>> repositoryRedis,
            IMediator mediator, IMapper mapper)
        {
            _repositoryRedis = repositoryRedis;
            _repository = repository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<MeasureUnitsAndVersionSelectDTO> Handle(GetMeasureUnitByVersionQurey request, CancellationToken cancellationToken)
        {
            var listMeasureunit = new List<MeasureUnitModelDTO>();
            listMeasureunit = await _repositoryRedis.GetAsync($"Measureunits_{request.Version+0.01}");
            if (listMeasureunit == null)
            {
               var measureUnits = await _repository.Table
                                .Include(t => t.Translation)
                                .Where(m => m.Version == request.Version+0.01 && m.IsActive == true)
                                .ToListAsync(cancellationToken);
                if (measureUnits.Count() > 0)
                {
                    listMeasureunit = measureUnits.Select(item => new MeasureUnitModelDTO()
                    {
                        Id = item.Id,
                        Value = item.Value,
                        Arabic = item.Translation.Arabic,
                        English = item.Translation.English,
                        Persian = item.Translation.Persian,
                        MeasureUnitCategory = (MeasureUnitCategory)item.MeasureUnitCategory
                    }).ToList();
                    await _repositoryRedis.UpdateAsync($"Measureunits_{request.Version+0.01}", listMeasureunit);
                    request.Version = request.Version + 0.01;
                }
            }
            else
            {
                request.Version = request.Version + 0.01;
            }
            return new MeasureUnitsAndVersionSelectDTO()
            {
                measureUnits = listMeasureunit,
                Version = request.Version
            };
        }
    }
}
