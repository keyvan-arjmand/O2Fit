using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Data.Contracts;
using Ordering.Domain.Entities.Order;
using Ordering.Domain.Entities.Payment;
using Ordering.Domain.Enum;
using Ordering.Domain.Models;
using Ordering.Service.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Command
{
    public class CreateOrderBazarCommandHandler : IRequestHandler<CreateOrderBazarCommand, Order>, IScopedDependency
    {
        private readonly IOrderRepository _repositoryOrder;
        private readonly IUserService _userService;
        private readonly IRepository<BankTransaction> _repositoryTransaction;
        private readonly IUserProfileService _userProfileService;
        private readonly IRepositoryRedis<Order> _repositoryRedis;
        private readonly ISocialService _socialRepository;


        public CreateOrderBazarCommandHandler(IOrderRepository repositoryOrder, IUserService userService,
            IRepository<BankTransaction> repositoryTransaction, IUserProfileService userProfileService,
            IRepositoryRedis<Order> repositoryRedis, ISocialService socialRepository)
        {
            _repositoryOrder = repositoryOrder;
            _userService = userService;
            _repositoryTransaction = repositoryTransaction;
            _userProfileService = userProfileService;
            _repositoryRedis = repositoryRedis;
            _socialRepository = socialRepository;

        }
        public async Task<Order> Handle(CreateOrderBazarCommand request, CancellationToken cancellationToken)
        {

            Order _order = new Order
            {
                UserId = request.cafeBazarModel.UserId,
                CreateDate = DateTime.Now,
                Amount = request.cafeBazarModel.Price,
                PackageId = request.cafeBazarModel.PackageId
                //ExpireTime = request.Expiretime,
            };


            int OrderId = await _repositoryOrder.AddCafeBazarAsync(_order, cancellationToken);

            var order = await _repositoryOrder.Table.Include(a => a.Package).ThenInclude(a => a.TranslationName)
            .Include(a => a.Discount)
            .FirstOrDefaultAsync(a => a.Id == OrderId, cancellationToken);

            if (request.cafeBazarModel.IsSuccess)
            {
                BankTransaction bankTransaction = new BankTransaction();

                //var orderExpireTime = await _repositoryOrder.Table
                // .Include(a => a.BankTransaction)
                // .Include(o=>o.Package).ThenInclude(t=>t.TranslationName)
                // .Where(a => a.UserId == order.UserId && a.ExpireTime > DateTime.Now && a.BankTransaction != null)
                // .OrderByDescending(a => a.CreateDate).FirstOrDefaultAsync(cancellationToken);

                bool previusBuy = await _repositoryOrder.Table.Include(a => a.BankTransaction)
               .AnyAsync(a => a.UserId == order.UserId && a.BankTransaction != null, cancellationToken);

                //int _Duration = order.Package.Duration;
                int _DurationCal = 0;
                int _DurationDiet = 0;
                if (order.Package.PackageType == PackageType.CalorieCounting)
                {
                    _DurationCal = order.Package.Duration;
                }
                else if (order.Package.PackageType == PackageType.Diet)
                {
                    _DurationCal = order.Package.Duration;
                    _DurationDiet = order.Package.Duration;
                }

                if (previusBuy == false)
                {
                    await _userService.AddReferreralCount(order.UserId);
                }

                switch (request.cafeBazarModel.Bank)
                {
                    case Bank.CafeBazar:
                        {
                            bankTransaction = new BankTransaction()
                            {
                                Amount = order.Amount,
                                Bank = request.cafeBazarModel.Bank,
                                OrderId = order.Id,
                                DateTime = DateTime.Now,
                                SaleReferenceId = request.cafeBazarModel.SaleReferenceId   //شماره پیگیری
                                ,
                                State = "OK"
                            };

                            break;
                        }


                    case Bank.Myket:
                        {
                            bankTransaction = new BankTransaction()
                            {
                                Amount = order.Amount,
                                Bank = request.cafeBazarModel.Bank,
                                OrderId = order.Id,
                                DateTime = DateTime.Now,
                                SaleReferenceId = request.cafeBazarModel.SaleReferenceId   //شماره پیگیری
                                ,
                                State = "OK"
                            };

                            break;
                        }


                    case Bank.Discount:
                        {
                            _DurationCal = 30;

                            bankTransaction = new BankTransaction()
                            {
                                Amount = order.Amount,
                                Bank = request.cafeBazarModel.Bank,
                                OrderId = order.Id,
                                DateTime = DateTime.Now,

                            };

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                await _repositoryTransaction.AddAsync(bankTransaction, cancellationToken);

                _repositoryTransaction.Detach(bankTransaction);


                var refitResult = await _userProfileService.Get(order.UserId);
                var user = refitResult.Data;


                if (order.Package.PackageType == PackageType.CalorieCounting)
                {
                    if (user.PkExpireDate > DateTime.Now)
                    {
                        var days = user.PkExpireDate - DateTime.Now.Date;
                        _DurationCal = _DurationCal + days?.Days ?? 0;
                    }
                    order.ExpireTime = DateTime.Now.AddDays(_DurationCal);
                    await _repositoryOrder.UpdateAsync(order, cancellationToken);
                    _repositoryOrder.Detach(order);

                    await _userProfileService.UpdateExpireTimeUser(order.UserId, order.ExpireTime.ToString(), null, "D6862E20-45DB-11EB-BBAC-6133FC2CA371");
                }

                else if (order.Package.PackageType == PackageType.Diet)
                {
                    if (user.PkExpireDate > DateTime.Now)
                    {
                        var days = user.PkExpireDate - DateTime.Now.Date;
                        _DurationCal = _DurationCal + days?.Days ?? 0;
                    }
                    if (user.DietPkExpireDate > DateTime.Now)
                    {
                        var dietDays = user.DietPkExpireDate - DateTime.Now.Date;
                        _DurationDiet = _DurationDiet + dietDays?.Days ?? 0;
                    }
                    var dietExpierDate = DateTime.Now.AddDays(_DurationDiet);
                    order.ExpireTime = DateTime.Now.AddDays(_DurationCal);
                    await _repositoryOrder.UpdateAsync(order, cancellationToken);
                    _repositoryOrder.Detach(order);
                    await _userProfileService.UpdateExpireTimeUser(order.UserId, order.ExpireTime.ToString(), dietExpierDate.ToString(), "D6862E20-45DB-11EB-BBAC-6133FC2CA371");
                }



                //if (orderExpireTime != null)
                //{
                //    var days = orderExpireTime.ExpireTime.Date - DateTime.Now.Date;
                //    _Duration = _Duration + days.Days;
                //    _repositoryOrder.Detach(orderExpireTime);
                //}

                //order.ExpireTime = DateTime.Now.AddDays(_Duration);

                //await _repositoryOrder.UpdateAsync(order, cancellationToken);
                //_repositoryOrder.Detach(order);
                //await _userProfileService.UpdateExpireTimeUser(order.UserId, order.ExpireTime.ToString(),null, "D6862E20-45DB-11EB-BBAC-6133FC2CA371");

                await _repositoryRedis.UpdateDisableLoopAsync($"Order_User_{order.UserId}", order);
                try
                {
                    var message = new AdminContactUsMessageDTO()
                    {
                        UserId = order.UserId,
                        Language = request.cafeBazarModel.Language,
                        Classification = Classification.Finance,
                        InsertDate = DateTime.Now,
                        ToAdmin = false
                    };
                    switch (request.cafeBazarModel.Language)
                    {
                        case "Persian":
                            {
                                message.Title = "خرید اشتراک";
                                message.Message = $" با تشکر از اعتماد شما، اشتراک  جدید{order.Package.TranslationName.Persian} فعال شد کد پیگیری پرداخت:{bankTransaction.SaleReferenceId}";
                                //message.Message = $" با تشکر از اعتماد شما، اشتراک  { orderExpireTime.Package.TranslationName.Persian} جدید فعال شد کد پیگیری پرداخت:{bankTransaction.SaleReferenceId}";
                            }
                            break;
                        case "English":
                            {
                                message.Title = "Increasing credit";
                                message.Message = $" Thanks, new {order.Package.TranslationName.English} premium activated Payment code:{bankTransaction.SaleReferenceId} ";
                            }
                            break;
                        case "Arabic":
                            {
                                message.Title = "زيادة الائتمان";
                                message.Message = $"شكرًا ، تم تفعيل الاشتراك {order.Package.TranslationName.Persian} كود الدفع:{bankTransaction.SaleReferenceId}";
                            }
                            break;
                    }

                    await _socialRepository.SendMessageToUserAsync(message, "265dc0Cd-5227-468A-bFef-7022c34Da490", cancellationToken);

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            return order;
        }
    }
}
