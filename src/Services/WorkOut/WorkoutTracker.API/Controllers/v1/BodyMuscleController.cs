using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Contracts;
using Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using WorkoutTracker.API.Models;
using WorkoutTracker.Common.Utilities;
using WorkoutTracker.Data.Repositories;
using WorkoutTracker.Domain.Entities.WorkOut;
using Service.v1.Command;
using Service.v1.Query;

namespace WorkoutTracker.API.Controllers.v1
{
    [ApiVersion("1")]
    public class BodyMuscleController : BaseController
    {
        private readonly IRepository<BodyMuscle> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BodyMuscleController(IRepository<BodyMuscle> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<List<BodyMuscleDTO>> GetAsync(int? Page, int PageSize
         , CancellationToken cancellationToken)
        {
            List<BodyMuscle> bodyMuscles = await _repository.Table.Include(m => m.Translation)
                                      .OrderByDescending(i => i.Id)
                                         .Skip((Page - 1 ?? 0) * PageSize)
                                         .Take(PageSize)
                                        .ToListAsync(cancellationToken);
      

            List<int> nameid = new List<int>();

            var countDetails = bodyMuscles.Count();

            //Get NameIds
            foreach (var item in bodyMuscles)
            {
                nameid.Add(item.NameId);
            }

            //for get translation ingredient
            var translations = await _mediator.Send(new GetTranslationQuery
            {
                Ids = nameid,
                Language = LanguageName
            });


            List<BodyMuscleDTO> _paging = new List<BodyMuscleDTO>();


                foreach (var item in bodyMuscles)
                {
                    BodyMuscleDTO invoicePaging = new BodyMuscleDTO()
                    {
                        Name =new TranslationDto() { 
                         Arabic= item.Translation.Arabic,
                         Persian= item.Translation.Persian,
                         English= item.Translation.English,
                         Id= item.Translation.Id,
                        },
                        Id = item.Id,
                    };

                    _paging.Add(invoicePaging);
                }

            return _paging;
        }


        [HttpPost]
        public async Task<ApiResult> CreateAsync(BodyMuscleDTO bodyMuscleDTO, CancellationToken cancellationToken)
        {
            BodyMuscle bodyMuscle = new BodyMuscle();
            var name = await _mediator.Send(new CraeteTranslationCommand
            {
                Translation = bodyMuscleDTO.Name.ToEntity(_mapper)
            });
            bodyMuscle.NameId = name.Id;
            await _repository.AddAsync(bodyMuscle, cancellationToken);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<ApiResult> Update(int id,BodyMuscleDTO bodyMuscleDTO,CancellationToken cancellationToken)
        {
            bodyMuscleDTO.Name.Id = id;

            await _mediator.Send(new UpdateTranslationCommand
            {
                Translation = bodyMuscleDTO.Name.ToEntity(_mapper),
            });

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            BodyMuscle bodyMuscle  = await _repository.GetByIdAsync(cancellationToken, id);

            List<int> _list = new List<int>();
            _list.Add(bodyMuscle.NameId);

            await _mediator.Send(new DeleteTranslationCommand
            {
                Ids = _list
            });

           await _repository.DeleteAsync(bodyMuscle, cancellationToken);
           return Ok();
        }

    }
}
