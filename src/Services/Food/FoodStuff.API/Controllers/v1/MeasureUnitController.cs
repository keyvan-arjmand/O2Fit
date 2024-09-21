using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Contracts;
using FoodStuff.API.Models;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Enum;
using Service.v1.Command;
using Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using Microsoft.EntityFrameworkCore.Internal;
using AutoMapper.QueryableExtensions;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Service.Models;
using FoodStuff.Service.v1.Query.GetMeasureUnitByVersion;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    public class MeasureUnitController : BaseController
    {
        private readonly IRepository<MeasureUnit> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MeasureUnitController(IRepository<MeasureUnit> repository, IMediator mediator, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }



        [HttpGet("GetById")]
        [Authorize(Roles = "Admin")]
        public async Task<MeasureUnitDTO> GetByIdAsync(int Id
           , CancellationToken cancellationToken)
        {
            var MesasureUnit = await _repository.Table
                .Include(t => t.Translation)
                .Where(m => m.Id == Id).FirstAsync(cancellationToken);


            MeasureUnitDTO result = new MeasureUnitDTO()
            {

                Name = new TranslationDto()
                {
                    Id = MesasureUnit.Translation.Id,
                    Arabic = MesasureUnit.Translation.Arabic,
                    English = MesasureUnit.Translation.English,
                    Persian = MesasureUnit.Translation.Persian
                },
                Value = MesasureUnit.Value,
                Id = MesasureUnit.Id,
                meassureUnitCategory = (int)MesasureUnit.MeasureUnitCategory
            };

            return result;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<MeasureUnitResultDto>> GetAsync(int? Page, int PageSize
           , CancellationToken cancellationToken)
        {
            List<MeasureUnit> measureUnits = await _repository.TableNoTracking.Include(m => m.Translation)
                                      .OrderByDescending(i => i.Id)
                                         .Skip((Page - 1 ?? 0) * PageSize)
                                         .Take(PageSize)
                                        .ToListAsync(cancellationToken);

            var countDetails = measureUnits.Count();
            List<MeasureUnitResultDto> data= new List<MeasureUnitResultDto>();

            if (measureUnits.Count > 0)
            {
               // foreach (var item in measureUnits)
               // {
               //     MeasureUnitDTO invoicePaging = new MeasureUnitDTO()
               //     {
               //
               //         Name = new TranslationDto()
               //         {
               //             Id = item.Translation.Id,
               //             Arabic = item.Translation.Arabic,
               //             English = item.Translation.English,
               //             Persian = item.Translation.Persian
               //         },
               //         Value = item.Value,
               //         Id = item.Id,
               //         meassureUnitCategory = (int)item.MeasureUnitCategory
               //     };
               //
               //     _paging.Add(invoicePaging);
               // }
               data = _mapper.Map<List<MeasureUnit>, List<MeasureUnitResultDto>>(measureUnits);
            }

            var result = new PageResult<MeasureUnitResultDto>
            {
                Count = countDetails,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = data
            };

            return result;
        }


        [HttpGet("GetAll")]
        public async Task<List<MeasureUnitModelDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var listMesasure = await _repository.Table.Include(m => m.Translation).OrderBy(i => i.Id).ToListAsync(cancellationToken);
            var result = listMesasure.Select(item => new MeasureUnitModelDTO()
            {
                Id = item.Id,
                Value = item.Value,
                Arabic = item.Translation.Arabic,
                English = item.Translation.English,
                Persian = item.Translation.Persian,
                MeasureUnitCategory = (MeasureUnitCategory)item.MeasureUnitCategory
            }).ToList();
            return result;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Create(MeasureUnitDTO measureuntdto, CancellationToken cancellationToken)
        {
            //Add Ing
            MeasureUnit measurunit = new MeasureUnit();
            if (measureuntdto.NameId > 0)
            {
                measurunit.NameId = measureuntdto.NameId ?? 3;
            }
            else
            {
                var name = await _mediator.Send(new CreateTranslationCommand
                {
                    Translation = measureuntdto.Name.ToEntity(_mapper)
                });
                measurunit.NameId = name.Id;
            }
            measurunit.Value = measureuntdto.Value;
            measurunit.MeasureUnitCategory = (MeasureUnitCategory)measureuntdto.meassureUnitCategory;
            await _repository.AddAsync(measurunit, cancellationToken);
            return Ok();

        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Edit(int id, MeasureUnitDTO measureunitdto, CancellationToken cancellationToken)
        {

            MeasureUnit measurunit = await _repository.GetByIdAsync(cancellationToken, id);
            measureunitdto.Name.Id = measurunit.NameId;
            var name = await _mediator.Send(new UpdateTranslationCommand
            {
                Translation = measureunitdto.Name.ToEntity(_mapper)
            });

            measurunit.NameId = name.Id;
            measurunit.Value = measureunitdto.Value;
            await _repository.UpdateAsync(measurunit, cancellationToken);
            return Ok();
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            MeasureUnit measurunit = await _repository.GetByIdAsync(cancellationToken, id);

            List<int> _list = new List<int>();
            _list.Add(measurunit.NameId);

            await _repository.DeleteAsync(measurunit, cancellationToken);
            await _mediator.Send(new DeleteTranslationCommand
            {
                Ids = _list
            });

            return Ok();
        }


        [HttpGet("Search")]
        [Authorize(Roles = "Admin")]
        public async Task<List<MeasureUnitDTO>> SearchAsync(string name
           , CancellationToken cancellationToken)
        {
            List<MeasureUnit> MeasureUnits = new List<MeasureUnit>();
            MeasureUnits = await _repository.Table
                            .Include(t => t.Translation)
                            .Where(m => m.Translation.Persian.Contains(name) || m.Translation.English.Contains(name) || m.Translation.Arabic.Contains(name)).ToListAsync(cancellationToken);

            if (Char.IsDigit(name[0]))
            {
                List<MeasureUnit> measureunitValueList = new List<MeasureUnit>();
                measureunitValueList = await _repository.Table.Include(t => t.Translation).Where(m => m.Value == Convert.ToDouble(name)).ToListAsync(cancellationToken);
                if (measureunitValueList.Count() > 0)
                {
                    MeasureUnits.AddRange(measureunitValueList);
                }
            }


            List<MeasureUnitDTO> result = new List<MeasureUnitDTO>();
            foreach (var item in MeasureUnits)
            {

                MeasureUnitDTO meas = new MeasureUnitDTO()
                {

                    Name = new TranslationDto()
                    {
                        Id = item.Translation.Id,
                        Arabic = item.Translation.Arabic,
                        English = item.Translation.English,
                        Persian = item.Translation.Persian
                    },
                    Value = item.Value,
                    Id = item.Id,
                    meassureUnitCategory = (int)item.MeasureUnitCategory
                };
                result.Add(meas);
            }
            result = result.Distinct<MeasureUnitDTO>().ToList();
            return result;
        }

        [HttpGet("[action]")]
        public async Task<ApiResult<MeasureUnitsAndVersionSelectDTO>> GetByVersionAsync(double versionNum,CancellationToken cancellationToken)
        {
           return await _mediator.Send(new GetMeasureUnitByVersionQurey() { Version = versionNum });           
        }

    }
}
