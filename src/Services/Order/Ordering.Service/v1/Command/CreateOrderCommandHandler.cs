using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities.Discount;
using Ordering.Domain.Entities.Order;
using Ordering.Domain.Entities.Package;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Command
{
   public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>, IScopedDependency
   {
      private readonly IRepository<Discount> _repositoryDiscount;
      private readonly IRepository<Package> _repositoryPackage;
      private readonly IRepository<Order> _repositoryOrder;

      public CreateOrderCommandHandler(IRepository<Discount> repositoryDiscount, IRepository<Package> repositoryPackage, IRepository<Order> repositoryOrder)
      {
         _repositoryDiscount = repositoryDiscount;
         _repositoryPackage = repositoryPackage;
         _repositoryOrder = repositoryOrder;
      }

      public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
      {
         Order order = new Order();

         Discount discount = new Discount();

         Package package = await _repositoryPackage.TableNoTracking.SingleOrDefaultAsync(a => a.Id == request.PackageId, cancellationToken);

         if (package.DiscountPercent != null)
         {
            double priceDis = (double)package.DiscountPercent;
            package.Price = package.Price - ((package.Price * priceDis) / 100);
         }

         if (request.IsDiscountActive && request.DiscountId != null)
         {
            discount = await _repositoryDiscount.TableNoTracking.FirstOrDefaultAsync(a => a.Id == request.DiscountId, cancellationToken);
         }

         if (discount.Id > 0)
         {
            _repositoryDiscount.Detach(discount);
         }

         if (package != null)
         {
            double Price = package.Price;
            int Percent;

            order = new Order
            {
               UserId = request.UserId,
               CreateDate = DateTime.Now,
               Amount = package.Price,
               PackageId = package.Id
            };

            if (discount.Id > 0)
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
                  order.Amount = Price - ((Price * Percent) / 100);
                  order.DiscountId = discount.Id;
               }
            }

            if (request.IsDiscountActive && request.DiscountUser != null)
            {
               Percent = (int)request.DiscountUser;
               order.Amount = Price - ((Price * Percent) / 100);
               order.DiscountUser = request.DiscountUser;
            }

            await _repositoryOrder.AddAsync(order, cancellationToken);

            _repositoryPackage.Detach(package);

         }


         if (order != null)
         {
            _repositoryOrder.Detach(order);
         }

         return order;
      }
   }
}
