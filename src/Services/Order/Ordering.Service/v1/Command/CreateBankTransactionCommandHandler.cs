using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Data.Contracts;
using Ordering.Domain.Entities.Discount;
using Ordering.Domain.Entities.Order;
using Ordering.Domain.Entities.Payment;
using Ordering.Domain.Enum;
using Ordering.Domain.Models;
using Ordering.Service.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Command
{
    public class CreateBankTransactionCommandHandler : IRequestHandler<CreateBankTransactionCommand>, IScopedDependency
    {
        private readonly IRepository<Order> _repositoryOrder;
        private readonly IRepository<BankTransaction> _repositoryTransaction;
        private readonly IRepository<Discount> _repositoryDiscount;
        private readonly IRepositoryRedis<Order> _repositoryRedis;
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly ISocialService _socialRepository;


        public CreateBankTransactionCommandHandler(IRepository<Order> repositoryOrder, IRepository<BankTransaction> repositoryTransaction,
          IRepository<Discount> repositoryDiscount, IRepositoryRedis<Order> repositoryRedis, IUserService userService,
          IUserProfileService userProfileService, ISocialService socialRepository)
        {
            _repositoryOrder = repositoryOrder;
            _repositoryTransaction = repositoryTransaction;
            _repositoryDiscount = repositoryDiscount;
            _repositoryRedis = repositoryRedis;
            _userService = userService;
            _userProfileService = userProfileService;
            _socialRepository = socialRepository;

        }

        public async Task<Unit> Handle(CreateBankTransactionCommand request, CancellationToken cancellationToken)
        {


            BankTransaction bankTransaction = new BankTransaction();

            var order = await _repositoryOrder.Table.Include(a => a.Package).ThenInclude(a => a.TranslationName)
                .Include(a => a.Discount)
                .FirstOrDefaultAsync(a => a.Id == request.Order.Id, cancellationToken);


            bool previusBuy = await _repositoryOrder.Table.Include(a => a.BankTransaction)
                .AnyAsync(a => a.UserId == order.UserId && a.BankTransaction != null, cancellationToken);

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

            switch (request.Bank)
            {
                case Bank.Saman:
                    {
                        bankTransaction = new BankTransaction
                        {
                            Amount = order.Amount,
                            Bank = request.Bank,
                            OrderId = order.Id,
                            DateTime = DateTime.Now,
                            ResNum = request.SamanResult.ResNum,
                            TraceNo = request.SamanResult.TRACENO,
                            RefNum = request.SamanResult.RefNum,
                            SecurePan = request.SamanResult.SecurePan,
                            State = request.SamanResult.State,
                            StateCode = request.SamanResult.StateCode,
                            CID = request.SamanResult.CID,
                            SaleReferenceId = request.SamanResult.RefNum
                        };

                        break;
                    }
                case Bank.Melat:
                    {
                        bankTransaction = new BankTransaction
                        {
                            Amount = order.Amount,
                            Bank = request.Bank,
                            OrderId = order.Id,
                            DateTime = DateTime.Now,
                            RefNum = request.MelatResult.RefId,
                            ResNum = request.MelatResult.ResCode,
                            SaleReferenceId = request.MelatResult.SaleReferenceId
                        };

                        break;
                    }
                case Bank.YekPay:
                    {
                        bankTransaction = new BankTransaction
                        {
                            Amount = order.Amount,
                            Bank = request.Bank,
                            OrderId = order.Id,
                            DateTime = DateTime.Now,
                            Authority = request.YekPayResult.Authority
                        };

                        break;
                    }
                case Bank.Discount:
                    {
                        _DurationCal = 30;

                        bankTransaction = new BankTransaction
                        {
                            Amount = order.Amount,
                            Bank = request.Bank,
                            OrderId = order.Id,
                            DateTime = DateTime.Now,
                        };

                        break;
                    }

            }

            await _repositoryTransaction.AddAsync(bankTransaction, cancellationToken);

            _repositoryTransaction.Detach(bankTransaction);

            if (order.DiscountId != null)
            {
                Discount discount = await _repositoryDiscount.Table.Where(a => a.IsActive == true && a.Id == order.DiscountId).FirstOrDefaultAsync(cancellationToken);

                if (discount != null)
                {

                    if (discount.UserId != null)
                    {
                        if (order.UserId == discount.UserId)
                        {
                            discount.IsActive = false;
                        }
                    }

                    if (discount.UsableCount != null)
                    {
                        if (discount.UsableCount > 0)
                        {
                            discount.UsableCount = discount.UsableCount - 1;
                        }
                        else
                        {
                            discount.IsActive = false;
                        }
                    }

                    if (discount.EndDateTime < DateTime.Now)
                    {
                        discount.IsActive = false;
                    }

                    await _repositoryDiscount.UpdateAsync(discount, cancellationToken);
                }

                _repositoryDiscount.Detach(discount);
            }

            //if (orderExpireTime != null)
            //{
            //    var days = orderExpireTime.ExpireTime.Date - DateTime.Now.Date;
            //    _Duration = _Duration + days.Days;
            //    _repositoryOrder.Detach(orderExpireTime);
            //}

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


            await _repositoryRedis.UpdateDisableLoopAsync($"Order_User_{order.UserId}", order);
            try
            {
                #region Success Message To User
                var message = new AdminContactUsMessageDTO()
                {
                    UserId = order.UserId,
                    Language = request.Language,
                    Classification = Classification.Finance,
                    InsertDate = DateTime.Now,
                    ToAdmin = false
                };
                switch (request.Language)
                {
                    case "Persian":
                        {
                            message.Title = "خرید اشتراک";
                            message.Message = $" با تشکر از اعتماد شما، اشتراک  {order.Package.TranslationName.Persian} جدید فعال شد کد پیگیری پرداخت:{bankTransaction.SaleReferenceId}";
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
                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Unit.Value;
        }
    }
}
