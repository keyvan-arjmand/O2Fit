using Common.Exceptions;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMessaging.API.Models;
using SocialMessaging.Domain.Entities.Market;
using SocialMessaging.Domain.Entities.Translation;
using SocialMessaging.Service.v1.Query.MarketMessage;
using SocialMessaging.Service.ViewModels.MarketMessage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace SocialMessaging.API.Controllers.v1
{
    [ApiVersion("1")]
    public class MarketMessageController : BaseController
    {
        private readonly IRepository<MarketMessage> _marketMessageRepository;
        private readonly IRepository<TranslationDto> _translationRepository;
        private readonly IMediator _mediator;

        public MarketMessageController(IRepository<MarketMessage> marketMessageRepository,
            IRepository<TranslationDto> translationRepository, IMediator mediator)
        {
            _marketMessageRepository = marketMessageRepository;
            _translationRepository = translationRepository;
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Post(IFormFile image, [FromForm] CreateMarketMessageDTO createMarketMessageDTO, CancellationToken cancellationToken)
        {
            try
            {
                int adminId = int.Parse(User.Claims.First(i => i.Type == "UserId").Value);


                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MarketMessageImages");


                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);


                string fileNameWithPath = Path.Combine(path, image.FileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                var marketMessage = new MarketMessage
                {
                    AdminId = adminId,
                    DateCreate = DateTime.Now,
                    StartDate = createMarketMessageDTO.StartDate,
                    EndDate = createMarketMessageDTO.EndDate,
                    Link = createMarketMessageDTO.Link,
                    Target = createMarketMessageDTO.Target,
                    Postpone = createMarketMessageDTO.Postpone,
                    Title = new TranslationDto
                    {
                        Arabic = createMarketMessageDTO.Title.Arabic,
                        English = createMarketMessageDTO.Title.English,
                        Persian = createMarketMessageDTO.Title.Persian
                    },
                    Description = new TranslationDto
                    {
                        Arabic = createMarketMessageDTO.Description.Arabic,
                        English = createMarketMessageDTO.Description.English,
                        Persian = createMarketMessageDTO.Description.Persian
                    },
                    ButtonName = new TranslationDto
                    {
                        Arabic = createMarketMessageDTO.ButtonName.Arabic,
                        English = createMarketMessageDTO.ButtonName.English,
                        Persian = createMarketMessageDTO.ButtonName.Persian
                    },
                    Image = image.FileName


                };
                await _marketMessageRepository.AddAsync(marketMessage, cancellationToken);

                return Ok();
            }
            catch (Exception e)
            {

                throw new AppException(e.Message);
            }

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Put(IFormFile image, [FromForm] UpdateMarketMessageDTO updateMarketMessageDTO, CancellationToken cancellationToken)
        {
            var pastMarketMessage = await _marketMessageRepository
                 .Table.Include(t => t.Title)
                 .Include(d => d.Description)
                 .Include(b => b.ButtonName)
                 .Where(m => m.Id == updateMarketMessageDTO.Id).FirstAsync();


            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MarketMessageImages");

            var pathCombine = Path.Combine(path, pastMarketMessage.Image);


            if (image != null)
            {
                if (System.IO.File.Exists(pathCombine))
                {
                    System.IO.File.Delete(pathCombine);
                }

                string fileNameWithPath = Path.Combine(path, image.FileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }




            pastMarketMessage.Title.Persian = updateMarketMessageDTO.Title.Persian;
            pastMarketMessage.Title.English = updateMarketMessageDTO.Title.English;
            pastMarketMessage.Title.Arabic = updateMarketMessageDTO.Title.Arabic;

            _translationRepository.Detach(pastMarketMessage.Title);
            await _translationRepository.UpdateAsync(pastMarketMessage.Title, cancellationToken);

            pastMarketMessage.Description.Persian = updateMarketMessageDTO.Description.Persian;
            pastMarketMessage.Description.English = updateMarketMessageDTO.Description.English;
            pastMarketMessage.Description.Arabic = updateMarketMessageDTO.Description.Arabic;

            await _translationRepository.UpdateAsync(pastMarketMessage.Description, cancellationToken);

            pastMarketMessage.ButtonName.Persian = updateMarketMessageDTO.ButtonName.Persian;
            pastMarketMessage.ButtonName.Arabic = updateMarketMessageDTO.ButtonName.Arabic;
            pastMarketMessage.ButtonName.English = updateMarketMessageDTO.ButtonName.English;

            await _translationRepository.UpdateAsync(pastMarketMessage.ButtonName, cancellationToken);
            if (image != null)
            {
                pastMarketMessage.Image = image.FileName;
            }

            pastMarketMessage.Link = updateMarketMessageDTO.Link;
            pastMarketMessage.EndDate = updateMarketMessageDTO.EndDate;
            pastMarketMessage.StartDate = updateMarketMessageDTO.StartDate;
            pastMarketMessage.Target = updateMarketMessageDTO.Target;
            pastMarketMessage.Postpone = updateMarketMessageDTO.Postpone;

            await _marketMessageRepository.UpdateAsync(pastMarketMessage, cancellationToken);




            return Ok();
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<List<GetAllMarketMessageViewModel>>> GetAll(int? Page, int PageSize, CancellationToken cancellationToken)
        {
            return Ok(await _marketMessageRepository.TableNoTracking.Include(b => b.ButtonName)
                  .Include(t => t.Title).Include(d => d.Description)
                  .Skip((Page - 1 ?? 0) * PageSize)
                  .Take(PageSize)
                  .Select(s => new GetAllMarketMessageViewModel
                  {
                      Id = s.Id,
                      AdminId = s.AdminId,
                      ButtonName = s.ButtonName.Persian,
                      Description = s.Description.Persian,
                      DateCreate = s.DateCreate,
                      Image = s.Image,
                      Postpone = s.Postpone,
                      StartDate = s.StartDate,
                      EndDate = s.EndDate,
                      Link = s.Link,
                      Target = s.Target,
                      Title = s.Title.Persian
                  })
                  .OrderByDescending(t => t.Id)
                  .ToListAsync(cancellationToken));
        }

        [HttpGet("GetById")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<MarketMessage>> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _marketMessageRepository.TableNoTracking.Include(b => b.ButtonName)
                  .Include(t => t.Title).Include(d => d.Description).Where(m => m.Id == id)
                  .FirstOrDefaultAsync(cancellationToken));
        }

        [HttpGet("GetByDateUser")]
        [Authorize]
        public async Task<ApiResult<GetByDateUserViewModel>> GetByDateUser()
        {
            return Ok(await _mediator.Send(new GetByDateUserQuery()));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            var marketMessage = await _marketMessageRepository.Table.Include(b => b.ButtonName)
                   .Include(t => t.Title).Include(d => d.Description).Where(m => m.Id == id)
                   .FirstOrDefaultAsync(cancellationToken);
            if (marketMessage != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MarketMessageImages");

                var pathCombine = Path.Combine(path, marketMessage.Image);


                if (System.IO.File.Exists(pathCombine))
                {
                    System.IO.File.Delete(pathCombine);
                }

                await _marketMessageRepository.DeleteAsync(marketMessage, cancellationToken);

                await _translationRepository.DeleteAsync(marketMessage.Title, cancellationToken);
                await _translationRepository.DeleteAsync(marketMessage.Description, cancellationToken);
                await _translationRepository.DeleteAsync(marketMessage.ButtonName, cancellationToken);

                return Ok();

            }
            else
            {
                throw new AppException("Not Found");
            }



        }

    }
}
