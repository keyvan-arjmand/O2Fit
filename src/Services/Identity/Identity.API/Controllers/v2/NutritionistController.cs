using Data.Repositories;
using Identity.API.Infrastructure;
using Identity.API.Models;
using Identity.Service.Services.GenerateReferralCode;
using Identity.Service.v1.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Common;
using Domain;
using WebFramework.Api;
using Data.Contracts;
using Identity.Data.Contracts;
using Identity.Service.Services.Sms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Controllers.v2
{
    [AllowAnonymous]
    [ApiVersion("2")]
    public class NutritionistController : BaseController
    {

        private readonly IReferralCodeGenerator _referralCodeGenerator;
        private readonly IUserRepository userRepository;
        private readonly IRepositoryRedis<UsernameConfirm> repositoryRedis;
        private readonly UserManager<User> userManager;
        private readonly IEmailSender _emailSender;
        private readonly IJwtService jwtService;
        private readonly ISmsService _smsService;

        public NutritionistController(IReferralCodeGenerator referralCodeGenerator, IUserRepository userRepository, IRepositoryRedis<UsernameConfirm> repositoryRedis, UserManager<User> userManager, IEmailSender emailSender, IJwtService jwtService, ISmsService smsService)
        {
            _referralCodeGenerator = referralCodeGenerator;
            this.userRepository = userRepository;
            this.repositoryRedis = repositoryRedis;
            this.userManager = userManager;
            _emailSender = emailSender;
            this.jwtService = jwtService;
            _smsService = smsService;
        }

        [HttpPost]
        public async Task<ApiResult> Post(UserDto userDto, CancellationToken cancellationToken)
        {
            if (!UsernameValidate.IsPhone(userDto.UserName) && !UsernameValidate.IsEmail(userDto.UserName))
            {
                return new ApiResult(isSuccess: false, ApiResultStatusCode.BadRequest, "Invalid UserName");
            }

            string phone = "", email = "";
            if (UsernameValidate.IsPhone(userDto.UserName))
            {
                phone = userDto.UserName;
            }

            if (UsernameValidate.IsEmail(userDto.UserName))
            {
                email = userDto.UserName;
            }


            var user = await userRepository.TableNoTracking.FirstOrDefaultAsync(a => a.UserName == userDto.UserName,
                cancellationToken);

            if (user == null)
            {

                user = new User
                {
                    Email = email.ToLower(),
                    PhoneNumber = phone,
                    CountryId = userDto.CountryId,
                    Language = userDto.LanguageId,
                    ReferreralInviter = "",
                    RegisterDate = DateTime.Now,
                    StartOfWeek = userDto.StartOfWeek,
                    ReferreralCode = _referralCodeGenerator.Generate().ToUpper(),
                    UserName = userDto.UserName
                };

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Nutritionist");
                }
            }

            string code = GenerateCode.Number(5);

            UsernameConfirm _confirm = new UsernameConfirm()
            {
                Code = code,
                Username = user.UserName,
                ExpireTime = DateTime.Now.AddSeconds(120)
            };

            if (UsernameValidate.IsPhone(user.UserName))
            {
                //await smsIdentity.VerificationCodeAsync(user.UserName, code);
                await _smsService.SendSmsNewVersionSmsIr(user.UserName, code);
            }
            else
            {
                await _emailSender.SenderAsync(code, "O2FIT", user.UserName, LanguageName);
            }

            await repositoryRedis.UpdateAsync($"ConfirmCode_{user.UserName.ToLower()}", _confirm);



            return new ApiResult(isSuccess: true, ApiResultStatusCode.Success);
        }

        [Route("ConfirmCode")]
        [HttpPost]
        public async Task<ApiResult<ConfirmCodeViewModel>> ConfirmCode(ConfirmCodeDto confirmCodeDto,
            CancellationToken cancellationToken)
        {


            ConfirmCodeViewModel check = new ConfirmCodeViewModel();

            UsernameConfirm usernameConfirm =
                await repositoryRedis.GetAsync($"ConfirmCode_{confirmCodeDto.Username.ToLower()}");

            if (usernameConfirm == null)
            {
                check.IsActive = false;
                check.WrongCode = false;
                check.ExpireCode = true;
                return Ok(check);
            }

            if (DateTime.Now >= usernameConfirm.ExpireTime)
            {
                check.IsActive = false;
                check.WrongCode = false;
                check.ExpireCode = true;
                return Ok(check);
            }

            if (usernameConfirm.Code == confirmCodeDto.Code)
            {
                User user = await userRepository.TableNoTracking.FirstOrDefaultAsync(
                    a => a.UserName == confirmCodeDto.Username.ToLower(), cancellationToken);

                if (user == null)
                    return NotFound();

                if (UsernameValidate.IsPhone(user.UserName))
                {
                    user.PhoneNumberConfirmed = true;
                }
                else
                {
                    user.EmailConfirmed = true;
                }

                user.IsActive = true;
                await repositoryRedis.DeleteAsync($"ConfirmCode_{confirmCodeDto.Username}");

                await userRepository.UpdateAsync(user, cancellationToken);

                var roles = await userManager.GetRolesAsync(user);
                var jwt = await jwtService.GenerateAsync(user, roles.ToList());

                UserSelectViewModel userSelectViewModel = new UserSelectViewModel()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Language = user.Language,
                    CountryId = user.CountryId,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    AccessFailedCount = user.AccessFailedCount,
                    LockoutEnabled = user.LockoutEnabled,
                    ImageUri = user.ImageUri,
                    StartOfWeek = user.StartOfWeek,
                    Token = jwt
                };

                check.UserSelectViewModel = userSelectViewModel;


                return Ok(check);
            }

            check.IsActive = false;
            check.WrongCode = true;
            check.ExpireCode = false;


            return Ok(check);
        }

        [HttpGet("SendSmsRegisterNutritionist")]
        public async Task<ApiResult> SendSmsRegisterNutritionist(int userId)
        {
            var phone = await userRepository.TableNoTracking.Where(u => u.Id == userId)
                    .Select(u => u.PhoneNumber).FirstOrDefaultAsync();
            await _smsService.SendSmsRegisterNutritionist(phone);
            return Ok();
        }

        [HttpGet("GetUserIdByPhone")]
        [Authorize]
        public async Task<GetUserIdByPhoneViewModel> GetUserIdByPhone(string phone, CancellationToken cancellationToken)
        {
            return await userRepository.TableNoTracking.Where(u => u.UserName == phone)
                    .Select(s => new GetUserIdByPhoneViewModel { UserId = s.Id })
                    .FirstOrDefaultAsync(cancellationToken);

        }

    }
}
