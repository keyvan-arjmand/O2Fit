using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Messages.Contracts.Services.Payments.Transaction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Payment.Application.Transactions.V1.Command.UpdateTransactionMellat;
using Payment.Application.Transactions.V1.Command.UpdateTransactionSaman;
using Payment.Application.Transactions.V1.Query.GetByBankOrderId;
using Payment.Domain.Enums;
using Payment.Web.BankServices.Mellat;
using Payment.Web.BankServices.Saman;
using Payment.Web.Models;
using static MassTransit.ValidationResultExtensions;
using SamanMessage = Payment.Web.Models.SamanMessage;
using SamanResult = Payment.Web.Models.SamanResult;

namespace Payment.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankApiController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BankApiController> _logger;

        public BankApiController(IMediator mediator, ILogger<BankApiController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Mellat")]
        public async Task<ActionResult> VerifyMellatRequest(MellatBackDataDto model)
        {
            bool isError = false;
            string error = string.Empty;
            string succeed = string.Empty;

            try
            {
                var transAction = await _mediator.Send(new GetByBankOrderIdQuery { BankOrderId = Convert.ToInt64(model.SaleOrderId) });

                Certificate.BypassCertificateError();
                var payment = new MellatBank.PaymentGatewayClient();

                var result = await payment.bpVerifyRequestAsync(Convert.ToInt64(BpmConfig.TerminalId),
                    BpmConfig.UserName,
                    BpmConfig.Password,
                    Convert.ToInt64(model.SaleOrderId),
                    Convert.ToInt64(model.SaleOrderId),
                    Convert.ToInt64(model.SaleReferenceId));

                if (result.Body.@return == "0")
                {
                    succeed = "پرداخت شما با موفقیت انجام شد";

                    await _mediator.Send(new UpdateTransactionMellatCommand
                    {
                        MellatResult = new Application.Dtos.Mellat.MellatResult
                        {
                            RefId = model.RefId,
                            ResCode = model.ResCode,
                            SaleOrderId = Convert.ToInt32(model.SaleOrderId),
                            SaleReferenceId = model.SaleReferenceId,
                        },
                        Status = PaymentResult.Success,
                        TransactionId = transAction.Id,
                        UserName = transAction.UserName
                    });
                }
                else
                {
                    await _mediator.Send(new UpdateTransactionMellatCommand
                    {
                        MellatResult = new Application.Dtos.Mellat.MellatResult
                        {
                            RefId = model.RefId,
                            ResCode = model.ResCode,
                            SaleOrderId = Convert.ToInt32(model.SaleOrderId),
                            SaleReferenceId = model.SaleReferenceId,
                        },
                        Status = PaymentResult.Failed,
                        TransactionId = transAction.Id,
                        UserName = transAction.UserName
                    });
                    isError = true;
                    succeed = $"پرداخت آنلاین با خطا {result.Body.@return} مواجه شده است.";
                }

            }
            catch (Exception e)
            {
                isError = true;
                error = e.Message;
            }

            return Ok(new MellatMessage
            {
                IsError = isError,
                ErrorMessage = error,
                SucceedMessage = succeed
            });
        }

        [HttpPost("Saman")]
        public async Task<ActionResult> VerifySamanRequest(SamanResult model)
        {
            bool isError = false;
            string error =string.Empty;
            string succeed = string.Empty;
            try
            {
                var transAction = await _mediator.Send(new GetByBankOrderIdQuery { BankOrderId = Convert.ToInt64(model.ResNum) });

                if (model.State.Equals("OK"))
                {
                    if (transAction != null)
                    {
                        double amountTransaction = Convert.ToDouble($"{transAction.FinalAmount}0");
                        var result = await VerifyTransaction.Check(model.RefNum);

                        if (result > 0)
                        {
                            if (result == amountTransaction)
                            {
                                succeed = "پرداخت شما با موفقیت انجام شد";
                                await _mediator.Send(new UpdateTransactionSamanCommand
                                {
                                    SamanResult = new Application.Dtos.Saman.SamanResult()
                                    {
                                      CId = model.CId,
                                      MId = model.MId,
                                      RefNum = model.RefNum,
                                      ResNum = model.ResNum,
                                      SecurePan = model.SecurePan,
                                      State = model.State,
                                      TraceNo = model.TraceNo,
                                      StateCode = model.StateCode,
                                    },
                                    Status = PaymentResult.Success,
                                    TransactionId = transAction.Id,
                                    UserName = transAction.UserName
                                });
                            }
                            else
                            {
                                await _mediator.Send(new UpdateTransactionSamanCommand
                                {
                                    SamanResult = new Application.Dtos.Saman.SamanResult()
                                    {
                                        CId = model.CId,
                                        MId = model.MId,
                                        RefNum = model.RefNum,
                                        ResNum = model.ResNum,
                                        SecurePan = model.SecurePan,
                                        State = model.State,
                                        TraceNo = model.TraceNo,
                                        StateCode = model.StateCode,
                                    },
                                    Status = PaymentResult.Failed,
                                    TransactionId = transAction.Id,
                                    UserName = transAction.UserName
                                });
                                ReverseTransaction.Send(model.RefNum, model.MId);
                            }
                        }
                        else
                        {
                            await _mediator.Send(new UpdateTransactionSamanCommand
                            {
                                SamanResult = new Application.Dtos.Saman.SamanResult()
                                {
                                    CId = model.CId,
                                    MId = model.MId,
                                    RefNum = model.RefNum,
                                    ResNum = model.ResNum,
                                    SecurePan = model.SecurePan,
                                    State = model.State,
                                    TraceNo = model.TraceNo,
                                    StateCode = model.StateCode,
                                },
                                Status = PaymentResult.Failed,
                                TransactionId = transAction.Id,
                                UserName = transAction.UserName
                            });
                            error = TransactionChecking.Check(out isError, (int)result);
                        }
                    }
                }
                else
                {
                    await _mediator.Send(new UpdateTransactionSamanCommand
                    {
                        SamanResult = new Application.Dtos.Saman.SamanResult()
                        {
                            CId = model.CId,
                            MId = model.MId,
                            RefNum = model.RefNum,
                            ResNum = model.ResNum,
                            SecurePan = model.SecurePan,
                            State = model.State,
                            TraceNo = model.TraceNo,
                            StateCode = model.StateCode,
                        },
                        Status = PaymentResult.Failed,
                        TransactionId = transAction.Id,
                        UserName = transAction.UserName
                    });
                    isError = true;
                    error = "پرداخت آنلاین با خطا مواجه شده است";
                }
            }
            catch (Exception e)
            {
                isError = true;
                error = e.Message;
            }
            SamanMessage samanMessage = new SamanMessage()
            {
                IsError = isError,
                ErrorMsg = error,
                SucceedMsg = succeed
            };

            return Ok(samanMessage);
        }
    }
}
