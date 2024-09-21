using AutoMapper;
using Common.Exceptions;
using Data.Contracts;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ordering.API.Models;
using Ordering.Domain.Entities.Discount;
using Ordering.Domain.Entities.Order;
using Ordering.Domain.Entities.Translation;
using Service.v1.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Ordering.API.Controllers.v1
{
    [ApiVersion("1")]
    public class DiscountController : BaseController
    {
        private readonly IRepository<Discount> _repository;
        private readonly IRepository<Order> _OrderRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DiscountController(IRepository<Discount> repository, IMapper mapper, IMediator mediator, IRepository<Order> orderRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _OrderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<PageResult<Discount>> Get(int? Page, int PageSize, CancellationToken cancellationToken, int userId)
        {

            List<Discount> discounts = await _repository.Table.Include(a => a.Translation)
                                                                        .Where(a => a.IsActive == true && a.EndDateTime > DateTime.Now)
                                                                        .ToListAsync(cancellationToken);

            var orders = await _OrderRepository.Table.Include(a => a.BankTransaction).Where(a => a.UserId == userId && a.BankTransaction != null).ToListAsync();

            if (orders.Count > 0)
            {
                var discountId = orders.Where(a => a.DiscountId != null).ToList();

                if (discountId.Count > 0)
                {
                    foreach (var item in discountId)
                    {
                        var disId = discounts.Where(a => a.Id == item.DiscountId).FirstOrDefault();

                        if (disId != null)
                        {
                            discounts.Remove(disId);
                        }

                    }
                }
            }

            var _disUser = discounts.Where(a => a.UserId != null).Any();

            if (_disUser)
            {
                List<Discount> checkUserId = discounts.Where(a => a.UserId != null && a.UserId != userId).ToList();

                if (checkUserId.Count > 0)
                {
                    foreach (var item in checkUserId)
                    {
                        discounts.Remove(item);
                    }
                }
            }

            var _disUserCount = discounts.Where(a => a.UsableCount != null).Any();

            if (_disUserCount)
            {
                List<Discount> checkUsable = discounts.Where(a => a.UsableCount != null && a.UsableCount == 0).ToList();

                if (checkUsable.Count > 0)
                {
                    foreach (var item in checkUsable)
                    {
                        discounts.Remove(item);
                    }
                }
            }

            int _count = discounts.Count();

            discounts = discounts.OrderByDescending(a => a.EndDateTime).Skip((Page - 1 ?? 0) * PageSize).Take(PageSize).ToList();

            if (discounts.Count > 0)
            {
                foreach (var item in discounts)
                {
                    _repository.Detach(item);
                }
            }

            if (orders.Count > 0)
            {
                foreach (var item in orders)
                {
                    _OrderRepository.Detach(item);
                }
            }

            var result = new PageResult<Discount>
            {
                Count = _count,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = discounts
            };

            return result;
        }


        [HttpGet("GetAllAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<DiscountDto>> GetAllAsync(int? Page, int PageSize
       , CancellationToken cancellationToken)
        {
            List<Discount> discounts = await _repository.Table.Include(m => m.Translation)
                                      .OrderByDescending(i => i.Id)
                                         .Skip((Page - 1 ?? 0) * PageSize)
                                         .Take(PageSize)
                                        .ToListAsync(cancellationToken);

            var countDetails = discounts.Count();
            List<DiscountDto> _paging = new List<DiscountDto>();

            if (discounts.Count > 0)
            {
                foreach (var item in discounts)
                {
                    DiscountDto invoicePaging = new DiscountDto()
                    {

                        Translation = new Translation()
                        {
                            Id = item.Translation.Id,
                            Arabic = item.Translation.Arabic,
                            English = item.Translation.English,
                            Persian = item.Translation.Persian
                        },
                        Code = item.Code,
                        EndDateTime = item.EndDateTime,
                        IsActive = item.IsActive,
                        Percent = item.Percent,
                        StartDate = item.StartDate,
                        UsableCount = item.UsableCount,
                        UserId = item.UserId,
                        Id = item.Id,
                    };

                    _paging.Add(invoicePaging);
                }
            }

            var result = new PageResult<DiscountDto>
            {
                Count = countDetails,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = _paging
            };

            return result;
        }

        [HttpGet("GetById")]
        [Authorize(Roles = "Admin")]
        public async Task<DiscountDto> GetByIdAsync(int Id
         , CancellationToken cancellationToken)
        {
            var discount = await _repository.Table
                .Include(t => t.Translation)
                .Where(m => m.Id == Id).FirstAsync(cancellationToken);


            DiscountDto result = new DiscountDto()
            {

                Translation = new Translation()
                {
                    Id = discount.Translation.Id,
                    Arabic = discount.Translation.Arabic,
                    English = discount.Translation.English,
                    Persian = discount.Translation.Persian
                },
                Code = discount.Code,
                EndDateTime = discount.EndDateTime,
                IsActive = discount.IsActive,
                Percent = discount.Percent,
                StartDate = discount.StartDate,
                UsableCount = discount.UsableCount,
                UserId = discount.UserId,
                Id = discount.Id,
            };

            return result;
        }


        [HttpPost]
        public async Task<ApiResult<Discount>> Post(DiscountDto discountDto, CancellationToken cancellationToken)
        {
            bool isExist = _repository.TableNoTracking.Any(d => d.Code == discountDto.Code);
            if (isExist)
                throw new AppException("DiscountCode Is duplicate");

            Discount _discount = discountDto.ToEntity(_mapper);
            Translation _Translation = await _mediator.Send(new CraeteTranslationCommand { Translation = _discount.Translation });
            _discount.NameId = _Translation.Id;
            await _repository.AddAsync(_discount, cancellationToken);
            return _discount;
        }

        [HttpPut]
        public async Task<ApiResult<Discount>> Put(DiscountDto discountDto, CancellationToken cancellationToken)
        {
            Discount _discount = discountDto.ToEntity(_mapper);
            await _repository.UpdateAsync(_discount, cancellationToken);
            return null;
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            Discount discount = _repository.TableNoTracking.SingleOrDefault(a => a.Id == id);

            if (discount == null)
            {
                return NotFound();
            }

            List<int> _ids = new List<int>();
            _ids.Add(discount.NameId);

            await _repository.DeleteAsync(discount, cancellationToken);

            await _mediator.Send(new DeleteTranslationCommand { Ids = _ids });

            return Ok();
        }
    }
}
