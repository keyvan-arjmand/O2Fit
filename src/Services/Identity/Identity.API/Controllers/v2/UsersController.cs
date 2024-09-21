using Common;
using Common.Exceptions;
using Data.Contracts;
using Domain;
using Identity.API.Infrastructure;
using Identity.API.Models;
using Identity.Data.Contracts;
using Identity.Service.Services.GenerateReferralCode;
using Identity.Service.Services.Sms;
using Identity.Service.v1.Query;
using Identity.Service.v1.Query.GetUserInformation;
using Identity.Service.v2.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Models;
using Service.v2.Command;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Identity.API.Controllers.v2
{
    [AllowAnonymous]
    [ApiVersion("2")]
    public class UsersController : BaseController
    {
        private readonly IReferralCodeGenerator _referralCodeGenerator;
        private readonly IUserRepository userRepository;
        private readonly UserManager<User> userManager;
        private readonly IEmailSender _emailSender;
        private readonly IMediator _mediator;
        private readonly IJwtService jwtService;
        private readonly ISmsService _smsService;
        private readonly IRedisCacheClient _redisCacheClient;

        public UsersController(IUserRepository userRepository,
            IRedisCacheClient redisCacheClient,
            UserManager<User> userManager,
            IEmailSender emailSender,
            IMediator mediator, IJwtService jwtService,
            IReferralCodeGenerator referralCodeGenerator,
            ISmsService smsService)

        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            _emailSender = emailSender;
            _mediator = mediator;
            this.jwtService = jwtService;
            _referralCodeGenerator = referralCodeGenerator;
            _smsService = smsService;
            _redisCacheClient = redisCacheClient;
        }

        [HttpPost]
        public async Task<ApiResult> Create(UserDto userDto, CancellationToken cancellationToken)
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



            var userDTO = await _mediator.Send(new GetUserQuery { UserName = userDto.UserName.ToLower() }, cancellationToken);

            UsernameConfirm _confirm;
            string code;
            if (userDTO == null)
            {
                string inviterUser = null;

                if (!string.IsNullOrEmpty(userDto.ReferreralInviter))
                {
                    userDto.ReferreralInviter = userDto.ReferreralInviter.Trim();
                    bool checkInviter =
                        await _mediator.Send(new GetReferreralCodeQuery { code = userDto.ReferreralInviter },
                            cancellationToken);

                    if (checkInviter)
                    {
                        inviterUser = userDto.ReferreralInviter.ToUpper();
                    }
                    else
                    {
                        return new ApiResult(isSuccess: false, ApiResultStatusCode.NotFound,
                            "Not Found ReferralInviter");
                    }
                }

                code = GenerateCode.Number(5);

                User user = new User
                {
                    Email = email.ToLower(),
                    PhoneNumber = phone,
                    CountryId = userDto.CountryId,
                    Language = userDto.LanguageId,
                    ReferreralInviter = inviterUser,
                    RegisterDate = DateTime.Now,
                    StartOfWeek = userDto.StartOfWeek,
                    ReferreralCode = _referralCodeGenerator.Generate().ToUpper(),
                    UserName = userDto.UserName,
                    ConfirmCode = code,
                    ConfirmCodeExpireTime = DateTime.Now.AddMinutes(2)
                };

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }


                _confirm = new UsernameConfirm
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


                // await _redisCacheClient.Db6.AddAsync($"ConfirmCode_{user.UserName.ToLower()}", _confirm);
            }
            else
            {
                code = GenerateCode.Number(5);

                _confirm = new UsernameConfirm
                {
                    Code = code,
                    Username = userDTO.UserName,
                    ExpireTime = DateTime.Now.AddSeconds(120)
                };

                if (UsernameValidate.IsPhone(userDTO.UserName))
                {
                    //await smsIdentity.VerificationCodeAsync(user.UserName, code);
                    await _smsService.SendSmsNewVersionSmsIr(userDTO.UserName, code);
                }
                else
                {
                    await _emailSender.SenderAsync(code, "O2FIT", userDTO.UserName, LanguageName);
                }

                await _mediator.Send(new AddConfirmationCodeCommand
                {
                    Username = userDTO.UserName.ToUpper(),
                    ConfirmCode = _confirm.Code,
                    ConfirmCodeExpireTime = _confirm.ExpireTime
                },
                    cancellationToken).ConfigureAwait(false);


                //await _redisCacheClient.Db6.AddAsync($"ConfirmCode_{userDTO.UserName.ToLower()}", _confirm);
            }

            return new ApiResult(isSuccess: true, ApiResultStatusCode.Success);
        }

        [Route("ConfirmCode")]
        [HttpPost]
        public async Task<ApiResult<ConfirmCodeViewModel>> ConfirmCode(ConfirmCodeDto confirmCodeDto,
            CancellationToken cancellationToken)
        {

            ConfirmCodeViewModel check = new ConfirmCodeViewModel();

            var usernameConfirm =
                await _mediator.Send(
                    new GetUserByUsernameAndConfirmCodeQuery { Code = confirmCodeDto.Code, Username = confirmCodeDto.Username },
                    cancellationToken);
            //     await _redisCacheClient.Db6.GetAsync<UsernameConfirm>($"ConfirmCode_{confirmCodeDto.Username.ToLower()}");

            // await _redisCacheClient.Db6.RemoveAsync($"ConfirmCode_{confirmCodeDto.Username.ToLower()}");

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

            if (usernameConfirm.ConfirmCode == confirmCodeDto.Code)
            {

                var user = await _mediator.Send(new GetUserConfirmQuery { UserName = confirmCodeDto.Username.ToUpper() }, cancellationToken);

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
                    ReferreralCode = user.ReferreralCode,
                    ReferreralInviter = user.ReferreralInviter == null ? null : user.ReferreralInviter,
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


        [Route("GetUtcTime")]
        [HttpGet]
        public ApiResult<UtcDataTimeViewModel> GetUtcTime()
        {
            var date = DateTime.UtcNow;
            var result = new UtcDataTimeViewModel
            {
                UtcDateTime = date.Year + "-" + date.Month + "-" + date.Day
            };
            return Ok(result);

        }


        [Route("IsExist")]
        [HttpPost]
        public async Task<ApiResult<IsExistUserViewModel>> IsExist(IsExistUserDto isExistUserDto,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.TableNoTracking.SingleOrDefaultAsync(
                a => a.UserName.ToLower() == isExistUserDto.UserName, cancellationToken);
            if (user == null)
                return Ok(new IsExistUserViewModel { IsNew = true });
            return Ok(new IsExistUserViewModel { IsNew = false });
        }


        [HttpGet("GetUsersRegisterInfoInLimitDate")]
        [Authorize(Roles = "Admin")]
        public List<UsersRegisterInfoInLimitDate> GetUsersRegisterInfoInLimitDate()
        {

            List<UsersRegisterInfoInLimitDate> list = new List<UsersRegisterInfoInLimitDate>();

            for (int i = 0; i > -30; i--)
            {
                DateTime date1 = DateTime.Now.AddDays(i);

                var activeCount = userRepository.TableNoTracking
                    .Count(u => u.RegisterDate.Date == date1.Date && u.IsActive);

                var registerCount = userRepository.TableNoTracking
                    .Count(u => u.RegisterDate.Date == date1.Date);

                list.Add(new UsersRegisterInfoInLimitDate
                {
                    Date = date1.Date,
                    ActiveCount = activeCount,
                    RegisterCount = registerCount
                });

            }
            list = list.OrderByDescending(u => u.Date).ToList();
            return list;
        }


        [HttpPost("AssignReferralCode")]
        [Authorize]
        public async Task<ApiResult> AssignReferralCode(AssignReferralCodeDTO assignReferralCodeDto,
            CancellationToken cancellationToken)
        {
            bool isExist = await _mediator.Send(new GetReferreralCodeQuery
            {
                code = assignReferralCodeDto.Code
            }, cancellationToken);

            if (isExist)
            {
                var user = await userRepository.Table.SingleOrDefaultAsync(a =>
                    a.Id == assignReferralCodeDto.UserId, cancellationToken);
                if (user == null)
                {
                    throw new AppException(ApiResultStatusCode.NotFound, "User Not Found");
                }

                user.ReferreralInviter = assignReferralCodeDto.Code;
                await userRepository.UpdateAsync(user, cancellationToken);

                return Ok();
            }

            throw new AppException(ApiResultStatusCode.BadRequest, "Invalid Referral Code");

        }

        [HttpPost("AdminLogin")]
        public async Task<ApiResult> AdminLogin(AdminLoginDTO adminLoginDto, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                User user = await userRepository.Table.SingleOrDefaultAsync(a => a.NormalizedUserName == adminLoginDto.UserName.ToUpper(), cancellationToken);

                if (user == null)
                {
                    throw new AppException(ApiResultStatusCode.NotFound, "نام کاربری اشتباه است");
                }

                var result = await userManager.CheckPasswordAsync(user, adminLoginDto.Password);

                if (result == false)
                {
                    throw new AppException(ApiResultStatusCode.NotFound, "گذر واژه اشتباه است");
                }
                var role = await userManager.GetRolesAsync(user);

                if (role.All(r => r == "Customer" || r == "Nutritionist"))
                {
                    throw new AppException(ApiResultStatusCode.ServerError, "کاربر دسترسی به پنل ندارد");
                }

                string _code = GenerateCode.Number(5);

                UsernameConfirm _confirm = new UsernameConfirm
                {
                    Code = _code,
                    Username = user.UserName,
                    ExpireTime = DateTime.Now.AddSeconds(120)
                };

                if (UsernameValidate.IsPhone(user.UserName))
                {
                    await _smsService.SendSmsNewVersionSmsIr(user.UserName, _code);
                }
                else
                {
                    await _emailSender.SenderAsync(_code, "O2FIT", user.UserName, LanguageName);
                }

                //await _redisCacheClient.Db6.AddAsync($"ConfirmCode_{user.UserName.ToLower()}", _confirm);
                user.ConfirmCode = _code;
                user.ConfirmCodeExpireTime = _confirm.ExpireTime;
                await userRepository.UpdateAsync(user, cancellationToken);

                return Ok();

            }

            return Ok();
        }



        // [Route("CreateRole")]
        // [HttpPost]
        // [Authorize(Roles = "Admin")]
        // public async Task<ApiResult> CreateRole(CreateRoleDTO createRoleDto)
        // {
        //     await _roleManager.CreateAsync(new Role
        //     {
        //        Name = createRoleDto.Name.ToUpper(),
        //        Description = $"{createRoleDto.Name.ToLower()} role"
        //     });
        //     
        //     return Ok();
        // }

        // [HttpGet]
        // [Route("GetAllRole")]
        // [Authorize("Admin")]
        // public async Task<ApiResult<List<Role>>> GetAllRole()
        // {
        //    var roles = await  _roleManager.Roles.Distinct().ToListAsync();
        //    return Ok(roles);
        // }


        // [HttpPost]
        // [Route("AssignRoleToUser")]
        // public async Task<ApiResult> AssignRoleToUser()
        // {
        //
        // }

        [HttpGet("GetUserIdByUserName")]
        [Authorize(Roles = "Admin")]
        public async Task<GetUserIdByUserNameViewModel> GetUserIdByUserName(string userName, CancellationToken cancellationToken)
        {

            return await _mediator.Send(new GetUserIdByUserNameQuery { UserName = userName }, cancellationToken);

        }
    }
}