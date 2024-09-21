using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities.Package;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Query.GetAllPackageAdmin
{
    public class GetAllPackageAdminQueryHandler : IRequestHandler<GetAllPackageAdminQuery, List<Package>>, IScopedDependency
    {
        private readonly IRepository<Package> _repository;
        private readonly IRepositoryRedis<List<Package>> _repositoryRedis;

        public GetAllPackageAdminQueryHandler(IRepository<Package> repository, IRepositoryRedis<List<Package>> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<List<Package>> Handle(GetAllPackageAdminQuery request, CancellationToken cancellationToken)
        {


            var packages = await _repository.Table
                                     .Include(a => a.TranslationName)
                                     .Include(a => a.TranslationDescription)
                                     .OrderByDescending(a => a.Id)
                                     .ToListAsync();

            //List<PackageCurrency> _SelectPackage = new List<PackageCurrency>();

            //string _LanguageName = request.LanguageName == null ? "Persian" : request.LanguageName;

            //if (packages.Count > 0)
            //{
            //    foreach (var item in packages)
            //    {
            //        PackageCurrency packageCurrency = new PackageCurrency()
            //        {
            //            Id = item.Id,
            //            DiscountPercent = item.DiscountPercent,
            //            Duration = item.Duration,
            //            Price = item.Price,
            //            Currency = item.Currency,
            //            IsPromote=item.IsPromote,
            //            PackageType=item.PackageType
            //        };

            //        switch (_LanguageName)
            //        {
            //            case "Persian":
            //                {
            //                    packageCurrency.Name = item.TranslationName.Persian;
            //                    packageCurrency.Description = item.TranslationDescription.Persian;
            //                    break;
            //                }
            //            case "English":
            //                {
            //                    packageCurrency.Name = item.TranslationName.English;
            //                    packageCurrency.Description = item.TranslationDescription.English;
            //                    break;
            //                }
            //            case "Arabic":
            //                {
            //                    packageCurrency.Name = item.TranslationName.Arabic;
            //                    packageCurrency.Description = item.TranslationDescription.Arabic;
            //                    break;
            //                }
            //            default:
            //                break;
            //        }

            //        _SelectPackage.Add(packageCurrency);
            //    }
            //}

            return packages;
        }
    }
}
