using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ordering.Domain.Entities.Order;
using Ordering.Service.Services.Payment.YekPay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Command
{
    public class CreateYekPayCommandHandler : IRequestHandler<CreateYekPayCommand, string>, IScopedDependency
    {
        private readonly IRepository<Order> _repository;
        private readonly IMediator _mediator;

        public CreateYekPayCommandHandler(IRepository<Order> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<string> Handle(CreateYekPayCommand request, CancellationToken cancellationToken)
        {
            var orderId = await _mediator.Send(new CreateOrderCommand
            {
                UserId = request.Order.UserId,
                PackageId = request.Order.PackageId,
                DiscountId = request.Order.DiscountId,
                IsDiscountActive = request.IsDiscountActive,
                DiscountUser = request.DiscountUser
            });

            var order = await _repository.TableNoTracking.SingleOrDefaultAsync(a => a.Id == orderId.Id);
            order.Email = request.Order.Email;
            order.Mobile = request.Order.Mobile;
            order.FirstName = request.Order.FirstName;
            order.LastName = request.Order.LastName;
            order.Address = request.Order.Address;
            order.PostalCode = request.Order.PostalCode;
            order.Country = request.Order.Country;
            order.City = request.Order.City;
            order.Description = request.Order.Description == null ? null : request.Order.Description;


            string paramz = "";
            paramz = "merchantId=" + YekPayConfig.merchant;
            paramz += "&fromCurrencyCode=" + YekPayConfig.fcc;
            paramz += "&toCurrencyCode=" + YekPayConfig.tcc;
            paramz += "&email=" + request.Order.Email;
            paramz += "&mobile=" + request.Order.Mobile;
            paramz += "&firstName=" + request.Order.FirstName;
            paramz += "&lastName=" + request.Order.LastName;
            paramz += "&address=" + request.Order.Address;
            paramz += "&postalCode=" + request.Order.PostalCode;
            paramz += "&country=" + request.Order.Country;
            paramz += "&city=" + request.Order.City;
            paramz += "&description=" + request.Order.Description;
            paramz += "&amount=" + Convert.ToDecimal(order.Amount);
            paramz += "&orderNumber=" + orderId.Id;
            paramz += "&callback=" + YekPayConfig.callback;

            clsRestAPI api = new clsRestAPI();

            string res = api.LoadWebSite(YekPayConfig.requestUrl, paramz);

            request_response result = JsonConvert.DeserializeObject<request_response>(res);

            if (result.Code == "100")
            {
                order.Authority = result.Authority;
                await _repository.UpdateAsync(order, cancellationToken);

                string Payment_URL = YekPayConfig.paymentUrl + result.Authority;
                return Payment_URL;
            }
            else
            {
                return null;
            }

        }
    }
}
