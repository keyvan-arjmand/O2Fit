using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;
using Identity.Domain;
using Identity.Domain.Entities.Country;
using System.Reflection;
using System.Transactions;
using Identity.Domain.Entities.Translation;
using MediatR;
using AutoMapper;
using Service.v1.Query;
using Microsoft.EntityFrameworkCore;
using Identity.Domain.Enum;
using Identity.API.Models;
using Common.Utilities;
using System.Threading;

namespace Identity.API.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1")]
    public class CountriesController : BaseController
    {
        private readonly IRepository<Country> _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CountriesController(IRepository<Country> repository, IMediator mediator, IMapper mapper)
        {
            _repository = repository;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ApiResult<List<Translation>>> Get()
        {
            List<Country> _Country = await _repository.TableNoTracking.ToListAsync();

            List<int> _CountryIds = new List<int>();

            foreach (var item in _Country)
            {
                _CountryIds.Add(item.NameId);
            }

            List<Translation> _Translation = await _mediator.Send(new GetTranslationQuery
            {
                Ids = _CountryIds,
                Language = LanguageName
            });

            List<Translation> _getData = new List<Translation>();

            foreach (var item in _Translation)
            {
                Translation translation = new Translation()
                {
                    Id = _Country.Where(a => a.NameId == item.Id).Select(a => a.Id).First(),
                    Persian = item.Persian,
                    English = item.English,
                    Arabic = item.Arabic
                };
                _getData.Add(translation);
            }

            return _getData;
        }

        [HttpGet("Language")]
        public IActionResult LanguageValue()
        {
            return Ok(EnumExtensions.GetEnumNameValues<Language>());
        }

        //[HttpGet("CountryAdd")]
        //public async Task<IActionResult> CountryAdd(CancellationToken cancellationToken)
        //{
        //    List<Country> _country = new List<Country>();

        //    for (int i = 2; i < 199; i++)
        //    {
        //        Country _c = new Country()
        //        {
        //            NameId = i
        //        };

        //        _country.Add(_c);
        //    }

        //    await _repository.AddRangeAsync(_country,cancellationToken);

        //    return Ok();
        //}
    }
}
