using AutoMapper;
using Common;
using Common.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ordering.API.Models;
using Ordering.Data.Contracts;
using Ordering.Domain.Entities.Package;
using Ordering.Domain.Entities.Translation;
using Ordering.Domain.Enum;
using Ordering.Domain.Models;
using Ordering.Service.v1.Command;
using Ordering.Service.v1.Query;
using Ordering.Service.v1.Query.GetAllPackageAdmin;
using Service.v1.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Ordering.API.Controllers.v1
{
    [ApiVersion("1")]
    public class PackageController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IPackageRepository _repository;

        public PackageController(IMapper mapper, IMediator mediator, IPackageRepository repository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet("Currency")]
        public virtual IActionResult CurrencyValue()
        {
            return Ok(EnumExtensions.GetEnumNameValues<Currency>());
        }

        [HttpGet]
        public virtual async Task<ApiResult<List<PackageCurrency>>> Get()
        {
            return await _mediator.Send(new GetPackageQuery { LanguageName = LanguageName });
        }
        [HttpGet("GetAllPackageAdmin")]
        [Authorize(Roles = "Admin")]
        public virtual async Task<ApiResult<List<Package>>> GetAllPackageAdmin()
        {
            return await _mediator.Send(new GetAllPackageAdminQuery { LanguageName = LanguageName });
        }

        [HttpGet("GetById")]
        public virtual async Task<ApiResult<PackageDto>> GetById(int id, CancellationToken cancellationToken)
        {
            Package _package = await _repository.GetById(id, cancellationToken);
            PackageDto package = new PackageDto()
            {
                Id = _package.Id,
                TranslationName = _package.TranslationName,
                TranslationDescription = _package.TranslationDescription,
                DiscountPercent = _package.DiscountPercent,
                Duration = _package.Duration,
                Currency = (Currency)_package.Currency,
                IsActive = _package.IsActive,
                Price = _package.Price,
                Sort = _package.Sort
            };
            return package;
        }


        [HttpPost]
        public async Task<ApiResult<Package>> Post(CreatePackageDTO createPackageDTO, CancellationToken cancellationToken)
        {

            Package _set = await _mediator.Send(new CreatePackageCommand { createPackageDTO = createPackageDTO });
            return Ok(_set);
        }

        [HttpPut]
        public virtual async Task<ApiResult> Put(UpdatePackageDTO package, CancellationToken cancellationToken)
        {
            try
            {
                Package oldpackage = await _repository.Entities.FindAsync(package.Id);
                _repository.Detach(oldpackage);
                await _mediator.Send(new UpdateTranslationCommand
                {
                    Translation = new Translation
                    {
                        Arabic = package.TranslationName.Arabic,
                        Id = oldpackage.NameId,
                        English = package.TranslationName.English,
                        Persian = package.TranslationName.Persian,
                    }
                });

                await _mediator.Send(new UpdateTranslationCommand
                {
                    Translation = new Translation
                    {
                        Arabic = package.TranslationDescription.Arabic,
                        Id = oldpackage.DescriptionId,
                        English = package.TranslationDescription.English,
                        Persian = package.TranslationDescription.Persian,
                    }
                });


                oldpackage.IsActive = package.IsActive;
                oldpackage.Duration = package.Duration;
                oldpackage.DiscountPercent = package.DiscountPercent;
                oldpackage.Currency = package.Currency;
                oldpackage.IsPromote = package.IsPromote;
                oldpackage.NutritionistId = package.NutritionistId;
                oldpackage.PackageType = package.PackageType;
                oldpackage.Price = package.Price;
                oldpackage.Sort = package.Sort;


                await _repository.UpdateAsync(oldpackage, cancellationToken);

                await _mediator.Send(new UpdatePackageCommand());

                return new ApiResult(true, ApiResultStatusCode.Success, "آپدیت با موفقیت انجام شد");
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ApiResultStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public virtual async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            Package package = await _repository.GetByIdAsync(cancellationToken, id);

            _repository.Detach(package);

            List<int> _ids = new List<int>();
            _ids.Add(package.NameId);
            _ids.Add(package.DescriptionId);

            await _repository.DeleteAsync(package, cancellationToken);

            await _mediator.Send(new DeleteTranslationCommand { Ids = _ids });

            await _mediator.Send(new UpdatePackageCommand());

            return Ok();
        }

        [HttpGet("GetNutritionistPackage")]
        public async Task<List<PackageDto>> GetNutritionistPackage(int NutritionistId, CancellationToken cancellationToken)
        {
            var result = await _repository.TableNoTracking.Where(
                    p => p.NutritionistId == NutritionistId).Select(s =>
                    new PackageDto
                    {
                        Id = s.Id,
                        TranslationName = s.TranslationName,
                        IsActive = s.IsActive,
                        NutritionistId = s.NutritionistId,
                        Currency = s.Currency,
                        DiscountPercent = s.DiscountPercent,
                        Duration = s.Duration,
                        PackageType = s.PackageType,
                        Price = s.Price,
                        Sort = s.Sort,
                        TranslationDescription = s.TranslationDescription
                    })
                .ToListAsync(cancellationToken);


            return result;

        }

    }
}
