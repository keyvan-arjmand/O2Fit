using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMessaging.API.Models;
using SocialMessaging.Domain.DTO;
using SocialMessaging.Domain.Entities.Market;
using SocialMessaging.Service.v1.Command.InternalLinkMarketMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace SocialMessaging.API.Controllers.v1
{
    [Authorize(Roles = "Admin")]
    [ApiVersion("1")]
    public class InternalLinkController : BaseController
    {
        private readonly IRepository<InternalLink> _internalLinkRepository;
        private readonly IMediator _mediator;

        public InternalLinkController(IRepository<InternalLink> internalLinkRepository, IMediator mediator)
        {
            _internalLinkRepository = internalLinkRepository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResult> PostAsync(CreateInternalLinkDTO createInternalLinkDTO, CancellationToken cancellationToken)
        {
            int adminId = int.Parse(User.Claims.First(i => i.Type == "UserId").Value);

            await _internalLinkRepository.AddAsync(new InternalLink
            {
                AdminId = adminId,
                DateCreate = DateTime.Now,
                Link = createInternalLinkDTO.Link,
                Name = createInternalLinkDTO.Name,
            }, cancellationToken);

            return Ok();
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult<List<InternalLink>>> GetAll(CancellationToken cancellationToken)
        {
            return await _internalLinkRepository.TableNoTracking.ToListAsync(cancellationToken);
        }

        [HttpGet("GetById")]
        public async Task<ApiResult<InternalLink>> GetById(int Id, CancellationToken cancellationToken)
        {
            return await _internalLinkRepository.GetByIdAsync(cancellationToken, Id);
        }

        [HttpPut]
        public async Task<ApiResult> Update(UpdateInternalLinkDTO updateInternalLinkDTO, CancellationToken cancellationToken)
        {

            int adminId = int.Parse(User.Claims.First(i => i.Type == "UserId").Value);

            await _mediator.Send(new UpdateInternalLinkCommand
            {
                AdminId = adminId,
                Id = updateInternalLinkDTO.Id,
                Link = updateInternalLinkDTO.Link,
                Name = updateInternalLinkDTO.Name,
            });

            return Ok();
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var internalLink = _internalLinkRepository.Table.Where(i => i.Id == Id).FirstOrDefault();
            await _internalLinkRepository.DeleteAsync(internalLink, cancellationToken);
            return Ok();
        }
    }
}
