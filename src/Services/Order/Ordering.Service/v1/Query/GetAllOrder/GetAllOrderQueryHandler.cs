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
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, PageResult<GetAllOrderQueryResult>>, ITransientDependency
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BankTransaction> _banktransaction;
        private readonly IRepository<Package> _PackageRepository;
        private readonly IMediator _mediator;
        private readonly IUserService _UserService;
        public GetAllOrderQueryHandler(IRepository<Order> orderRepository, IRepository<BankTransaction> banktransaction,
            IRepository<Package> PackageRepository, IMediator mediator, IUserService UserService)
        {
            _orderRepository = orderRepository;
            _PackageRepository = PackageRepository;
            _mediator = mediator;
            _banktransaction = banktransaction;
            _UserService = UserService;
        }

        public async Task<PageResult<GetAllOrderQueryResult>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            List<Order> _Orders = await _orderRepository.Table.Include(a => a.Package).ToListAsync();
            List<Order> Orders = _Orders;
            List<int> _listintUsers = new List<int>();
            if (request.userName != null)
            {
                var users = await _UserService.GetUserByUserName(request.userName);
                _listintUsers = JsonConvert.DeserializeObject<List<int>>(users.data.ToString());
                Orders = new List<Order>();
                foreach (var item in _listintUsers)
                {
                    var Order = _Orders.Where(a => a.UserId == item).ToList();
                    if (Order != null)
                    {
                        foreach (var item1 in Order)
                        {
                            Orders.Add(item1);
                        }
                    }
                }
            }


            if (request.startDate != null && request.endDate != null)
            {
                Orders = Orders.Where(a => a.CreateDate.Date >= request.startDate && a.CreateDate.Date <= request.endDate).ToList();
            }
            else if (request.startDate != null && request.endDate == null)
            {
                Orders = Orders.Where(a => a.CreateDate.Date >= request.startDate).ToList();
            }
            else if (request.endDate != null && request.startDate == null)
            {
                Orders = Orders.Where(a => a.CreateDate.Date <= request.endDate).ToList();
            }


            var countDetails = Orders.Count();
            Orders = Orders.OrderByDescending(u => u.Id)
                                       .Skip((request.Page - 1 ?? 0) * request.PageSize)
                                       .Take(request.PageSize).ToList();


            List<GetAllOrderQueryResult> _paging = new List<GetAllOrderQueryResult>();

            if (Orders.Count > 0)
            {
                foreach (var item in Orders)
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
                    var _res = await _UserService.GetUserById(item.UserId, "dfg@%$rrtavhlk%%opdcxhjk*&%^%$#@");
                    var _CurrentUser = JsonConvert.DeserializeObject<User>(_res.data.ToString());

                    GetAllOrderQueryResult invoicePaging = new GetAllOrderQueryResult()
                    {
                        Amount = item.Amount,
                        BankId = banktransaction != null ? ((int)banktransaction.Bank) : 100,
                        CreateDate = item.CreateDate,
                        ExpireTime = item.ExpireTime,
                        OrderId = item.Id,
                        Package = translations.Persian,
                        Username = _CurrentUser.userName,
                        Currency = (int)item.Package.Currency,
                        SecurePan = banktransaction != null ? banktransaction.SecurePan : "",
                        State = banktransaction != null ? banktransaction.State : "",
                        RegisterDateUser=_CurrentUser.RegisterDate

                    };

                    _paging.Add(invoicePaging);
                }
            }

            var result = new PageResult<GetAllOrderQueryResult>
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

