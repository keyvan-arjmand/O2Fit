using Data.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social.Domain.Enum;
using SocialMessaging.API.Models;
using SocialMessaging.Common.Utilities;
using SocialMessaging.Domain.Entities.ContactUs;
using SocialMessaging.Service.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace SocialMessaging.API.Controllers.v2
{

    [ApiVersion("2")]
    [Authorize(Roles = "Admin")]
    public class MessageController : BaseController
    {
        private readonly IRepository<ContactUsMessage> _repository;
        private readonly IUserService _userService;

        public MessageController(IRepository<ContactUsMessage> contactUsMessageRepository, IUserService userService)
        {
            _repository = contactUsMessageRepository;
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<PageResult<ContactUsMessageViewModel>> GetAll(int? page, int pageSize, CancellationToken cancellationToken)
        {

            List<ContactUsMessage> messages = await _repository.TableNoTracking
                .Where(m => m.UserId != 0 && m.ReplyToMessage == 0)
                .OrderByDescending(a => a.Id)
                .Skip((page - 1 ?? 0) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            List<ContactUsMessageViewModel> List = new List<ContactUsMessageViewModel>();

            foreach (var item in messages)
            {

                var userProfile = await _userService.GetUserProfile(item.UserId, accessToken);

                ContactUsMessageViewModel contactUsMessageViewModel = new ContactUsMessageViewModel
                {
                    UserId = item.UserId,
                    AdminId = item.AdminId,
                    Classification = item.Classification,
                    FullName = userProfile.FullName,
                    Id = item.Id,
                    ImageUri = userProfile.ImageUri,
                    InsertDate = item.InsertDate,
                    IsGeneral = item.IsGeneral,
                    IsRead = item.IsRead,
                    Message = item.Message,
                    Title = item.Title,

                    UserName = userProfile.UserName,
                };

                List.Add(contactUsMessageViewModel);

            }

            var result = new PageResult<ContactUsMessageViewModel>()
            {
                Items = List,
                Count = List.Count()
            };
            return result;
        }

        [HttpGet("Management")]
        public async Task<PageResult<ContactUsMessageViewModel>> Management(int? page, int pageSize, CancellationToken cancellationToken)
        {

            List<ContactUsMessage> messages = await _repository.TableNoTracking
                .Where(m => m.IsForce == false && m.IsGeneral == false &&
                m.ToAdmin && m.IsReadAdmin == false && m.Classification == Classification.Admin && m.UserId != 0)
                .Skip((page - 1 ?? 0) * pageSize)
                .Take(pageSize)
                .OrderByDescending(a => a.Id)
                .ToListAsync(cancellationToken);

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            List<ContactUsMessageViewModel> List = new List<ContactUsMessageViewModel>();

            foreach (var item in messages)
            {

                var userProfile = await _userService.GetUserProfile(item.UserId, accessToken);

                ContactUsMessageViewModel contactUsMessageViewModel = new ContactUsMessageViewModel
                {
                    UserId = item.UserId,
                    AdminId = item.AdminId,
                    Classification = item.Classification,
                    FullName = userProfile.FullName,
                    Id = item.Id,
                    ImageUri = userProfile.ImageUri,
                    InsertDate = item.InsertDate,
                    IsGeneral = item.IsGeneral,
                    IsRead = item.IsRead,
                    Message = item.Message,
                    Title = item.Title,

                    UserName = userProfile.UserName,
                };

                List.Add(contactUsMessageViewModel);

            }

            var result = new PageResult<ContactUsMessageViewModel>()
            {
                Items = List,
                Count = List.Count()
            };
            return result;
        }

        [HttpGet("Financial")]
        public async Task<PageResult<ContactUsMessageViewModel>> Financial(int? page, int pageSize, CancellationToken cancellationToken)
        {

            List<ContactUsMessage> messages = await _repository.TableNoTracking
                .Where(m => m.IsForce == false && m.IsGeneral == false &&
                m.ToAdmin && m.IsReadAdmin == false && m.Classification == Classification.Finance && m.UserId != 0)
                .Skip((page - 1 ?? 0) * pageSize)
                .Take(pageSize)
                .OrderByDescending(a => a.Id)
                .ToListAsync(cancellationToken);

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            List<ContactUsMessageViewModel> List = new List<ContactUsMessageViewModel>();

            foreach (var item in messages)
            {

                var userProfile = await _userService.GetUserProfile(item.UserId, accessToken);

                ContactUsMessageViewModel contactUsMessageViewModel = new ContactUsMessageViewModel
                {
                    UserId = item.UserId,
                    AdminId = item.AdminId,
                    Classification = item.Classification,
                    FullName = userProfile.FullName,
                    Id = item.Id,
                    ImageUri = userProfile.ImageUri,
                    InsertDate = item.InsertDate,
                    IsGeneral = item.IsGeneral,
                    IsRead = item.IsRead,
                    Message = item.Message,
                    Title = item.Title,

                    UserName = userProfile.UserName,
                };

                List.Add(contactUsMessageViewModel);

            }

            var result = new PageResult<ContactUsMessageViewModel>()
            {
                Items = List,
                Count = List.Count()
            };
            return result;
        }

        [HttpGet("CriticismSuggestions")]
        public async Task<PageResult<ContactUsMessageViewModel>> CriticismSuggestions(int? page, int pageSize, CancellationToken cancellationToken)
        {

            List<ContactUsMessage> messages = await _repository.TableNoTracking
                .Where(m => m.IsForce == false && m.IsGeneral == false &&
                m.ToAdmin && m.IsReadAdmin == false && m.Classification == Classification.HumanResource && m.UserId != 0)
                .Skip((page - 1 ?? 0) * pageSize)
                .Take(pageSize)
                .OrderByDescending(a => a.Id)
                .ToListAsync(cancellationToken);

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            List<ContactUsMessageViewModel> List = new List<ContactUsMessageViewModel>();

            foreach (var item in messages)
            {

                var userProfile = await _userService.GetUserProfile(item.UserId, accessToken);

                ContactUsMessageViewModel contactUsMessageViewModel = new ContactUsMessageViewModel
                {
                    UserId = item.UserId,
                    AdminId = item.AdminId,
                    Classification = item.Classification,
                    FullName = userProfile.FullName,
                    Id = item.Id,
                    ImageUri = userProfile.ImageUri,
                    InsertDate = item.InsertDate,
                    IsGeneral = item.IsGeneral,
                    IsRead = item.IsRead,
                    Message = item.Message,
                    Title = item.Title,

                    UserName = userProfile.UserName,
                };

                List.Add(contactUsMessageViewModel);

            }

            var result = new PageResult<ContactUsMessageViewModel>()
            {
                Items = List,
                Count = List.Count()
            };
            return result;
        }


        [HttpGet("CustomerService")]
        public async Task<PageResult<ContactUsMessageViewModel>> CustomerService(int? page, int pageSize, CancellationToken cancellationToken)
        {
            List<ContactUsMessage> messages = await _repository.TableNoTracking
                .Where(m => m.IsForce == false && m.IsGeneral == false &&
                m.ToAdmin && m.IsReadAdmin == false && m.Classification == Classification.TechnicalSupport && m.UserId != 0)
                .Skip((page - 1 ?? 0) * pageSize)
                .Take(pageSize)
                .OrderByDescending(a => a.Id)
                .ToListAsync(cancellationToken);

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            List<ContactUsMessageViewModel> List = new List<ContactUsMessageViewModel>();

            foreach (var item in messages)
            {

                var userProfile = await _userService.GetUserProfile(item.UserId, accessToken);

                ContactUsMessageViewModel contactUsMessageViewModel = new ContactUsMessageViewModel
                {
                    UserId = item.UserId,
                    AdminId = item.AdminId,
                    Classification = item.Classification,
                    FullName = userProfile.FullName,
                    Id = item.Id,
                    ImageUri = userProfile.ImageUri,
                    InsertDate = item.InsertDate,
                    IsGeneral = item.IsGeneral,
                    IsRead = item.IsRead,
                    Message = item.Message,
                    Title = item.Title,

                    UserName = userProfile.UserName,
                };

                List.Add(contactUsMessageViewModel);

            }

            var result = new PageResult<ContactUsMessageViewModel>()
            {
                Items = List,
                Count = List.Count()
            };
            return result;
        }

        [HttpGet("WebMessage")]
        public async Task<PageResult<ContactUsMessageViewModel>> WebMessage(int? page, int pageSize, CancellationToken cancellationToken)
        {
            List<ContactUsMessageViewModel> messages = await _repository.TableNoTracking
                .Where(m => m.IsForce == false && m.IsGeneral == false &&
                m.ToAdmin && m.IsReadAdmin == false && m.Classification == Classification.WebMessage && m.UserId == 0)
                .Skip((page - 1 ?? 0) * pageSize)
                .Take(pageSize)
                .Select(s => new ContactUsMessageViewModel
                {
                    UserId = s.UserId,
                    AdminId = s.AdminId,
                    Classification = s.Classification,
                    Id = s.Id,
                    InsertDate = s.InsertDate,
                    IsGeneral = s.IsGeneral,
                    IsRead = s.IsRead,
                    Message = s.Message,
                    Title = s.Title,
                })
                .OrderByDescending(a => a.Id)
                .ToListAsync(cancellationToken);



            var result = new PageResult<ContactUsMessageViewModel>()
            {
                Items = messages,
                Count = messages.Count()
            };
            return result;
        }

        [HttpGet("GetMessageByUserName")]
        public async Task<PageResult<ContactUsMessageViewModel>> GetMessageByUserName(string userName)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var user = await _userService.GetUserIdByUserName(userName, accessToken);
            if (user != null)
            {
                List<ContactUsMessageViewModel> List = new List<ContactUsMessageViewModel>();

                var messages = await _repository.TableNoTracking.Where(m => m.UserId == user.UserId).ToListAsync();
                var userProfile = await _userService.GetUserProfile(user.UserId, accessToken);
                foreach (var item in messages)
                {

                    ContactUsMessageViewModel contactUsMessageViewModel = new ContactUsMessageViewModel
                    {
                        UserId = item.UserId,
                        AdminId = item.AdminId,
                        Classification = item.Classification,
                        FullName = userProfile.FullName,
                        Id = item.Id,
                        ImageUri = userProfile.ImageUri,
                        InsertDate = item.InsertDate,
                        IsGeneral = item.IsGeneral,
                        IsRead = item.IsRead,
                        Message = item.Message,
                        Title = item.Title,

                        UserName = userProfile.UserName,
                    };

                    List.Add(contactUsMessageViewModel);

                }

                var result = new PageResult<ContactUsMessageViewModel>()
                {
                    Items = List,
                    Count = List.Count()
                };
                return result;
            }
            return null;

        }
    }
}
