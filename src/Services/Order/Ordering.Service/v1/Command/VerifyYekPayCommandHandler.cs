using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ordering.Domain.Entities.Order;
using Ordering.Service.Services;
using Ordering.Service.Services.Payment.YekPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Command
{
    public class VerifyYekPayCommandHandler : IRequestHandler<VerifyYekPayCommand, YekPayMessage>, IScopedDependency
    {
        private readonly IRepository<Order> _repository;
        private readonly IMediator _mediator;

        public VerifyYekPayCommandHandler(IRepository<Order> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<YekPayMessage> Handle(VerifyYekPayCommand request, CancellationToken cancellationToken)
        {
            YekPayMessage yekPayMessage = new YekPayMessage();

            try
            {
                var order = await _repository.TableNoTracking.SingleOrDefaultAsync(a => a.Authority == request.YekPayResult.Authority, cancellationToken);

                yekPayMessage.OrderId = order.Id;

                if (true || request.YekPayResult.Success == "1")
                {
                    string paramz = "";
                    paramz = "merchantId=" + YekPayConfig.merchant;
                    paramz += "&authority=" + request.YekPayResult.Authority;

                    clsRestAPI api = new clsRestAPI();

                    //Payment Real
                    string res = api.LoadWebSite(YekPayConfig.verifyUrl, paramz);
                    verify_response result = JsonConvert.DeserializeObject<verify_response>(res);

                    //Payment Facke
                    //verify_response result = new verify_response { 
                    //    Code = "100",
                    //    Authority = order.Authority
                    //};

                    if (result.Code == "100")
                    {
                        await _mediator.Send(new CreateBankTransactionCommand
                        {
                            Order = order,
                            SamanResult = null,
                            MelatResult = null,
                            YekPayResult = request.YekPayResult,
                            Bank = Domain.Enum.Bank.YekPay,
                            Language=request.Language
                        });

                        yekPayMessage.succeedMsg = "Payment Completed";
                        yekPayMessage.isError = false;
                    }
                    else
                    {
                        yekPayMessage.errorMsg = "Error : " + result.Description;
                        yekPayMessage.isError = true;
                    }
                }
                else
                {
                    yekPayMessage.errorMsg = "Payment Cancelled";
                    yekPayMessage.isError = true;
                }
            }
            catch (Exception ex)
            {
                yekPayMessage.errorMsg = "Error : " + ex.Message;
                yekPayMessage.isError = true;
            }

            return yekPayMessage;

        }
    }
}
