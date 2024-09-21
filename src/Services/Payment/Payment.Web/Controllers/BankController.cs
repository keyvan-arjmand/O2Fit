using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.Web.Models;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using Amazon.Runtime;
using Common.Enums.TypeEnums;
using MediatR;
using Payment.Application.Dtos.Mellat;
using Payment.Application.Dtos.Saman;
using Payment.Application.Transactions.V1.Command.UpdateTransactionMellat;
using static MassTransit.ValidationResultExtensions;
using MellatResult = Payment.Application.Dtos.Mellat.MellatResult;
using SamanResult = Payment.Web.Models.SamanResult;
using Payment.Application.Transactions.V1.Command.UpdateTransactionSaman;
using Payment.Application.Transactions.V1.Query.GetByBankOrderId;
using Payment.Web.BankServices.Mellat;
using Payment.Web.BankServices.Saman;
using EventBus.Messages.Contracts.Services.Payments.Transaction;
using EventBus.Messages.Contracts.Services.Payments.Package;
using Payment.Application.Transactions.V1.Command.CreateTransaction;
using Payment.Application.Transactions.V1.Command.CreateTransactionWallet;
using Payment.Domain.Enums;
using MellatMessage = Payment.Web.Models.MellatMessage;
using SamanMessage = Payment.Web.Models.SamanMessage;

namespace Payment.Web.Controllers
{
    public class BankController : Controller
    {
        private readonly IRequestClient<GetTransactionByBankOrderId> _GetTransaction;
        private readonly HttpClient _httpClient;
        private readonly ILogger<BankController> _logger;
        private readonly IMediator _mediator;

        public BankController(ILogger<BankController> logger, IMediator mediator,
            IRequestClient<GetTransactionByBankOrderId> getTransaction, HttpClient httpClient)
        {
            _logger = logger;
            _mediator = mediator;
            _GetTransaction = getTransaction;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Bank(int orderId, int bankId)
        {
            return View("Bank");
        }

        public async Task<IActionResult> MellatPayment(int bankOrderId)
        {
            try
            {
                ViewBag.ErrorBank = null;

                MellatSelectDto mellatSelectDto = new MellatSelectDto();
                var transAction = await _mediator.Send(new GetByBankOrderIdQuery { BankOrderId = bankOrderId });

                _logger.LogInformation($"get mellat payment orderId = {transAction.BankOrderId}");
                if (transAction != null)
                {
                    string date = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') +
                                  DateTime.Now.Day.ToString().PadLeft(2, '0');
                    string time = DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                                  DateTime.Now.Minute.ToString().PadLeft(2, '0') +
                                  DateTime.Now.Second.ToString().PadLeft(2, '0');

                    Certificate.BypassCertificateError();

                    var payment = new MellatBank.PaymentGatewayClient();

                    var result = await payment.bpPayRequestAsync(int.Parse(BpmConfig.TerminalId),
                        BpmConfig.UserName,
                        BpmConfig.Password,
                        transAction.BankOrderId,
                        long.Parse($"{transAction.FinalAmount}0"),
                        date,
                        time,
                        "خرید از اپلیکیشن",
                        BpmConfig.RedirectUrl,
                        "0",
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty);


                    string[] res = result.Body.@return.Split(',');

                    if (res[0] == "0")
                    {
                        mellatSelectDto.IsError = false;
                        mellatSelectDto.RefId = res[1];
                        mellatSelectDto.ErrorMessage = null;
                        mellatSelectDto.PostUrl = BpmConfig.PostUrl;
                        mellatSelectDto.RedirectUrl = BpmConfig.RedirectUrl;
                        mellatSelectDto.BankOrderId = transAction.BankOrderId;
                    }
                    else
                    {
                        mellatSelectDto.IsError = true;
                        mellatSelectDto.RefId = null;
                        mellatSelectDto.ErrorMessage = "خطای " + res[0] + " در ارتباط با بانک";
                    }

                    if (mellatSelectDto.IsError == false)
                    {
                        ViewBag.RefId = mellatSelectDto.RefId;
                        ViewBag.RedirectURL = $"{mellatSelectDto.RedirectUrl}";
                        ViewBag.BankUrl = mellatSelectDto.PostUrl;
                    }
                    else
                    {
                        ViewBag.ErrorBank = mellatSelectDto.ErrorMessage;
                    }
                }
                else
                {
                    ViewBag.ErrorBank = "خطا در ارتباط با درگاه بانک ملت";
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorBank = e.Message;
            }

            return View("MellatPayment");
        }

        public async Task<IActionResult> MellatBack(string RefId, string ResCode, string SaleOrderId,
            string SaleReferenceId)
        {
            ViewBag.Message = string.Empty;
            ViewBag.RefId = RefId;
            ViewBag.ResCode = ResCode;
            ViewBag.SaleReferenceId = SaleReferenceId;
            //Order Id Back
            ViewBag.SaleOrderId = SaleOrderId;
            try
            {
                if (ResCode == "0")
                {
                    var mellatBackDto = new MellatBackDataDto
                    {
                        RefId = RefId,
                        ResCode = ResCode,
                        SaleOrderId = SaleOrderId,
                        SaleReferenceId = SaleReferenceId
                    };
                    var serializeData = JsonConvert.SerializeObject(mellatBackDto);
                    var content = new StringContent(serializeData, Encoding.UTF8, "application/json");
                    var client = await _httpClient.PostAsync("https://banktest.o2fitt.com/api/BankApi/Mellat", content);
                    _logger.LogInformation($"=>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>content info {content}");
                    if (client.IsSuccessStatusCode)
                    {
                        var value = await client.Content.ReadAsStringAsync();
                        MellatMessage mellatMessage = JsonConvert.DeserializeObject<MellatMessage>(value);

                        if (mellatMessage.IsError)
                        {
                            ViewBag.Message = mellatMessage.ErrorMessage;
                        }
                        else
                        {
                            ViewBag.Message = mellatMessage.SucceedMessage;
                        }
                    }
                    else
                    {
                        ViewBag.Message = "خطا در پرداخت تایید پرداخت";
                    }
                }
                else
                {
                    ViewBag.Message = "پرداخت آنلاین با خطا مواجه شده است.";
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "خطا در برقراری ارتباط با درگاه بانک ملت";
            }

            return View("MellatBack");
        }

        public async Task<IActionResult> SamanPayment(int bankOrderId)
        {
            try
            {
                ViewBag.ErrorBank = null;
                var transAction = await _mediator.Send(new GetByBankOrderIdQuery { BankOrderId = bankOrderId });
                if (transAction != null)
                {
                    string token =
                        await SepToken.Get(transAction.BankOrderId.ToString(), $"{transAction.FinalAmount}0");

                    SamanSelectDto samanSelectDto = new SamanSelectDto()
                    {
                        Token = token,
                        PostUrl = SepConfig.PostUrl,
                        RedirectUrl = SepConfig.RedirectUrl,
                        BankOrderId = transAction.BankOrderId
                    };

                    if (samanSelectDto != null)
                    {
                        ViewBag.BankToken = samanSelectDto.Token;
                        ViewBag.RedirectURL = $"{samanSelectDto.RedirectUrl}";
                        ViewBag.BankUrl = samanSelectDto.PostUrl;
                    }
                    else
                    {
                        ViewBag.ErrorBank = "خطا در اتصال به بانک";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorBank = e.Message;
            }

            return View("SamanPayment");
        }

        public async Task<IActionResult> SamanBack(string State, string StateCode, string ResNum,
            string MID, string RefNum, string CID, string TRACENO, string SecurePan)
        {
            #region ViewBag

            ViewBag.Message = string.Empty;
            ViewBag.State = State;
            ViewBag.StateCode = StateCode;
            ViewBag.MID = MID;
            ViewBag.RefNum = RefNum;
            ViewBag.CID = CID;
            ViewBag.TRACENO = TRACENO;
            ViewBag.SecurePan = SecurePan;
            //OrderId Back
            ViewBag.ResNum = ResNum;

            #endregion

            _logger.LogInformation($"=>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>call Api ResNum={ResNum}&CID={CID}&");
            try
            {
                if (State != null && ResNum != null && RefNum != null)
                {
                    SamanResult samanResult = new SamanResult()
                    {
                        CId = CID,
                        MId = MID,
                        RefNum = RefNum,
                        ResNum = ResNum,
                        SecurePan = SecurePan,
                        State = State,
                        StateCode = StateCode,
                        TraceNo = TRACENO,
                    };
                    var serializeData = JsonConvert.SerializeObject(samanResult);
                    var content = new StringContent(serializeData, Encoding.UTF8, "application/json");
                    var client = await _httpClient.PostAsync("https://banktest.o2fitt.com/api/BankApi/Saman", content);
                    _logger.LogInformation($"=>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>content info {content}");
                    if (client.IsSuccessStatusCode)
                    {
                        var value = await client.Content.ReadAsStringAsync();
                        SamanMessage? samanMessage = JsonConvert.DeserializeObject<SamanMessage>(value);
                        if (samanMessage.IsError)
                        {
                            ViewBag.Message = samanMessage.ErrorMsg;
                        }
                        else
                        {
                            ViewBag.Message = samanMessage.SucceedMsg;
                        }
                    }
                    else
                    {
                        ViewBag.Message = "خطا در پرداخت تایید پرداخت";
                    }
                }
                else
                {
                    ViewBag.Message = "خطا در پرداخت مجدد سعی نمایید";
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "خطا در پرداخت";
            }

            return View("SamanBack");
        }

        public RedirectResult BackApp([FromQuery] string orderId)
        {
            return Redirect("o2fitt://app/paymentResultScreen/1928637");
        }
    }
}