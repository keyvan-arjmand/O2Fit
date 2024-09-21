using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Data.Contracts;
using Ordering.Service.v1.Query;
using Ordering.Service.v2.Query.GetPackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Ordering.API.Controllers.v2
{
    [ApiVersion("2")]
    public class PackageController : v1.PackageController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IPackageRepository _repository;

        public PackageController(IMapper mapper, IMediator mediator, IPackageRepository repository):base(mapper, mediator, repository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _repository = repository;
        }

        public override async Task<ApiResult<List<PackageCurrency>>> Get()
        {
            var packages = await base.Get();
            if (packages != null)
            {
                packages = await _mediator.Send(new GetPackageV2Query { LanguageName = LanguageName });
            }
            return packages;
        }
    }
}
