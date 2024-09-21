using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Contracts;
using FoodStuff.API.Models;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.Translation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.v1.Command;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1")]
    public class BrandController : BaseController
    {
        private readonly IRepository<Brand> repository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        public BrandController(IMapper mapper, IMediator mediator, IRepository<Brand> repository)
        {
           this.repository = repository;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet("Search")]
        public virtual async Task<ApiResult<List<BrandSelectDTO>>> SearchAsync(string name, CancellationToken cancelationToken)
        {
            var Brands = repository.Table.Where(b => b.Translation.Persian.Contains(name)).OrderByDescending(b => b.Id);
            var BrandList = Brands.Select(b => new BrandSelectDTO()
            {
                Id = b.Id,
                Name = b.Translation.Persian,
            });
            return BrandList.ToList();
        }

        [HttpPost]
        public async Task<ApiResult> PostAsync(BrandDTO brandDTO, CancellationToken cancelationToken)
        {
            var Brand = new Brand()
            {
                Id = brandDTO.Id,
                LogoUri = brandDTO.LogoUri,
                Address = brandDTO.Address
            };
            var brandName = await mediator.Send(new CreateTranslationCommand
            {
                Translation = new Translation()
                {
                    Persian = brandDTO.Name.Persian,
                    English = brandDTO.Name.English,
                    Arabic = brandDTO.Name.Arabic
                }
            }, cancelationToken);
            Brand.NameId = brandName.Id;
            await repository.AddAsync(Brand, cancelationToken);
            return Ok();
        }

    }
}
