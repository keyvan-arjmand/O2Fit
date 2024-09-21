using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities.Package;
using Ordering.Domain.Entities.Translation;
using Ordering.Domain.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Command
{
    public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, Package>, IScopedDependency
    {
        private readonly IRepository<Package> _repository;
        private readonly IRepositoryRedis<List<Package>> _repositoryRedis;


        public CreatePackageCommandHandler(IRepository<Package> repository, IRepositoryRedis<List<Package>> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<Package> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
        {

            var name = new Translation
            {
                Arabic = request.createPackageDTO.TranslationName.Arabic,
                English = request.createPackageDTO.TranslationName.English,
                Persian = request.createPackageDTO.TranslationName.Persian,

            };

            var description = new Translation
            {
                Arabic = request.createPackageDTO.TranslationDescription.Arabic,
                English = request.createPackageDTO.TranslationDescription.English,
                Persian = request.createPackageDTO.TranslationDescription.Persian,

            };

            var package = new Package
            {
                TranslationDescription = description,
                TranslationName = name,
                Currency = request.createPackageDTO.Currency,
                DiscountPercent = request.createPackageDTO.DiscountPercent,
                Duration = request.createPackageDTO.Duration,
                IsActive = request.createPackageDTO.IsActive,
                IsPromote = request.createPackageDTO.IsPromote,
                NutritionistId = request.createPackageDTO.NutritionistId,
                PackageType = request.createPackageDTO.PackageType,
                Price = request.createPackageDTO.Price,
                Sort = request.createPackageDTO.Sort,
            };


            await _repository.AddAsync(package, cancellationToken);

            var _pack = new List<Package>();
            if (package.PackageType == PackageType.CalorieCounting)
            {
                _pack = await _repository.Table
                                                     .Include(a => a.TranslationName)
                                                     .Include(a => a.TranslationDescription)
                                                     .Where(a => a.IsActive == true && a.PackageType == PackageType.CalorieCounting)
                                                     .OrderBy(a => a.Sort)
                                                     .ToListAsync();

                await _repositoryRedis.UpdateAsync("Package", _pack);
            }
            _pack = await _repository.Table
                                        .Include(a => a.TranslationName)
                                        .Include(a => a.TranslationDescription)
                                        .Where(a => a.IsActive == true)
                                        .OrderBy(a => a.Sort)
                                        .ToListAsync();

            await _repositoryRedis.UpdateAsync("PackageList", _pack);

            return package;
        }
    }
}
