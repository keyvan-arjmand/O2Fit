using AutoMapper;
using Common;
using Common.Utilities;
using Data.Contracts;
using Domain;
using Identity.API.Infrastructure;
using Identity.API.Models;
using Identity.Common.Utilities;
using Identity.Data.Contracts;
using Identity.Service.Services.Sms;
using Identity.Service.v1.Command;
using Identity.Service.v1.Query;
using Identity.Service.v1.Query.GetUserInformation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Identity.API.Controllers.v1
{
    [ApiVersion("1")]
    public class UsersController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IRepositoryRedis<UsernameConfirm> repositoryRedis;
        private readonly ISmsIdentity smsIdentity;
        private readonly ILogger<UsersController> logger;
        private readonly IJwtService jwtService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ISmsService _smsService;

        public UsersController(IUserRepository userRepository, IRepositoryRedis<UsernameConfirm> repositoryRedis,
            ISmsIdentity smsIdentity,
            IJwtService jwtService,
            UserManager<User> userManager, RoleManager<Role> roleManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            IMediator mediator, IMapper mapper, ISmsService smsService)
        {
            this.userRepository = userRepository;
            this.repositoryRedis = repositoryRedis;
            this.smsIdentity = smsIdentity;
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            _emailSender = emailSender;
            _mediator = mediator;
            _mapper = mapper;
            _smsService = smsService;
        }


        [HttpGet("UsersCount")]
        [Authorize(Roles = "Admin")]
        public int UsersCount()
        {
            int UsersCount = userRepository.Table.Count();
            //int CredibleUsersCount = userRepository.Table.Where(u=>u.).Count();
            //int UsersCount = userRepository.Table.Count();
            return UsersCount;
        }

        //Admin Use
        [HttpGet("GetById")]
        [AllowAnonymous]
        public virtual async Task<GetUsersInformationQueryResult> GetByUserId(int UserId, string grantpass, CancellationToken cancellationToken)
        {
            GetUsersInformationQueryResult _User = new GetUsersInformationQueryResult();

            if (grantpass == "dfg@%$rrtavhlk%%opdcxhjk*&%^%$#@")
            {
                _User = await _mediator.Send(new GetUsersInfomationQuery { UserId = UserId });
            }

            return _User;
        }

        [HttpGet("GetByUserIdAdmin")]
        [Authorize(Roles = "Admin")]
        public virtual async Task<GetUsersInformationQueryResult> GetByUserIdAdmin(int UserId, CancellationToken cancellationToken)
        {

            var user = await _mediator.Send(new GetUsersInfomationQuery { UserId = UserId });

            return user;
        }

        [HttpGet("Search")]
        [Authorize(Roles = "Admin")]
        public virtual async Task<PageResult<GetUsersInformationQueryResult>> Search(string Name, DateTime StartDate, DateTime EndDate, int? Page, int PageSize, CancellationToken cancellationToken)
        {
            PageResult<GetUsersInformationQueryResult> SearchResult = await _mediator.Send(
                new GetUsersQuery
                {
                    Name = Name,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    Page = Page,
                    PageSize = PageSize
                });
            return SearchResult;
        }

        //[HttpGet("GetById")]
        //[AllowAnonymous]
        //public virtual async Task<ActionResult<User>> GetByUserId(int UserId, CancellationToken cancellationToken)
        //{
        //    User _user = await userManager.FindByIdAsync(UserId.ToString());
        //    return _user;
        //}


        [HttpGet("GetIdByUserName")]
        [AllowAnonymous]
        public virtual async Task<List<int>> GetIdByUserName(string userName, CancellationToken cancellationToken)
        {
            List<int> _list = new List<int>();
            var _users = await userRepository.Table.Where(a => a.UserName.Contains(userName)).ToListAsync(cancellationToken);
            foreach (var item in _users)
            {
                _list.Add(item.Id);
            }
            return _list;
        }


        [HttpGet("CheckReferreralCode")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckReferreralCode(string code)
        {
            return Ok(await _mediator.Send(new GetReferreralCodeQuery { code = code }));
        }

        [HttpGet("CheckUserActiveReferreralCode")]
        public virtual async Task<IActionResult> CheckUserActiveReferreralCode(int userId)
        {
            return Ok(await _mediator.Send(new GetActiveUserReferreralCodeQuery { UserId = userId }));
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult DayOfWeeks()
        {
            return Ok(EnumExtensions.GetEnumNameValues<DayOfWeek>());
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual async Task<IActionResult> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var _ReferreralCode = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            string _InviterUser = null;
            userDto.ReferreralInviter = (userDto.ReferreralInviter == null) ? null : userDto.ReferreralInviter.Trim();
            if (userDto.ReferreralInviter != null && userDto.ReferreralInviter != "" && userDto.ReferreralInviter != "NULL")
            {
                bool _CheckInviter = await _mediator.Send(new GetReferreralCodeQuery { code = userDto.ReferreralInviter });

                if (_CheckInviter)
                {
                    _InviterUser = userDto.ReferreralInviter.ToUpper();
                }
                else
                {
                    return Ok("Invalid Referreral");
                }
            }

            var user = new User
            {
                Email = userDto.Email.ToLower(),
                PhoneNumber = userDto.PhoneNumber,
                CountryId = userDto.CountryId,
                Language = userDto.LanguageId,
                ReferreralInviter = _InviterUser,
                RegisterDate = DateTime.Now,
                StartOfWeek = userDto.StartOfWeek,
                ReferreralCode = _ReferreralCode.ToUpper(),
                UserName = !string.IsNullOrEmpty(userDto.Email) == true ? userDto.Email.ToLower() : userDto.PhoneNumber
            };

            bool _checkUser = await userRepository.TableNoTracking.Where(a => a.UserName.ToLower() == user.UserName).AnyAsync(cancellationToken);

            if (_checkUser)
            {
                return Ok("Pre-User Registration");
            }

            var result = await userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                var addRole = await userManager.AddToRoleAsync(user, "Customer");

                string _code = GenerateCode.Number(5);

                UsernameConfirm _confirm = new UsernameConfirm()
                {
                    Code = _code,
                    Username = user.UserName,
                    ExpireTime = DateTime.Now.AddSeconds(120)
                };

                if (UsernameValidate.IsPhone(user.UserName))
                {
                    //await smsIdentity.VerificationCodeAsync(user.UserName, _code);
                    await _smsService.SendSmsNewVersionSmsIr(user.UserName, _code);
                }
                else
                {
                    await _emailSender.SenderAsync(_code, "O2FIT", user.UserName, LanguageName);
                }

                await repositoryRedis.UpdateAsync($"ConfirmCode_{user.UserName}", _confirm);

                return Ok(_code);
            }

            return Ok($"Faild Create");
        }

        [Route("[action]")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto _loginDto, CancellationToken cancellationToken)
        {
            LoginSelectDto _value = new LoginSelectDto();

            if (ModelState.IsValid)
            {
                User _user = await userRepository.TableNoTracking.SingleOrDefaultAsync(a => a.UserName.ToLower() == _loginDto.UserName.ToLower(), cancellationToken);

                if (_user == null)
                {
                    _value.Value = 0;
                    _value.Name = "WrongUsername";
                    _value.Description = "Wrong Username";
                    return Ok(_value);
                }

                var result = await userManager.CheckPasswordAsync(_user, _loginDto.Password);

                if (result)
                {
                    var resultLogin = await signInManager.PasswordSignInAsync(_user, _loginDto.Password, false, lockoutOnFailure: true);

                    if (resultLogin.Succeeded)
                    {
                        if (_user.EmailConfirmed == false && _user.PhoneNumberConfirmed == false)
                        {
                            //UsernameConfirm usernameConfirm = await repositoryRedis.GetAsync($"ConfirmCode_{_user.UserName}");

                            string _code = GenerateCode.Number(5);

                            UsernameConfirm _confirm = new UsernameConfirm()
                            {
                                Code = _code,
                                Username = _user.UserName,
                                ExpireTime = DateTime.Now.AddSeconds(120)
                            };

                            if (UsernameValidate.IsPhone(_user.UserName))
                            {
                                //await smsIdentity.VerificationCodeAsync(_user.UserName, _code);
                                await _smsService.SendSmsNewVersionSmsIr(_user.UserName, _code);
                            }
                            else
                            {
                                await _emailSender.SenderAsync(_code, "O2FIT", _user.UserName, LanguageName);
                            }

                            await repositoryRedis.UpdateAsync($"ConfirmCode_{_user.UserName}", _confirm);

                            _value.Value = _code.ToInt();
                            _value.Name = "Active";
                            _value.Description = "Active User";

                            return Ok(_value);
                        }

                        UserSelectDto userSelectDto = new UserSelectDto()
                        {
                            Id = _user.Id,
                            Username = _user.UserName,
                            FirstName = _user.FirstName,
                            LastName = _user.LastName,
                            Language = _user.Language,
                            CountryId = _user.CountryId,
                            EmailConfirmed = _user.EmailConfirmed,
                            PhoneNumberConfirmed = _user.PhoneNumberConfirmed,
                            AccessFailedCount = _user.AccessFailedCount,
                            LockoutEnabled = _user.LockoutEnabled,
                            ImageUri = _user.ImageUri,
                            ReferreralCode = _user.ReferreralCode.ToUpper(),
                            StartOfWeek = _user.StartOfWeek,
                        };

                        return Ok(userSelectDto);
                    }

                    if (resultLogin.IsLockedOut)
                    {
                        _value.Value = 3;
                        _value.Name = "IsLockedOut";
                        _value.Description = "User Lockout";
                    }
                }
                else
                {
                    _value.Value = 4;
                    _value.Name = "WrongPassword";
                    _value.Description = "Wrong Password";
                }

            }

            return Ok(_value);
        }

        [Route("ConfirmCode")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmCode(ConfirmCodeDto confirmCodeDto, CancellationToken cancellationToken)
        {
            ConfirmCodeSelectDto _check = new ConfirmCodeSelectDto();

            UsernameConfirm usernameConfirm = await repositoryRedis.GetAsync($"ConfirmCode_{confirmCodeDto.Username.ToLower()}");

            if (usernameConfirm == null)
            {
                _check.IsActive = false;
                _check.WrongCode = false;
                _check.ExpireCode = true;
                return Ok(_check);
            }

            if (DateTime.Now >= usernameConfirm.ExpireTime)
            {
                _check.IsActive = false;
                _check.WrongCode = false;
                _check.ExpireCode = true;
                return Ok(_check);
            }

            if (usernameConfirm.Code == confirmCodeDto.Code)
            {
                User _user = await userRepository.TableNoTracking.SingleOrDefaultAsync(a => a.UserName == confirmCodeDto.Username.ToLower(), cancellationToken);

                if (_user == null)
                    return NotFound();

                if (UsernameValidate.IsPhone(_user.UserName))
                {
                    _user.PhoneNumberConfirmed = true;
                }
                else
                {
                    _user.EmailConfirmed = true;
                }

                _user.IsActive = true;
                //await repositoryRedis.DeleteAsync($"ConfirmCode_{confirmCodeDto.Username}");

                await userRepository.UpdateAsync(_user, cancellationToken);

                UserSelectDto userSelectDto = new UserSelectDto()
                {
                    Id = _user.Id,
                    Username = _user.UserName,
                    FirstName = _user.FirstName,
                    LastName = _user.LastName,
                    Language = _user.Language,
                    CountryId = _user.CountryId,
                    EmailConfirmed = _user.EmailConfirmed,
                    PhoneNumberConfirmed = _user.PhoneNumberConfirmed,
                    AccessFailedCount = _user.AccessFailedCount,
                    LockoutEnabled = _user.LockoutEnabled,
                    ImageUri = _user.ImageUri,
                    ReferreralCode = _user.ReferreralCode,
                    StartOfWeek = _user.StartOfWeek
                };

                return Ok(userSelectDto);
            }
            else
            {
                _check.IsActive = false;
                _check.WrongCode = true;
                _check.ExpireCode = false;
            }

            return Ok(_check);
        }

        [Route("ReSendCode")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ReSendCode(string userName)
        {
            userName = userName.ToLower();

            UsernameConfirm usernameConfirm = await repositoryRedis.GetAsync($"ConfirmCode_{userName.ToLower()}");

            string _code = GenerateCode.Number(5);

            UsernameConfirm _confirm = new UsernameConfirm()
            {
                Code = _code,
                Username = userName.ToLower(),
                ExpireTime = DateTime.Now.AddSeconds(120)
            };

            if (usernameConfirm == null || DateTime.Now >= usernameConfirm.ExpireTime)
            {
                if (UsernameValidate.IsPhone(userName))
                {
                    //await smsIdentity.VerificationCodeAsync(userName, _code);
                    await _smsService.SendSmsNewVersionSmsIr(userName, _code);
                }
                else
                {
                    await _emailSender.SenderAsync(_code, "O2FIT", userName, LanguageName);
                }

                await repositoryRedis.UpdateAsync($"ConfirmCode_{userName}", _confirm);
            }

            return Ok(_code);
        }

        [Route("ForgotPassword")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string userName, CancellationToken cancellationToken)
        {
            userName = userName.ToLower();

            User _user = await userManager.FindByNameAsync(userName.ToLower());

            if (_user == null)
            {
                return Ok(false);
            }

            UsernameConfirm _value = await repositoryRedis.GetAsync($"ConfirmCode_{userName.ToLower()}");

            string _code = GenerateCode.Number(5);

            UsernameConfirm _confirm = new UsernameConfirm()
            {
                Code = _code,
                Username = userName.ToLower(),
                ExpireTime = DateTime.Now.AddSeconds(120)
            };

            if (_value == null || DateTime.Now >= _value.ExpireTime)
            {
                if (UsernameValidate.IsPhone(userName))
                {
                    //await smsIdentity.VerificationCodeAsync(_user.UserName, _code);
                    await _smsService.SendSmsNewVersionSmsIr(_user.UserName, _code);
                }
                else
                {
                    await _emailSender.SenderAsync(_code, "O2FIT", _user.UserName, LanguageName);
                }

                await repositoryRedis.UpdateAsync($"ConfirmCode_{userName.ToLower()}", _confirm);
            }
            else
            {
                _code = _value.Code;
            }

            // For Test
            //return Ok(true);

            return Ok(_code);
        }

        [Route("ForgotPasswordConfirmCode")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<ForgotPasswordSelectDto>> ForgotPasswordConfirmCode(ConfirmCodeDto confirmCodeDto, CancellationToken cancellationToken)
        {
            ForgotPasswordSelectDto forgotPasswordSelectDto = new ForgotPasswordSelectDto();

            User _user = await userManager.FindByNameAsync(confirmCodeDto.Username.ToLower());

            if (_user == null)
                return NotFound();

            UsernameConfirm usernameConfirm = await repositoryRedis.GetAsync($"ConfirmCode_{confirmCodeDto.Username.ToLower()}");

            if (usernameConfirm == null)
            {
                forgotPasswordSelectDto.WrongCode = false;
                forgotPasswordSelectDto.ExpireCode = true;
            }
            else if (_user.UserName == usernameConfirm.Username)
            {
                if (usernameConfirm.ExpireTime > DateTime.Now)
                {
                    if (usernameConfirm.Code == confirmCodeDto.Code)
                    {
                        string _code = await signInManager.UserManager.GeneratePasswordResetTokenAsync(_user);
                        forgotPasswordSelectDto.Code = _code;
                        forgotPasswordSelectDto.WrongCode = false;
                        forgotPasswordSelectDto.ExpireCode = false;

                        await repositoryRedis.DeleteAsync($"ConfirmCode_{confirmCodeDto.Username.ToLower()}");

                        UsernameConfirm _confirm = new UsernameConfirm()
                        {
                            Code = _code,
                            Username = _user.UserName,
                            ExpireTime = DateTime.Now.AddDays(1)
                        };

                        await repositoryRedis.UpdateAsync($"ForgotPasswordConfirmCode_{confirmCodeDto.Username.ToLower()}", _confirm);
                    }
                    else
                    {
                        forgotPasswordSelectDto.WrongCode = true;
                        forgotPasswordSelectDto.ExpireCode = false;
                    }
                }
                else
                {
                    forgotPasswordSelectDto.WrongCode = false;
                    forgotPasswordSelectDto.ExpireCode = true;
                }
            }

            return Ok(forgotPasswordSelectDto);
        }

        [Route("ForgotPasswordChange")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<ForgotPasswordChangeSelectDto>> ForgotPasswordChange(ForgotPasswordDto forgotPasswordDto, CancellationToken cancellationToken)
        {
            ForgotPasswordChangeSelectDto _change = new ForgotPasswordChangeSelectDto();

            User _user = await userManager.FindByNameAsync(forgotPasswordDto.Username.ToLower());

            if (_user == null)
                return NotFound();

            UsernameConfirm usernameConfirm = await repositoryRedis.GetAsync($"ForgotPasswordConfirmCode_{forgotPasswordDto.Username.ToLower()}");

            if (usernameConfirm == null)
            {
                _change.IsChange = false;
                _change.ExpireCode = true;
            }
            else if (usernameConfirm.Username == _user.UserName)
            {
                if (usernameConfirm.ExpireTime > DateTime.Now)
                {
                    if (usernameConfirm.Code == forgotPasswordDto.Code)
                    {
                        var result = await userManager.ResetPasswordAsync(_user, usernameConfirm.Code, forgotPasswordDto.NewPassword);

                        if (result.Succeeded)
                        {
                            _change.IsChange = true;
                            _change.ExpireCode = false;

                            await repositoryRedis.DeleteAsync($"ForgotPasswordConfirmCode_{forgotPasswordDto.Username.ToLower()}");
                        }
                    }
                    else
                    {
                        _change.IsChange = false;
                        _change.ExpireCode = true;
                    }
                }
                else
                {
                    _change.IsChange = false;
                    _change.ExpireCode = true;
                }
            }

            return Ok(_change);
        }

        [HttpPost("ResetPasswordToken")]
        public async Task<ApiResult<string>> ResetPasswordToken(string UserName)
        {
            User _user = await userManager.FindByNameAsync(UserName.ToLower());

            if (_user == null)
                return NotFound();

            string _code = await signInManager.UserManager.GeneratePasswordResetTokenAsync(_user);

            return Ok(_code);
        }

        [HttpPost("ResetPassword")]
        public async Task<ApiResult> ResetPassword([FromForm] ResetPasswordDto input)
        {
            User _user = await userManager.FindByNameAsync(input.Username.ToLower());

            if (_user == null)
                return NotFound();

            var result = await userManager.ResetPasswordAsync(_user, input.Code, input.NewPassword);

            if (result.Succeeded)
            {
                return new ApiResult(true, ApiResultStatusCode.Success, "تغییر رمز عبور با موفقیت انجام شد.");
            }
            else
            {
                return new ApiResult(false, ApiResultStatusCode.Success, "خطا در تغییر رمز عبور.");
            }
        }

        [HttpPost("CheckReferreralInviter")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckReferreralInviter(int userId, bool previousPurchase)
        {
            bool referall = await _mediator.Send(new GetReferreralInviterQuery { UserId = userId, PreviousPurchase = previousPurchase });
            return Ok(referall);
        }

        [HttpPost("ApproveReferreral")]
        [AllowAnonymous]
        public async Task<IActionResult> ApproveReferreral(int userId)
        {
            await _mediator.Send(new UpdateReferreralDiscountCommand { userId = userId });
            return Ok();
        }

        [HttpPost("AddReferreralCount")]
        [AllowAnonymous]
        public async Task<IActionResult> AddReferreralCount(int userId)
        {
            await _mediator.Send(new UpdateReferreralCountCommand { UserId = userId });
            return Ok();
        }

        [HttpGet("SendAdminLoginCode")]
        [AllowAnonymous]
        public async Task<IActionResult> SendAdminLoginCode(string userName)
        {
            string _code = GenerateCode.Number(5);

            UsernameConfirm _confirm = new UsernameConfirm()
            {
                Code = _code,
                Username = userName,
                ExpireTime = DateTime.Now.AddSeconds(120)
            };

            if (UsernameValidate.IsPhone(userName))
            {
                //await smsIdentity.VerificationCodeAsync(userName, _code);
                await _smsService.SendSmsNewVersionSmsIr(userName, _code);
            }
            else
            {
                await _emailSender.SenderAsync(_code, "O2FIT", userName, LanguageName);
            }

            await repositoryRedis.UpdateAsync($"ConfirmCode_{userName}", _confirm);

            return Ok();
        }

        [Route("CheckAdminLoginCode")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmCode(string userName, string code, CancellationToken cancellationToken)
        {
            ConfirmCodeSelectDto _check = new ConfirmCodeSelectDto();

            UsernameConfirm usernameConfirm = await repositoryRedis.GetAsync($"ConfirmCode_{userName.ToLower()}");

            if (usernameConfirm == null)
            {
                _check.IsActive = false;
                _check.WrongCode = true;
                _check.ExpireCode = true;
                return Ok(_check);
            }

            if (DateTime.Now >= usernameConfirm.ExpireTime)
            {
                _check.IsActive = false;
                _check.WrongCode = false;
                _check.ExpireCode = true;
                return Ok(_check);
            }

            if (usernameConfirm.Code == code)
            {
                var _user = await userManager.FindByNameAsync(userName);
                if (_user == null)
                    return NotFound();

                _check.IsActive = true;
                _check.WrongCode = false;
                _check.ExpireCode = false;
            }
            else
            {
                _check.IsActive = false;
                _check.WrongCode = true;
                _check.ExpireCode = false;
            }
            return Ok(_check);
        }


        // [Route("CreateRole")]
        // [HttpPost]
        // [Authorize(Roles = "Admin")]
        // public virtual async Task<ApiResult> CreateRole(string name, string description)
        // {
        //     await roleManager.CreateAsync(new Role
        //     {
        //        Name = "Nutritionist",
        //        Description = "nutritionist role"
        //     });
        //
        //
        //     return null;
        // }

        //[HttpPut]
        //public virtual async Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
        //{
        //    var updateUser = await userRepository.GetByIdAsync(cancellationToken, id);

        //    updateUser.UserName = user.UserName;
        //    updateUser.PasswordHash = user.PasswordHash;
        //    updateUser.IsActive = user.IsActive;

        //    await userRepository.UpdateAsync(updateUser, cancellationToken);

        //    return Ok();
        //}

    }
}
