using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using WorkoutTracker.API.Models;
using WorkoutTracker.Common.Utilities;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.API.Controllers.v1
{ 
    [ApiVersion("1")]
    public class PersonalWorkOutController : BaseController
    {
        private readonly IPersonalWorkOutRepository _Personalrepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PersonalWorkOutController( IMediator mediator, IMapper mapper,
            IPersonalWorkOutRepository Personalrepository)
        {
            _Personalrepository = Personalrepository;
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<PageResult<PersonalWorkOut>> GetAsync(int userId,int languageid, int? Page, int PageSize
        , CancellationToken cancellationToken)
        {
            var listpersonal = _Personalrepository.GetAsync(userId,cancellationToken);

            List<int> nameid = new List<int>();

            var countDetails = listpersonal.Count();

            List<PersonalWorkOut> _personal = await listpersonal
                                           .Skip((Page - 1 ?? 0) * PageSize)
                                           .Take(PageSize)
                                           .ToListAsync(cancellationToken);


            List<PersonalWorkOut> _paging = new List<PersonalWorkOut>();

            if (_personal.Count > 0)
            {
                foreach (var item in _personal)
                {
                    PersonalWorkOut invoicePaging = new PersonalWorkOut()
                    {   Id=item.Id,
                        Name = item.Name,
                        Calorie = item.Calorie,
                        Duration = item.Duration,
                        UserId = item.UserId,
                        _id=item._id
                    };

                    _paging.Add(invoicePaging);
                }
            }

            var result = new PageResult<PersonalWorkOut>
            {
                Count = countDetails,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = _paging
            };

            return result;
        }



        [HttpPost("Create")]
        public async Task<ApiResult<PersonalWorkOut>> Create(PersonalWorkOutDTO personalWorkOutDTO , CancellationToken cancellationToken)
        {
            PersonalWorkOut personalWorkout = personalWorkOutDTO.ToEntity(_mapper);
            personalWorkout._id = personalWorkOutDTO._id;
            personalWorkout.Id= await _Personalrepository.AddAsync(personalWorkout, cancellationToken);
            return personalWorkout;
        }


        [HttpPut("{id}")]
        public async Task<ApiResult> Edit(int id, PersonalWorkOutDTO personalWorkOutDTO, CancellationToken cancellationToken)
        {
            PersonalWorkOut personalWorkout = await _Personalrepository.GetByIdAsync(cancellationToken, id);
            _Personalrepository.Detach(personalWorkout);
            personalWorkout = personalWorkOutDTO.ToEntity(_mapper);
            personalWorkout.Id = id;
            await _Personalrepository.UpdateAsync(personalWorkout, cancellationToken);
            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            PersonalWorkOut personalWorkout = await _Personalrepository.GetByIdAsync(cancellationToken, id);
            await _Personalrepository.DeleteAsync(personalWorkout, cancellationToken);
            return Ok();
        }
    }
}
