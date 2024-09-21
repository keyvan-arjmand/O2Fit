using Common;
using Data.Contracts;
using Domain.Common;
using MediatR;
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
    public class GetSuccessOrdersQueryHandler : IRequestHandler<GetSuccessOrdersQuery, PageResult<GetSuccessOrdersQueryResult>>, ITransientDependency
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BankTransaction> _banktransaction;
        private readonly IRepository<Package> _PackageRepository;
        private readonly IMediator _mediator;
        private readonly IUserService _UserService;


        public GetSuccessOrdersQueryHandler(IRepository<Order> orderRepository, IRepository<BankTransaction> banktransaction,
            IRepository<Package> PackageRepository, IMediator mediator, IUserService UserService)
        {
            _orderRepository = orderRepository;
            _PackageRepository = PackageRepository;
            _mediator = mediator;
            _banktransaction = banktransaction;
            _UserService = UserService;
        }


        public async Task<PageResult<GetSuccessOrdersQueryResult>> Handle(GetSuccessOrdersQuery request, CancellationToken cancellationToken)
        {
            List<BankTransaction> Orders = await _banktransaction.Table.Where(a => (int)a.Bank == 0 || (int)a.Bank == 1 || (int)a.Bank==2).Include(a => a.Order).ThenInclude(o=>o.Package).ToListAsync();
  
            List<int> _listintUsers = new List<int>();


            if (request.startDate != null && request.endDate != null)
            {
                Orders = Orders.Where(a => a.Order.CreateDate.Date >= request.startDate && a.Order.CreateDate.Date <= request.endDate).ToList();
            }
            else if (request.startDate != null && request.endDate == null)
            {
                Orders = Orders.Where(a => a.Order.CreateDate.Date >= request.startDate).ToList();
            }
            else if (request.endDate != null && request.startDate == null)
            {
                Orders = Orders.Where(a => a.Order.CreateDate.Date <= request.endDate).ToList();
            }


            var countDetails = Orders.Count();
            Orders = Orders.OrderByDescending(u => u.Id)
                                       .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                       .Take(request.PageSize).ToList();


            List<GetSuccessOrdersQueryResult> _paging = new List<GetSuccessOrdersQueryResult>();

            if (Orders.Count > 0)
            {
                foreach (var item in Orders)
                {
                    List<int> _list = new List<int>();
                    _list.Add(item.Order.Package.NameId);
                    var translations = await _mediator.Send(new GetTranslationByIdQuery
                    {
                        Id = item.Order.Package.NameId,
                    });


                    BankTransaction banktransaction = null;
                    if (await _banktransaction.Table.AnyAsync(a => a.OrderId == item.Id))
                    {
                        banktransaction = await _banktransaction.Table.Where(b => b.OrderId == item.Id).FirstAsync();
                    }
                    var _res = await _UserService.GetUserById(item.Order.UserId, "dfg@%$rrtavhlk%%opdcxhjk*&%^%$#@");
                    var _CurrentUser = JsonConvert.DeserializeObject<User>(_res.data.ToString());
                   
                  
                    GetSuccessOrdersQueryResult invoicePaging = new GetSuccessOrdersQueryResult()
                    {
                        
                        Amount = item.Amount,
                        Bank = item.Bank.ToString() ,
                        CreateDate = item.Order.CreateDate,
                        Serial = item.Id,
                        Package = translations.Persian,
                        Username = _CurrentUser.userName,
                        Currency = (int)item.Order.Package.Currency,
                        NumberTracking=(int)item.Bank ==0 ? item.SaleReferenceId : (int) item.Bank == 1 ? item.TraceNo : ""

                    };

                    _paging.Add(invoicePaging);
                }
            }

            var result = new PageResult<GetSuccessOrdersQueryResult>
            {
                Count = countDetails,
                PageIndex = request.Page ?? 1,
                PageSize = request.PageSize,
                Items = _paging
            };

            return result;
        }
    }
}
