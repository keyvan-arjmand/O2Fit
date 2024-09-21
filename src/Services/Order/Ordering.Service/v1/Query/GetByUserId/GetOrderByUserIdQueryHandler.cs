using Common;
using Data.Contracts;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ordering.Domain.Entities.Order;
using Ordering.Domain.Entities.Package;
using Ordering.Domain.Entities.Payment;
using Ordering.Domain.Models;
using Ordering.Service.Services;
using Service.v1.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Query.GetAllOrder
{
    public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrderByUserIdQuery, List<GetAllOrderQueryResult>>, ITransientDependency
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BankTransaction> _banktransaction;
        private readonly IRepository<Package> _PackageRepository;
        private readonly IMediator _mediator;
        private readonly IUserService _UserService;
        public GetOrderByUserIdQueryHandler(IRepository<Order> orderRepository, IRepository<BankTransaction> banktransaction,
            IRepository<Package> PackageRepository, IMediator mediator, IUserService UserService)
        {
            _orderRepository = orderRepository;
            _PackageRepository = PackageRepository;
            _mediator = mediator;
            _banktransaction = banktransaction;
            _UserService = UserService;
        }

        public async Task<List<GetAllOrderQueryResult>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            List<Order> _Orders = await _orderRepository.Table.Where(a=>a.UserId==request.UserId)
                .Include(a => a.Package)
                .Include(a=>a.BankTransaction).ToListAsync();
  

            List<GetAllOrderQueryResult> _paging = new List<GetAllOrderQueryResult>();

            if (_Orders.Count > 0)
            {
                foreach (var item in _Orders)
                {
                    List<int> _list = new List<int>();
                    _list.Add(item.Package.NameId);
                    var translations = await _mediator.Send(new GetTranslationByIdQuery
                    {
                        Id = item.Package.NameId,
                    });

                    BankTransaction banktransaction = null;
                    if (await _banktransaction.Table.AnyAsync(a => a.OrderId == item.Id))
                    {
                        banktransaction = await _banktransaction.Table.Where(b => b.OrderId == item.Id).FirstAsync();
                    }

                    GetAllOrderQueryResult invoicePaging = new GetAllOrderQueryResult()
                    {
                        Amount = item.Amount,
                        CreateDate = item.CreateDate,
                        ExpireTime = item.ExpireTime,
                        OrderId = item.Id,
                        Package = translations.Persian,
                        Currency = (int)item.Package.Currency,
                        SecurePan = banktransaction != null ? banktransaction.SecurePan : "",
                        State = banktransaction != null ? banktransaction.State : "",

                    };
                    if(banktransaction != null)
                    {
                    _paging.Add(invoicePaging);
                    }
                }
            }

            return _paging;
        }

    }

}

