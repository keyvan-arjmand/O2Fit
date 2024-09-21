using AutoMapper;
using Common;
using Common.Utilities;
using Data.Contracts;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ordering.API.Models;
using Ordering.Domain.Entities.Order;
using Ordering.Domain.Entities.Package;
using Ordering.Domain.Entities.Payment;
using Ordering.Domain.Enum;
using Ordering.Domain.Models;
using Ordering.Service.Services;
using Ordering.Service.Services.Payment.Mellat;
using Ordering.Service.Services.Payment.Saman;
using Ordering.Service.Services.Payment.YekPay;
using Ordering.Service.v1.Command;
using Ordering.Service.v1.Query;
using Ordering.Service.v1.Query.GetAllOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Ordering.API.Controllers.v1
{
    [ApiVersion("1")]
    public class OrderController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IRepository<Package> _repository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BankTransaction> _bankTransactionRep;
        private readonly IUserService _userService;


        public OrderController(IMapper mapper, IMediator mediator, IRepository<Package> repository, IUserService userService,
          IRepository<Order> orderRepository, IRepository<BankTransaction> bankTransactionRep)
        {
            _mapper = mapper;
            _mediator = mediator;
            _repository = repository;
            _userService = userService;
            _orderRepository = orderRepository;
            _bankTransactionRep = bankTransactionRep;

        }


        [HttpGet("GetAllOrders")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<GetAllOrderQueryResult>> GetAllOrders(int Page, int PageSize, string userName, DateTime? StartDate, DateTime? EndDate, CancellationToken cancellationToken)
        {
            PageResult<GetAllOrderQueryResult> _AllOrders = await _mediator.
               Send(new GetAllOrderQuery { Page = Page, PageSize = PageSize, userName = userName, startDate = StartDate, endDate = EndDate });
            return _AllOrders;
        }


        [HttpGet("GetSuccessOrders")]
        [AllowAnonymous]
        public async Task<PageResult<GetSuccessOrdersQueryResult>> GetSuccessOrders(int Page, int PageSize, string tid, DateTime? StartDate, DateTime? EndDate, CancellationToken cancellationToken)
        {
            if (tid == "O2afaaFddsOpmnB%%*7$$8564oXkpghrt")
            {
                PageResult<GetSuccessOrdersQueryResult> _AllOrders = await _mediator.
                    Send(new GetSuccessOrdersQuery { Page = Page, PageSize = PageSize, startDate = StartDate, endDate = EndDate });
                return _AllOrders;

            }
            else
            {
                return null;
            }
        }


        [HttpGet("OrdersCount")]
        [Authorize(Roles = "Admin")]
        public OrderCountDto OrdersCount()
        {
            int OrderCount = _orderRepository.TableNoTracking.Count();
            int OrderSuccessCount = _bankTransactionRep.TableNoTracking.Count();
            int OrderUnSuccessCount = OrderCount - OrderSuccessCount;
            OrderCountDto orderCountDto = new OrderCountDto
            {
                OrderCount = OrderCount,
                OrderSuccessCount = OrderSuccessCount,
                OrderUnSuccessCount = OrderUnSuccessCount
            };

            return orderCountDto;
        }

        [HttpGet("GetOrdersCountByDate")]
        [Authorize(Roles = "Admin")]
        public OrderCountDto GetOrdersCountByDate(DateTime dateTime)
        {
            int OrderCount = _orderRepository.TableNoTracking.Count(o => o.CreateDate.Date == dateTime.Date);
            int OrderSuccessCount = _bankTransactionRep.TableNoTracking.Count(o => o.DateTime.Date == dateTime.Date);

            OrderCountDto orderCountDto = new OrderCountDto
            {
                OrderCount = OrderCount,
                OrderSuccessCount = OrderSuccessCount
            };

            return orderCountDto;
        }

        [HttpGet("GetDailySalesAmount")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<GetDailySalesAmountViewModel>> GetDailySalesAmount(DateTime dateTime)
        {
            var result = new GetDailySalesAmountViewModel
            {
                SamanAmount = await _bankTransactionRep.TableNoTracking.Where(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.Saman).Select(s => s.Amount).SumAsync(),
                MelatAmount = await _bankTransactionRep.TableNoTracking.Where(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.Melat).Select(s => s.Amount).SumAsync(),
                CafeBazarAmount = await _bankTransactionRep.TableNoTracking.Where(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.CafeBazar).Select(s => s.Amount).SumAsync(),
                MyketAmount = await _bankTransactionRep.TableNoTracking.Where(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.Myket).Select(s => s.Amount).SumAsync(),
                YekPayAmount = await _bankTransactionRep.TableNoTracking.Where(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.YekPay).Select(s => s.Amount).SumAsync(),
                DiscountAmount = await _bankTransactionRep.TableNoTracking.Where(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.Discount).Select(s => s.Amount).SumAsync(),

                SamanCount = await _bankTransactionRep.TableNoTracking.CountAsync(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.Saman),
                MelatCount = await _bankTransactionRep.TableNoTracking.CountAsync(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.Melat),
                CafeBazarCount = await _bankTransactionRep.TableNoTracking.CountAsync(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.CafeBazar),
                MyketCount = await _bankTransactionRep.TableNoTracking.CountAsync(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.Myket),
                YekPayCount = await _bankTransactionRep.TableNoTracking.CountAsync(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.YekPay),
                DiscountCount = await _bankTransactionRep.TableNoTracking.CountAsync(b => b.DateTime.Date == dateTime.Date && b.Bank == Domain.Enum.Bank.Discount),


            };

            return Ok(result);
        }

        [HttpGet("GetUserIdByDate")]
        [Authorize(Roles = "Admin")]
        public async Task<List<int>> GetUserIdByDate(DateTime dateTime)
        {
            return _bankTransactionRep.TableNoTracking.Include(o => o.Order).Where(b => b.DateTime.Date == dateTime.Date)
                .Select(b => b.Order.UserId).ToList();
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("Bank")]
        public IActionResult Bank()
        {
            return Ok(EnumExtensions.GetEnumNameValues<Bank>());
        }

        [HttpPost("DiscountCheck")]
        public async Task<IActionResult> DiscountCheck(int UserId, string Code, int PackageId)
        {
            var discountSelectDto = await _mediator.Send(new GetDiscountQuery { UserId = UserId, Code = Code, PackageId = PackageId });
            return Ok(discountSelectDto);
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(OrderDto orderDto, CancellationToken cancellationToken)
        {
            var order = await _mediator.Send(new CreateOrderCommand
            {
                UserId = orderDto.UserId,
                PackageId = orderDto.PackageId,
                IsDiscountActive = orderDto.IsDiscountActive,
                DiscountId = orderDto.DiscountId,
                DiscountUser = orderDto.DiscountUser,
            }, cancellationToken);

            return Ok(order.Id);
        }



        [HttpPost("AddOrderCafeBazar")]
        public async Task<IActionResult> AddOrderCafeBazar(CafeBazarModel orderDto, CancellationToken cancellationToken)
        {
            orderDto.Language = LanguageName ?? "Persian";
            var order = await _mediator.Send(new CreateOrderBazarCommand { cafeBazarModel = orderDto }, cancellationToken);

            return Ok(order.Id);
        }

        [HttpPost("AddOrderYekPay")]
        public async Task<ApiResult<string>> AddOrderYekPay([FromBody] OrderYekPayDto orderYekPayDto, CancellationToken cancellationToken)
        {
            string _returnUrl = await _mediator.Send(new CreateYekPayCommand { Order = orderYekPayDto.ToEntity(_mapper), IsDiscountActive = orderYekPayDto.IsDiscountActive, DiscountUser = orderYekPayDto.DiscountUser });

            if (_returnUrl != null)
            {
                return Ok(_returnUrl);
            }
            else
            {
                return new ApiResult<string>(false, ApiResultStatusCode.BadRequest, null, "Bank Not Response");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder(int OrderId, int BankId)
        {
            try
            {
                Order _order = await _mediator.Send(new GetOrderQuery { Id = OrderId });

                if (_order != null && _order.BankTransaction == null)
                {
                    Bank _name = (Bank)BankId;

                    switch (_name)
                    {
                        case Domain.Enum.Bank.Saman:
                            {

                                string _token = await SepToken.Get(_order.Id.ToString(), $"{_order.Amount}0");


                                SamanSelectDto samanSelectDto = new SamanSelectDto()
                                {
                                    Token = _token,
                                    PostUrl = SepConfig.PostUrl,
                                    RedirectUrl = SepConfig.RedirectUrl
                                };

                                return Ok(samanSelectDto);

                            }
                        case Domain.Enum.Bank.Melat:
                            {
                                string date = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                                string time = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');

                                Certificate.BypassCertificateError();

                                var payment = new MelatBank.PaymentGatewayClient();

                                var result = await payment.bpPayRequestAsync(int.Parse(BpmConfig.TerminalID),
                                                                                BpmConfig.UserName,
                                                                                BpmConfig.Password,
                                                                                long.Parse(_order.Id.ToString()),
                                                                                long.Parse($"{_order.Amount.ToString()}0"),
                                                                                date,
                                                                                time,
                                                                                "خرید از اپلیکیشن",
                                                                                BpmConfig.RedirectUrl,
                                                                                "0");


                                string[] res = result.Body.@return.Split(',');

                                MelatSelectDto melatSelectDto = new MelatSelectDto();

                                if (res[0] == "0")
                                {
                                    melatSelectDto.isError = false;
                                    melatSelectDto.RefId = res[1];
                                    melatSelectDto.ErrorMessage = null;
                                    melatSelectDto.PostUrl = BpmConfig.PostUrl;
                                    melatSelectDto.RedirectUrl = BpmConfig.RedirectUrl;

                                    await _mediator.Send(new UpdateMelatRefIdCommand { Order = _order, RefId = res[1] });
                                }
                                else
                                {
                                    melatSelectDto.isError = true;
                                    melatSelectDto.RefId = null;
                                    melatSelectDto.ErrorMessage = "خطای " + res[0] + " در ارتباط با بانک";
                                }

                                return Ok(melatSelectDto);
                            }
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message + ex.InnerException + ex.StackTrace);
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("SamanResponse")]
        public async Task<IActionResult> SamanResponse(SamanResult samanResult)
        {
            bool isError = false;
            string errorMsg = "";
            string succeedMsg = "";

            try
            {
                if (samanResult.State.Equals("OK"))
                {
                    int _OrderId = Convert.ToInt32(samanResult.ResNum);

                    Order _order = await _mediator.Send(new GetOrderQuery { Id = _OrderId });

                    if (_order != null && _order.BankTransaction == null)
                    {
                        string _TotalBankSend = $"{_order.Amount}0";
                        double _TotallConvert = Convert.ToDouble(_TotalBankSend);

                        //Accept Payment
                        var result = await VerifyTransaction.Check(samanResult.RefNum);

                        //test method not pay accept
                        //var result = _TotallConvert;


                        if (result > 0)
                        {
                            // چک کردن مبلغ بازگشتی از سرویس با مبلغ تراکنش
                            if (result == _TotallConvert)
                            {
                                succeedMsg = "پرداخت شما با موفقیت انجام شد";

                                await _mediator.Send(new CreateBankTransactionCommand { Order = _order, SamanResult = samanResult, MelatResult = null, Bank = Domain.Enum.Bank.Saman, Language = LanguageName ?? "Persian" });
                            }
                            else
                            {
                                //بازگشت دادن مبلغ چون مبلغ ها یکسان نیستند
                                ReverseTransaction.Send(samanResult.RefNum, samanResult.MID);
                            }
                        }
                        else
                        {
                            errorMsg = TransactionChecking.Check(out isError, (int)result);
                        }
                    }
                }
                else
                {
                    isError = true;
                    errorMsg = "پرداخت آنلاین با خطا مواجه شده است";
                }
            }
            catch (Exception ex)
            {
                isError = true;
                errorMsg = ex.Message;
            }

            SamanMessage samanMessage = new SamanMessage()
            {
                isError = isError,
                errorMsg = errorMsg,
                succeedMsg = succeedMsg
            };

            return Ok(samanMessage);
        }

        [AllowAnonymous]
        [HttpPost("MelatResponse")]
        public async Task<IActionResult> MelatResponse(MelatResult melatResult)
        {
            bool isError = false;
            string errorMsg = "";
            string succeedMsg = "";

            try
            {
                Order order = await _mediator.Send(new GetOrderByRefIdQuery { RefId = melatResult.RefId });

                if (order != null)
                {
                    Certificate.BypassCertificateError();

                    var payment = new MelatBank.PaymentGatewayClient();

                    var result = await payment.bpVerifyRequestAsync(Convert.ToInt64(BpmConfig.TerminalID),
                                                                    BpmConfig.UserName,
                                                                    BpmConfig.Password,
                                                                    Convert.ToInt64(order.Id.ToString()),
                                                                    Convert.ToInt64(melatResult.SaleOrderId),
                                                                    Convert.ToInt64(melatResult.SaleReferenceId));


                    if (result.Body.@return == "0")
                    {
                        succeedMsg = "پرداخت شما با موفقیت انجام شد";
                        await _mediator.Send(new CreateBankTransactionCommand { Order = order, SamanResult = null, MelatResult = melatResult, Bank = Domain.Enum.Bank.Melat, Language = LanguageName ?? "Persian" });
                    }
                    else
                    {
                        isError = true;
                        succeedMsg = $"پرداخت آنلاین با خطا {result.Body.@return} مواجه شده است.";
                    }
                }
            }
            catch (Exception ex)
            {
                isError = true;
                errorMsg = ex.Message;
            }

            MelatMessage melatMessage = new MelatMessage
            {
                isError = isError,
                errorMsg = errorMsg,
                succeedMsg = succeedMsg
            };

            return Ok(melatMessage);
        }

        [AllowAnonymous]
        [HttpPost("YekPayResponse")]
        public async Task<ApiResult<YekPayMessage>> YekPayResponse(YekPayResult yekPayResult)
        {
            var response = await _mediator.Send(new VerifyYekPayCommand { YekPayResult = yekPayResult, Language = LanguageName ?? "English" });
            return Ok(response);
        }

        [HttpPost("CheckUserPackage")]
        public async Task<IActionResult> CheckUserPackage(int UserId)
        {
            string ExpireTime = await _mediator.Send(new GetPackageExpireTimeQuery { UserId = UserId });
            return Ok(ExpireTime);
        }

        [HttpPost("CheckOrderById")]
        public async Task<IActionResult> CheckOrderById(int OrderId)
        {
            GetOrderQueryResult getOrderQueryResult = new GetOrderQueryResult();

            var order = await _mediator.Send(new GetOrderQuery { Id = OrderId });

            if (order != null)
            {
                if (order.BankTransaction != null)
                {
                    getOrderQueryResult.State = true;

                    switch (order.BankTransaction.Bank)
                    {
                        case Domain.Enum.Bank.Melat:
                            {
                                getOrderQueryResult.TrackingCode = order.BankTransaction.SaleReferenceId;
                                break;
                            }
                        case Domain.Enum.Bank.Saman:
                            {
                                getOrderQueryResult.TrackingCode = order.BankTransaction.TraceNo;
                                break;
                            }
                        case Domain.Enum.Bank.YekPay:
                            {
                                getOrderQueryResult.TrackingCode = order.BankTransaction.Authority;
                                break;
                            }
                        case Domain.Enum.Bank.CafeBazar:
                            {
                                getOrderQueryResult.TrackingCode = order.BankTransaction.SaleReferenceId;
                                break;
                            }
                        case Domain.Enum.Bank.Myket:
                            {
                                getOrderQueryResult.TrackingCode = order.BankTransaction.SaleReferenceId;
                                break;
                            }
                        default:
                            break;
                    }
                }
                else
                {
                    getOrderQueryResult.State = false;
                }
            }

            return Ok(getOrderQueryResult);
        }


        [HttpGet("GetOrdersByUserId")]
        public async Task<List<GetAllOrderQueryResult>> GetOrdersByUserId(int UserId)
        {
            List<GetAllOrderQueryResult> getAllOrderQueryResults = await _mediator
                  .Send(new GetOrderByUserIdQuery { UserId = UserId });

            return getAllOrderQueryResults;

        }

    }
}
