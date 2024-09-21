using MediatR;
using Ordering.Domain.Entities.Order;
using Ordering.Service.Services.Payment.Mellat;
using Ordering.Service.Services.Payment.Saman;
using Ordering.Service.Services.Payment.YekPay;

namespace Ordering.Service.v1.Command
{
    public class CreateBankTransactionCommand : IRequest
    {
        public Order Order { get; set; }
        public SamanResult SamanResult { get; set; }
        public MelatResult MelatResult { get; set; }
        public YekPayResult YekPayResult { get; set; }
        public Domain.Enum.Bank Bank { get; set; }
        public string Language { get; set; }
    }
}
