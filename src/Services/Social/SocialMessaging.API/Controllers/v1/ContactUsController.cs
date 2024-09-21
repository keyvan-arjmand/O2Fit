using AutoMapper;
using Common;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Social.Domain.Enum;
using SocialMessaging.API.Models;
using SocialMessaging.Common.Utilities;
using SocialMessaging.Data.Contracts;
using SocialMessaging.Domain.Entities.ContactUs;
using SocialMessaging.Service.v1.Command.PutGeneralMessage;
using SocialMessaging.Service.v1.Query;
using SocialMessaging.Service.v1.Query.GetLastGeneralMessage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace SocialMessaging.API.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1")]
    public class ContactUsController : BaseController
    {
        private readonly IContactUsMessageRepository _repository;
        private readonly IRepositoryRedis<ContactUsMessage> _repositoryRedis;
        private readonly IRepositoryRedis<List<ContactUsMessage>> _repositoryLastMessagesRedis;
        private readonly IRepository<ContactUsMessageReader> _contactUsMessageReaderRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ContactUsController(IContactUsMessageRepository repository,
            IRepository<ContactUsMessageReader> contactUsMessageReaderRepository,
            IRepositoryRedis<List<ContactUsMessage>> repositoryLastMessagesRedis,
            IRepositoryRedis<ContactUsMessage> repositoryRedis,
            IWebHostEnvironment environment, IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
            _repository = repository;
            _environment = environment;
            _repositoryRedis = repositoryRedis;
            _repositoryLastMessagesRedis = repositoryLastMessagesRedis;
            _contactUsMessageReaderRepository = contactUsMessageReaderRepository;
        }


        #region Contact Us Message
        [HttpPost]
        public async Task<ApiResult<ContactUsMessage>> PostAsync(ContactUsMessageDTO contactUsMessageDTO, CancellationToken cancellationToken)
        {
            ContactUsMessage contactUsMessage = new ContactUsMessage
            {
                ToAdmin = true,
                CanReply = true,
                IsForce = false,
                IsGeneral = false,
                Language = LanguageName == null ? "Persian" : LanguageName,
                AdminId = 0,
                Title = contactUsMessageDTO.Title,
                Message = contactUsMessageDTO.Message,
                Classification = contactUsMessageDTO.Classification,
                ImageUri = null,
                InsertDate = DateTime.Now,
                IsRead = true,
                IsReadAdmin = false,
                ReplyToMessage = contactUsMessageDTO.ReplyToMessage,
                UserId = contactUsMessageDTO.UserId,
                Url = null
            };



            contactUsMessage = await _repository.AddAsync(contactUsMessage, cancellationToken);

            if (contactUsMessageDTO.ReplyToMessage > 0)
            {
                var parentMessage = await _repository.GetByIdAsync(cancellationToken, contactUsMessageDTO.ReplyToMessage);
                parentMessage.IsReadAdmin = false;
                parentMessage.CanReply = true;
                parentMessage.IsForce = false;
                parentMessage.IsGeneral = false;
                _repository.Detach(parentMessage);
                await _repository.UpdateAsync(parentMessage, cancellationToken);
            }
            return contactUsMessage;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<List<ContactUsMessage>>> GetAsync(int Id, CancellationToken cancellationToken)
        {
            List<ContactUsMessage> contactUsMessages = new List<ContactUsMessage>();
            var message = await _repository.Table.Where(m => m.Id == Id).FirstAsync(cancellationToken);
            contactUsMessages.Add(message);
            if (message.UserId != 0)
            {
                if (message.ReplyToMessage == 0)
                {
                    var childMessages = await _repository.Table.Where(m => m.ReplyToMessage == message.Id).ToListAsync(cancellationToken);

                    if (childMessages.Any())
                    {
                        contactUsMessages.AddRange(childMessages);
                    }
                }
                else
                {
                    var parentMessage = await _repository.Table.Where(m => m.Id == message.ReplyToMessage)
                        .FirstAsync(cancellationToken);
                    contactUsMessages.Add(parentMessage);
                    var childMessages = await _repository.Table.Where(m => m.ReplyToMessage == parentMessage.Id)
                        .ToListAsync(cancellationToken);
                    contactUsMessages.AddRange(childMessages);
                }
            }

            foreach (var item in contactUsMessages)
            {
                item.IsReadAdmin = true;
                _repository.Detach(item);
                await _repository.UpdateAsync(item, cancellationToken);
            }


            return contactUsMessages.OrderBy(m => m.InsertDate).ToList();
        }

        [HttpPut("ReadMessage")]
        public async Task<ApiResult<ContactUsMessage>> ReadMessageAsync(int Id, CancellationToken cancellationToken)
        {
            var message = await _repository.GetByIdAsync(cancellationToken, Id);
            message.CanReply = true;
            message.IsForce = false;
            message.IsGeneral = false;
            message.IsRead = true;
            await _repository.UpdateAsync(message, cancellationToken);

            //var contactUsMessageReader = new ContactUsMessageReader()
            //{
            //    MessageId = message.Id,
            //    UserId = message.UserId
            //};

            // await _contactUsMessageReaderRepository.AddAsync(contactUsMessageReader, cancellationToken);
            return message;
        }


        [HttpPost("Admin")]
        public async Task<ApiResult<ContactUsMessage>> PostAdminAsync(AdminContactUsMessageDTO contactUsMessageDTO, CancellationToken cancellationToken)
        {

            ContactUsMessage contactUsMessage = new ContactUsMessage
            {
                AdminId = contactUsMessageDTO.AdminId,
                ToAdmin = true,
                CanReply = true,
                IsGeneral = false,
                Classification = contactUsMessageDTO.Classification,
                ImageUri = contactUsMessageDTO.ImageUri,
                InsertDate = DateTime.Now,
                IsForce = false,
                IsRead = true,
                IsReadAdmin = false,
                Language = LanguageName == null ? "Persian" : LanguageName,
                Message = contactUsMessageDTO.Message,
                ReplyToMessage = contactUsMessageDTO.ReplyToMessage,
                Title = contactUsMessageDTO.Title,
                UserId = contactUsMessageDTO.UserId,
                Url = contactUsMessageDTO.Url
            };

            contactUsMessage = await _repository.AddAsync(contactUsMessage, cancellationToken);

            return contactUsMessage;
        }

        [HttpPost("AdminReplyMessage")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<ContactUsMessage>> PostReplyMessageAsync(AdminContactUsMessageDTO contactUsMessageDTO, CancellationToken cancellationToken)
        {
            ContactUsMessage contactUsMessage = contactUsMessageDTO.ToEntity(_mapper);
            contactUsMessage.UserId = contactUsMessageDTO.UserId;
            contactUsMessage.ToAdmin = false;
            contactUsMessage.InsertDate = DateTime.Now;
            contactUsMessage.CanReply = (contactUsMessageDTO.IsGeneral == true) ? false : true;
            contactUsMessage.ReplyToMessage = contactUsMessageDTO.ReplyToMessage;
            contactUsMessage.IsForce = false;
            contactUsMessage.IsGeneral = false;
            try
            {
                contactUsMessage = await _repository.AddAsync(contactUsMessage, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return contactUsMessage;
        }


        [HttpPost("[action]")]
        public async Task<ApiResult<ContactUsMessage>> SendMessageToUserAsync(AdminContactUsMessageDTO contactUsMessageDTO, string password, CancellationToken cancellationToken)
        {
            if (password == "265dc0Cd-5227-468A-bFef-7022c34Da490")
            {
                ContactUsMessage contactUsMessage = contactUsMessageDTO.ToEntity(_mapper);
                contactUsMessage.ToAdmin = false;
                contactUsMessage.InsertDate = DateTime.Now;
                contactUsMessage.CanReply = true;
                contactUsMessage.ReplyToMessage = 0;
                contactUsMessage.IsForce = false;
                contactUsMessage.IsGeneral = false;
                contactUsMessage.AdminId = contactUsMessageDTO.AdminId;
                contactUsMessage = await _repository.AddAsync(contactUsMessage, cancellationToken);
                return contactUsMessage;
            }
            return new ApiResult<ContactUsMessage>(false, ApiResultStatusCode.ServerError, null, "Server Not Response");
        }


        [HttpPost("WebMessage")]

        public async Task<ApiResult<ContactUsMessage>> PostWebMessageAsync(AdminContactUsMessageDTO contactUsMessageDTO, CancellationToken cancellationToken)
        {
            ContactUsMessage contactUsMessage = contactUsMessageDTO.ToEntity(_mapper);
            contactUsMessage.ToAdmin = true;
            contactUsMessage.CanReply = true;
            contactUsMessage.IsForce = false;
            contactUsMessage.IsGeneral = false;
            contactUsMessage = await _repository.AddAsync(contactUsMessage, cancellationToken);
            return contactUsMessage;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<ContactUsMessage>> GetAll(int? Page, int PageSize, CancellationToken cancellationToken)
        {
            List<ContactUsMessage> _paging = await _repository.GetParentMessages(Page, PageSize, cancellationToken);
            var result = new PageResult<ContactUsMessage>()
            {
                Items = _paging,
                Count = _paging.Count(),
                PageIndex = Page ?? 1,
                PageSize = PageSize,
            };
            return result;
        }


        [HttpGet("GetByUserId")]
        public async Task<ApiResult<IEnumerable<ContactUsMessage>>> GetByUserId(int userId, CancellationToken cancellationToken)
        {
            var messageList = (await _repository.GetByUserId(userId, LanguageName, cancellationToken)).ToList();
            return messageList;
        }

        [HttpDelete("[action]")]
        public async Task<ApiResult> DeleteAsync(int messageId, CancellationToken cancellationToken)
        {
            var messages = await _repository.GetAsync(messageId, cancellationToken);
            if (messages != null)
            {
                await _repository.DeleteRangeAsync(messages, cancellationToken);
                return Ok();
            }
            return new ApiResult(false, ApiResultStatusCode.ServerError);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<int> UnreadMessageCount(CancellationToken cancellationToken)
        {
            int unreadMessageCount = await _repository.TableNoTracking.Where(m => m.ToAdmin == true && m.IsReadAdmin == false).CountAsync(cancellationToken);
            return unreadMessageCount;
        }
        #endregion

        #region Market Message

        [HttpPost("CreatMarketMessage")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<ContactUsMessage>> CreatMarketMessageAsync(CancellationToken cancellationToken)
        {
            var jason = Request.Form;
            var file = jason.Files;
            AdminContactUsMessageDTO contactUsMessageDTO = JsonConvert.DeserializeObject<AdminContactUsMessageDTO>(Request.Form["JsonDetails"]);
            ContactUsMessage contactUsMessage = contactUsMessageDTO.ToEntity(_mapper);
            if (file.Count() > 0)
            {
                var image = jason.Files[0];
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MarketMessageImages", fileName);
                using (var strim = new FileStream(savePath, FileMode.Create))
                {
                    image.CopyTo(strim);
                }
                contactUsMessage.ImageUri = fileName;
            }
            contactUsMessage.ToAdmin = false;
            contactUsMessage.IsForce = true;
            contactUsMessage.IsGeneral = true;
            contactUsMessage.InsertDate = DateTime.Now;
            contactUsMessage.CanReply = false;
            contactUsMessage.ReplyToMessage = 0;
            contactUsMessage.Language = LanguageName ?? "Test";
            contactUsMessage = await _repository.AddAsync(contactUsMessage, cancellationToken);
            contactUsMessage.ImageUri = contactUsMessage.ImageUri == null ? null : Common.CommonStrings.CommonUrl + "MarketMessageImages/" + contactUsMessage.ImageUri;
            await _repositoryRedis.DeleteAsync("MarketMessage_" + contactUsMessage.Language);
            await _repositoryRedis.UpdateAsync("MarketMessage_" + contactUsMessage.Language, contactUsMessage);
            return contactUsMessage;
        }

        [HttpGet("MarketMessage")]
        public async Task<ApiResult<ContactUsMessage>> GetMarketMesage(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetMarketMessageQuery() { LanguageName = LanguageName ?? "English" });
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<ContactUsMessage>> GetAllMarketMessage(int? Page, int PageSize, CancellationToken cancellationToken)
        {
            List<ContactUsMessage> _paging = (await _repository.GetAllMarketMessage(cancellationToken))
                .Skip((Page - 1 ?? 0) * PageSize)
                .Take(PageSize)
                .ToList();

            var result = new PageResult<ContactUsMessage>()
            {
                Items = _paging,
                Count = _paging.Count(),
                PageIndex = Page ?? 1,
                PageSize = PageSize,
            };
            return result;
        }

        [HttpPut("UpdateMarketMessage")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<ContactUsMessage>> UpdateMarketMessageAsync(CancellationToken cancellationToken)
        {
            var jason = Request.Form;
            var file = jason.Files;
            AdminContactUsMessageDTO contactUsMessageDTO = JsonConvert.DeserializeObject<AdminContactUsMessageDTO>(Request.Form["JsonDetails"]);
            var oldMessage = _repository.TableNoTracking.Where(m => m.Id == contactUsMessageDTO.Id).FirstOrDefault();
            ContactUsMessage contactUsMessage = contactUsMessageDTO.ToEntity(_mapper);
            contactUsMessage.ImageUri = oldMessage.ImageUri;
            if (file.Count() > 0)
            {
                DeleteFile deleteFile = new DeleteFile(_environment);
                var image = jason.Files[0];
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MarketMessageImages");
                if (oldMessage.ImageUri != null)
                {
                    deleteFile.DeleteFiles(oldMessage.ImageUri, savePath);
                }
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var fullPath = Path.Combine(savePath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                contactUsMessage.ImageUri = fileName;
            }
            contactUsMessage.ToAdmin = false;
            contactUsMessage.IsForce = true;
            contactUsMessage.IsGeneral = true;
            contactUsMessage.InsertDate = DateTime.Now;
            contactUsMessage.CanReply = false;
            contactUsMessage.ReplyToMessage = 0;
            await _repository.UpdateAsync(contactUsMessage, cancellationToken);
            contactUsMessage.ImageUri = contactUsMessage.ImageUri == null ? null : Common.CommonStrings.CommonUrl + "MarketMessageImages/" + contactUsMessage.ImageUri;
            await _repositoryRedis.DeleteAsync("MarketMessage_" + LanguageName);
            await _repositoryRedis.UpdateAsync("MarketMessage_" + LanguageName, contactUsMessage);
            return contactUsMessage;
        }

        [HttpDelete("DeleteMarketMessage")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> DeleteMarketMessageAsync(int Id, CancellationToken cancellationToken)
        {
            var message = _repository.TableNoTracking.Where(m => m.Id == Id).FirstOrDefault();
            if (message.ImageUri != null)
            {
                DeleteFile deleteFile = new DeleteFile(_environment);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MarketMessageImages");
                deleteFile.DeleteFiles(message.ImageUri, savePath);
            }
            await _repository.DeleteAsync(message, cancellationToken);
            await _repositoryRedis.DeleteAsync("MarketMessage_" + message.Language);
            return Ok();
        }

        #endregion

        #region General Message

        [HttpGet("[action]")]
        public async Task<ApiResult<List<ContactUsMessage>>> UserMessagesAsync(int lastMessageId, int userId, CancellationToken cancellationToken)
        {
            var messages = new List<ContactUsMessage>();
            var generalMessages = await _mediator.Send(new GetLastGeneralMessageQuery()
            {
                language = LanguageName ?? "Persian",
                lastMessageId = lastMessageId
            });
            if (generalMessages != null) { messages.AddRange(generalMessages); }
            var marketMessage = await _mediator.Send(new GetMarketMessageQuery() { LanguageName = LanguageName ?? "English" });
            if (marketMessage != null) { messages.Add(marketMessage); }
            var userMessages = (await _repository.GetByUserId(userId, LanguageName, cancellationToken)).ToList();
            if (userMessages != null) { messages.AddRange(userMessages); }


            return messages;
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<ContactUsMessage>> PostGeneralMessageAsync(CancellationToken cancellationToken)
        {
            var jason = Request.Form;
            var file = jason.Files;
            AdminContactUsMessageDTO generalMessageDTO = JsonConvert.DeserializeObject<AdminContactUsMessageDTO>(Request.Form["JsonDetails"]);
            ContactUsMessage message = generalMessageDTO.ToEntity(_mapper);
            if (file.Count() > 0)
            {
                var image = jason.Files[0];
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "GeneralMessageImages", fileName);
                using (var strim = new FileStream(savePath, FileMode.Create))
                {
                    image.CopyTo(strim);
                }
                message.ImageUri = fileName;
            }
            message.IsForce = false;
            message.IsGeneral = true;
            message.ToAdmin = false;
            return await _repository.AddAsync(message, cancellationToken); ;
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<ContactUsMessage>> PutGeneralMessageAsync(AdminContactUsMessageDTO messageDTO, CancellationToken cancellationToken)
        {
            var jason = Request.Form;
            var file = jason.Files;
            AdminContactUsMessageDTO generalMessageDTO = JsonConvert.DeserializeObject<AdminContactUsMessageDTO>(Request.Form["JsonDetails"]);
            ContactUsMessage message = generalMessageDTO.ToEntity(_mapper);
            if (file.Count() > 0)
            {
                var image = jason.Files[0];
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "GeneralMessageImages", fileName);
                var oldMessage = await _repository.GetByIdAsync(cancellationToken, message.Id);
                if (oldMessage.ImageUri != null)
                {
                    DeleteFile deleteFile = new DeleteFile(_environment);
                    deleteFile.DeleteFiles(oldMessage.ImageUri, savePath);
                }
                using (var strim = new FileStream(savePath, FileMode.Create))
                {
                    image.CopyTo(strim);
                }
                message.ImageUri = fileName;
            }
            return await _mediator.Send(new PutGeneralMessageCommand() { generalMessage = message });
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<PageResult<ContactUsMessage>>> GetAllGeneralMessageAsync(int? Page, int PageSize, CancellationToken cancellationToken)
        {
            var _paging = await _repository.GetAllGeneralMessage(Page, PageSize, cancellationToken);
            var result = new PageResult<ContactUsMessage>()
            {
                Items = _paging.ToList(),
                Count = _paging.Count(),
                PageIndex = Page ?? 1,
                PageSize = PageSize,
            };
            return result;
        }

        [HttpGet("WebMessage")]
        public async Task<PageResult<ContactUsMessage>> WebMessage(int? page, int pageSize, CancellationToken cancellationToken)
        {
            List<ContactUsMessage> messages = await _repository.TableNoTracking
                .Where(m => m.IsForce == false && m.IsGeneral == false &&
                m.ToAdmin && m.IsReadAdmin == false && m.Classification == Classification.WebMessage && m.UserId == 0)
                .Skip((page - 1 ?? 0) * pageSize)
                .Take(pageSize)
                .OrderByDescending(a => a.Id)
                .ToListAsync(cancellationToken);



            var result = new PageResult<ContactUsMessage>()
            {
                Items = messages,
                Count = messages.Count()
            };
            return result;
        }

        #endregion
    }
}
