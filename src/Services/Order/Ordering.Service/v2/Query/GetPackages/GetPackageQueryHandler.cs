using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities.Package;
using Ordering.Service.v1.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v2.Query.GetPackages
{
    public class GetPackageQueryHandler : IRequestHandler<GetPackageV2Query, List<PackageCurrency>>, IScopedDependency
    {
        private readonly IRepository<Package> _repository;
        private readonly IRepositoryRedis<List<Package>> _repositoryRedis;

        public GetPackageQueryHandler(IRepository<Package> repository, IRepositoryRedis<List<Package>> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<List<PackageCurrency>> Handle(GetPackageV2Query request, CancellationToken cancellationToken)
        {
            List<Package> packages = new List<Package>();

            packages = await _repositoryRedis.GetAsync("PackageList");

            if (packages == null)
            {
                packages = await _repository.Table
                                        .Include(a => a.TranslationName)
                                        .Include(a => a.TranslationDescription)
                                        .Where(a => a.IsActive == true && a.NutritionistId == 0)
                                        .OrderBy(a => a.Sort)
                                        .ToListAsync(cancellationToken: cancellationToken);

                foreach (var item in packages)
                {
                    _repository.Detach(item);
                }

                await _repositoryRedis.UpdateAsync("PackageList", packages);
            }

            List<PackageCurrency> _SelectPackage = new List<PackageCurrency>();

            string _LanguageName = request.LanguageName == null ? "Persian" : request.LanguageName;

            if (packages.Count > 0)
            {
                foreach (var item in packages)
                {
                    PackageCurrency packageCurrency = new PackageCurrency()
                    {
                        Id = item.Id,
                        DiscountPercent = item.DiscountPercent,
                        Duration = item.Duration,
                        Price = item.Price,
                        Currency = item.Currency,
                        IsPromote = item.IsPromote,
                        PackageType = item.PackageType
                    };

                    //if (packageCurrency.DiscountPercent != null)
                    //{
                    //    double priceDis = (double)packageCurrency.DiscountPercent;
                    //    packageCurrency.Price = packageCurrency.Price - ((packageCurrency.Price * priceDis) / 100);
                    //}

                    switch (_LanguageName)
                    {
                        case "Persian":
                            {
                                packageCurrency.Name = item.TranslationName.Persian;
                                packageCurrency.Description = item.TranslationDescription.Persian;
                                break;
                            }
                        case "English":
                            {
                                packageCurrency.Name = item.TranslationName.English;
                                packageCurrency.Description = item.TranslationDescription.English;
                                break;
                            }
                        case "Arabic":
                            {
                                packageCurrency.Name = item.TranslationName.Arabic;
                                packageCurrency.Description = item.TranslationDescription.Arabic;
                                break;
                            }
                        default:
                            break;
                    }


                    _SelectPackage.Add(packageCurrency);
                }
            }

            return _SelectPackage;
        }
    }
}
