using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities.Discount;
using Ordering.Domain.Entities.Order;
using Ordering.Domain.Entities.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ordering.Domain.Enum;
using Ordering.Service.Services;
using Common.Utilities;

namespace Ordering.Service.v1.Query
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, DiscountSelectDto>, IScopedDependency
    {
        private readonly IRepository<Discount> _repositoryDiscount;
        private readonly IRepository<Order> _repositoryOrder;
        private readonly IRepository<Package> _repositoryPackage;
        private readonly IUserService _userService;

        public GetDiscountQueryHandler(IRepository<Discount> repositoryDiscount,
            IRepository<Order> repositoryOrder,
            IRepository<Package> repositoryPackage,
            IUserService userService)
        {
            _repositoryDiscount = repositoryDiscount;
            _repositoryOrder = repositoryOrder;
            _repositoryPackage = repositoryPackage;
            _userService = userService;
        }

        public async Task<DiscountSelectDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            DiscountSelectDto discountSelectDto = new DiscountSelectDto()
            {
                Amount = 0,
                IsActive = false,
                PackageId = request.PackageId,
                UserId = request.UserId,
                DiscountId = null,
                DiscountUser = null
            };

            Package package = await _repositoryPackage.TableNoTracking.SingleOrDefaultAsync(a => a.Id == request.PackageId, cancellationToken);
            double Price = package.Price;

            if (package.DiscountPercent != null)
            {
                double priceDis = (double)package.DiscountPercent;
                Price = Price - ((Price * priceDis) / 100);
            }

            int Percent = 0;

            if (string.IsNullOrEmpty(request.Code))
            {
                bool previousPurchase = await _repositoryOrder.Table.Include(a => a.BankTransaction)
                    .AnyAsync(a => a.UserId == request.UserId && a.BankTransaction != null);

                var referreralResult = await _userService.CheckUserReferreral(request.UserId, previousPurchase);
                bool referreral = Convert.ToBoolean(referreralResult.Data);

                //if (previousPurchase == true && referreral == true && package.Duration == 30)
                //{
                //    Percent = (int)DiscountUser.OneHundredPercent;

                //    discountSelectDto.Amount = Price - ((Price * Percent) / 100);
                //    discountSelectDto.IsActive = true;
                //    discountSelectDto.DiscountUser = DiscountUser.OneHundredPercent;
                //}

                if (previousPurchase == false && referreral == true)
                {
                    //First Buy
                    Percent = (int)DiscountUser.TenPercent;

                    discountSelectDto.Amount = Price - ((Price * Percent) / 100);
                    discountSelectDto.IsActive = true;
                    discountSelectDto.DiscountUser = DiscountUser.TenPercent;
                }

            }
            else
            {
                Discount discount = await _repositoryDiscount.TableNoTracking.Where(a => a.Code.ToLower() == request.Code.ToLower()).FirstOrDefaultAsync(cancellationToken);

                if (discount != null)
                {
                    bool _Usable = await _repositoryOrder.Table
                                                         .Include(a => a.BankTransaction)
                                                         .Where(a => a.UserId == request.UserId && a.DiscountId == discount.Id && a.BankTransaction != null).AnyAsync();

                    if (_Usable == false && discount.EndDateTime > DateTime.Now && discount.StartDate <= DateTime.Now && discount.IsActive == true)
                    {
                        Percent = discount.Percent;

                        bool IsActive = false;

                        if (discount.UserId != null)
                        {
                            if (request.UserId == discount.UserId)
                            {
                                IsActive = true;
                            }
                        }

                        if (discount.UsableCount != null)
                        {
                            if (discount.UsableCount > 0)
                            {
                                IsActive = true;
                            }
                        }

                        if (IsActive)
                        {
                            discountSelectDto.Amount = Price - ((Price * Percent) / 100);
                            discountSelectDto.IsActive = true;
                            discountSelectDto.DiscountId = discount.Id;
                        }
                    }
                }

                if (discount != null)
                {
                    _repositoryDiscount.Detach(discount);
                }
            }

            if (package != null)
            {
                _repositoryPackage.Detach(package);
            }

            return discountSelectDto;
        }
    }
}