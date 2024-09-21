using MediatR;
using Ordering.Service.Services.Payment.YekPay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Command
{
    public class VerifyYekPayCommand : IRequest<YekPayMessage>
    {
        public YekPayResult YekPayResult { get; set; }
        public string Language { get; set; }
    }
}
