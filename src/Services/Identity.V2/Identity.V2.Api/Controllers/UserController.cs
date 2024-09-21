using Common.Constants.gRPCAddresses;
using EventBus.Messages.Events.Services.Wallet;
using Grpc.Net.Client;
using Identity.V2.Application.Countries.V1.Queries.GetCountryIdByOldSystemCountryId;
using Identity.V2.Application.Countries.V1.Queries.GetOldSystemCountryIdByCountryId;
using Identity.V2.Application.Dtos.UserTrackSpecification;
using Identity.V2.Application.NutritionistProfiles.V1.Queries.GetNutritionistProfileById;
using Identity.V2.Application.Users.V1.Commands.ChangeIsCompleteValue;
using Identity.V2.Application.Users.V1.Commands.ChangeUserStatusByUserId;
using Identity.V2.Application.Users.V1.Commands.IncreaseReferralCountBuy;
using Identity.V2.Application.Users.V1.Commands.UpdateStateIdAndCityId;
using Identity.V2.Application.Users.V1.Queries.CheckReferralCodeAndUserIdIsValid;
using Identity.V2.Application.Users.V1.Queries.GetUserInfoById;
using Identity.V2.Domain.Enums;
using Identity.V2.Domain.ValueObjects;
using Track.Grpc;

namespace Identity.V2.Api.Controllers;

[ApiVersion("1")]
public class UserController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ISmsService _smsService;
    private readonly IEmailSender _emailSender;
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ICurrentUserService _currentUserService;

    public UserController(IMediator mediator, ISmsService smsService, IEmailSender emailSender,
        IUserService userService, IConfiguration configuration,
        IPublishEndpoint publishEndpoint, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _smsService = smsService;
        _emailSender = emailSender;
        _userService = userService;
        _configuration = configuration;
        _publishEndpoint = publishEndpoint;
        _currentUserService = currentUserService;
    }

    // [AllowAnonymous]
    // [HttpGet("is-user-exists")]
    // public async Task<ApiResult<UserDto?>> IsUserExists(string username,string pass)
    // {
    //     var user = await _userService.GetUserByUsernameAsync(username);
    //     if (user == null)
    //         return new ApiResult<UserDto?>(null, string.Empty, ApiResultStatusCode.NotFound, false);
    //
    //     var userDto = user.ToDto<UserDto>();
    //     return new ApiResult<UserDto?>(userDto, string.Empty, ApiResultStatusCode.Success);
    // }


    [AllowAnonymous]
    [HttpGet("get-utc-time")]
    public ActionResult GetUtcTime()
    {
        var date = DateTime.UtcNow;
        return Ok(new ApiResult<string>(date.Year + "-" + date.Month + "-" + date.Day, string.Empty,
            ApiResultStatusCode.Success));
    }

   
    //[Cached(0.1)]
    [HttpGet("get-user-data")]
    [HasPermission(PermissionsConstants.GetUserData)]
    public async Task<ActionResult> GetUserData(CancellationToken cancellationToken)
    {
        var userInfo = await _mediator.Send(new GetUserInfoByIdQuery(_currentUserService.UserId!), cancellationToken).ConfigureAwait(false);
        userInfo.User.CountryId = await _mediator
            .Send(new GetOldSystemCountryIdByCountryIdQuery(userInfo.User.Country), cancellationToken)
            .ConfigureAwait(false);

        #region gRPC Configuration

        // if (_hostEnvironment.IsDevelopment())
        // {
        //     using var channel = GrpcChannel.ForAddress(gRPCLocalAddresses.TrackgRPCAddress);
        //     var client = new UserTracks.UserTracksClient(channel);
        //     var request = new UserTrackSpecificationRequest
        //     {
        //         Id = userId
        //     };
        //     var result = await client.GetUserTrackSpecificationAsync(request);
        //     foreach (var userTrack in result.Tracks)
        //     {
        //         var track = new UserTrackSpecificationDto
        //         {
        //             Id = userTrack.Id,
        //             Arm = userTrack.Arm,
        //             UserId = userTrack.UserId,
        //             Chest = userTrack.Chest,
        //             Hip = userTrack.Hip,
        //             DateTime = userTrack.DateTime.ToDateTime(),
        //             Image = userTrack.Image,
        //             Neck = userTrack.Neck,
        //             Note = userTrack.Note,
        //             Shoulder = userTrack.Shoulder,
        //             Thigh = userTrack.Thigh,
        //             Waist = userTrack.Waist,
        //             Weight = userTrack.Weight,
        //             Wrist = userTrack.Wrist,
        //             AppId = userTrack.AppId,
        //             HighHip = userTrack.HighHip
        //         };
        //         userInfo.UserTrackSpecification.Add(track);
        //     }
        //
        //     return Ok(new ApiResult<UserInfoDto>(userInfo, string.Empty, ApiResultStatusCode.Success));
        // }
        

        #endregion
        
        
        return Ok(new ApiResult<UserInfoResponseDto>(userInfo, string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.GetProfileNutritionist)]
    [HttpGet("get-profile-nutritionist")]
    public async Task<ActionResult> GetProfileNutritionist(CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var result = await _mediator.Send(new GetNutritionistProfileByIdQuery(userId!), cancellationToken);
        return Ok(new ApiResult<NutritionistDataDto>(result, string.Empty, ApiResultStatusCode.Success));
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserDto dto, CancellationToken cancellationToken)
    {
        if (!ValidateUsername.IsEmail(dto.Username) && !ValidateUsername.IsPhone(dto.Username))
        {
            return BadRequest(new ApiResult(string.Empty, ApiResultStatusCode.BadRequest, false));
        }

        string phone = "", email = "";
        if (ValidateUsername.IsPhone(dto.Username))
        {
            phone = dto.Username;
        }

        if (ValidateUsername.IsEmail(dto.Username))
        {
            email = dto.Username;
        }

        if (!string.IsNullOrEmpty(dto.ReferralInviter))
        {
            dto.ReferralInviter = dto.ReferralInviter.Trim();
            bool checkInviter =
                await _mediator.Send(new IsUserReferralCodeValidQuery(dto.ReferralInviter),
                    cancellationToken).ConfigureAwait(false);

            dto.ReferralInviter = checkInviter ? dto.ReferralInviter.ToUpper() : null;
        }

        if (!string.IsNullOrEmpty(dto.Username))
        {
            var userExists = await _userService.GetUserByUsernameAsync(dto.Username);
            if (userExists == null)
            {
                var country = await _mediator.Send(new GetCountryByOldSystemCountryIdQuery(dto.CountryId),
                    cancellationToken).ConfigureAwait(false);

                string code = GenerateCode.Number(5);

                while (await _mediator.Send(new CheckConfirmCodeIsNotDuplicateQuery(code), cancellationToken)
                           .ConfigureAwait(false))
                {
                    code = GenerateCode.Number(5);
                }

                string referralCode = GenerateCode.Generate();

                while (await _mediator.Send(new CheckReferralCodeDuplicateQuery(referralCode), cancellationToken)
                           .ConfigureAwait(false))
                {
                    referralCode = GenerateCode.Generate();
                }

                if (!string.IsNullOrEmpty(dto.ReferralInviter))
                {
                    await _mediator.Send(new IncreaseReferralCountBuyCommand(dto.ReferralInviter), cancellationToken);
                }
                var user = new User
                {
                    Email = email.ToLower(),
                    PhoneNumber = phone,
                    CountryId = ObjectId.Parse(country.Id),
                    Language = dto.Language,
                    ReferralInviter = dto.ReferralInviter,
                    RegisterDate = DateTime.Now,
                    StartOfWeek = dto.StartOfWeek,
                    ReferralCode = referralCode,
                    UserName = dto.Username,
                    ConfirmCode = code,
                    ConfirmCodeExpireTime = DateTime.UtcNow.AddMinutes(2),
                    AutoGeneratedPassword = EncryptionAndDecryptionUtility.Encrypt(Guid.NewGuid().ToString(),
                        _configuration["EncryptionKey"]!)
                };
                user.Status = dto.IsNutritionist ? UserStatus.AwaitingConfirmation : UserStatus.NormalUser; 
                IdentityResult result;
                if (!string.IsNullOrEmpty(dto.Password))
                {
                    result = await _userService.CreateUserWithPasswordAsync(user, dto.Password)
                        .ConfigureAwait(false);
                }
                else
                {
                    result = await _userService.CreateUserWithoutPasswordAsync(user).ConfigureAwait(false);
                }


                if (result.Succeeded)
                {
                    #region Raise event

                    await _publishEndpoint.Publish<CreatedWalletForUser>(new
                    {
                        UserId = user.Id,
                        CurrencyName = country.CurrencyName,
                        OldCountryId = country.CountryId
                    }, cancellationToken);

                    #endregion

                    // var assignToRoleResult = await _userService.AssignRoleToUserAsync(user,
                    //     DefaultRolesConstants.Customer).ConfigureAwait(false);

                    //if (assignToRoleResult.Succeeded)
                    //{

                    var assignToRoleResult = await _userService.AssignRoleToUserAsync(user,
                        DefaultRolesConstants.Customer).ConfigureAwait(false);

                    if (dto.IsNutritionist)
                        await _userService.AssignRoleToUserAsync(user, DefaultRolesConstants.Nutritionist)
                            .ConfigureAwait(false);
                    
                    if (assignToRoleResult.Succeeded)
                    {
                        if (ValidateUsername.IsPhone(user.UserName))
                        {
                            await _smsService.SendSmsNewVersionSmsIrAsync(user.UserName, code).ConfigureAwait(false);
                        }
                        else
                        {
                            await _emailSender.SenderAsync(code, "O2FIT", user.UserName, LanguageName)
                                .ConfigureAwait(false);
                        }

                        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
                    }

                    return BadRequest(new ApiResult<string>(string.Empty,
                        string.Join(", ", result.Errors.Select(s => s.Description)),
                        ApiResultStatusCode.BadRequest, false));

                    //}
                    //
                    //return new ApiResult(string.Join(", ", assignToRoleResult.Errors.Select(s => s.Description)),
                    //    ApiResultStatusCode.ServerError, false);
                }

                return BadRequest(new ApiResult(string.Join(", ", result.Errors.Select(s => s.Description)),
                    ApiResultStatusCode.BadRequest, false));
            }
            else
            {
                var code = GenerateCode.Number(5);
                while (await _mediator.Send(new CheckConfirmCodeIsNotDuplicateQuery(code), cancellationToken)
                           .ConfigureAwait(false))
                {
                    code = GenerateCode.Number(5);
                }

                userExists.ConfirmCode = code;
                userExists.ConfirmCodeExpireTime = DateTime.UtcNow.AddMinutes(2);
                var test = await _userService.UpdateUserAsync(userExists);

                if (ValidateUsername.IsPhone(userExists.UserName))
                {
                    await _smsService.SendSmsNewVersionSmsIrAsync(userExists.UserName, code).ConfigureAwait(false);
                }
                else
                {
                    await _emailSender.SenderAsync(code, "O2FIT", userExists.UserName, LanguageName)
                        .ConfigureAwait(false);
                }

                return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
            }
        }

        return BadRequest(new ApiResult(string.Empty, ApiResultStatusCode.BadRequest, false));
    }

    //[EnableRateLimiting(O2fitIdentityConstants.RateLimitPolicyName)]
    [AllowAnonymous]
    [HttpPost("confirm-code")]
    public async Task<ActionResult> ConfirmCode([FromBody] ConfirmCodeDto dto)
    {
        var isIranian = false;
        var user = await _userService.GetUserByUsernameAsync(dto.Username);
        if (user != null)
        {
            if (user.ConfirmCode == dto.Code && user.ConfirmCodeExpireTime > DateTime.UtcNow)
            {
                user.IsActive = true;

                if (ValidateUsername.IsPhone(user.UserName))
                {
                    user.PhoneNumberConfirmed = true;
                    isIranian = true;
                }
                else
                {
                    user.EmailConfirmed = true;
                }

                var result = await _userService.UpdateUserAsync(user);
                if (result.Succeeded)
                {
                    if (isIranian)
                    {
                        var decryptedAutoGeneratedPassword = EncryptionAndDecryptionUtility.Decrypt(
                            user.AutoGeneratedPassword,
                            _configuration["EncryptionKey"]!);
                        var userData = new UserDataConfirmCodeDto
                        {
                            UserId = user.Id.ToString(),
                            Password = decryptedAutoGeneratedPassword
                        };
                        return Ok(new ApiResult<UserDataConfirmCodeDto>(userData, string.Empty,
                            ApiResultStatusCode.Success));
                    }

                    var userDataNotIran = new UserDataConfirmCodeDto
                    {
                        UserId = user.Id.ToString()
                    };
                    return Ok(new ApiResult<UserDataConfirmCodeDto>(userDataNotIran, string.Empty, ApiResultStatusCode.Success));
                }

                return BadRequest(new ApiResult<string>(string.Empty,
                    string.Join(", ", result.Errors.Select(s => s.Description)),
                    ApiResultStatusCode.BadRequest, false));
            }

            return BadRequest(new ApiResult<string>(string.Empty, "Invalid confirm code",
                ApiResultStatusCode.BadRequest, false));
        }

        return NotFound(new ApiResult<string>(string.Empty, "User not found", ApiResultStatusCode.NotFound, false));
    }

    //[EnableRateLimiting(O2fitIdentityConstants.RateLimitPolicyName)]
    [AllowAnonymous]
    [HttpPost("resend-code")]
    public async Task<ActionResult> ResendCode(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        if (user != null)
        {
            user.ConfirmCode = GenerateCode.Number(5);
            user.ConfirmCodeExpireTime = DateTime.UtcNow.AddMinutes(2);
            var identityResult = await _userService.UpdateUserAsync(user);
            if (identityResult.Succeeded)
            {
                if (ValidateUsername.IsPhone(user.UserName))
                {
                    await _smsService.SendSmsNewVersionSmsIrAsync(user.UserName, user.ConfirmCode)
                        .ConfigureAwait(false);
                }
                else
                {
                    await _emailSender.SenderAsync(user.ConfirmCode, "O2FIT", user.UserName, LanguageName)
                        .ConfigureAwait(false);
                }

                return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
            }

            return BadRequest(new ApiResult(string.Join(", ", identityResult.Errors.Select(s => s.Description)),
                ApiResultStatusCode.BadRequest, false));
        }

        return NotFound(new ApiResult("User not found", ApiResultStatusCode.NotFound, false));
    }

    [HasPermission(PermissionsConstants.AssignRoleToUser)]
    [HttpPost("assign-role-to-user")]
    public async Task<ActionResult> AssignRoleToUser([FromBody] AssignRoleToUserDto dto)
    {
        var result = await _userService.AssignRoleToUserAsync(dto).ConfigureAwait(false);
        if (result.Succeeded)
        {
            return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        }

        return BadRequest(new ApiResult(string.Join(", ", result.Errors.Select(s => s.Description)),
            ApiResultStatusCode.BadRequest, false));
    }

    [HasPermission(PermissionsConstants.AddPasswordForUser)]
    [HttpPost("add-password-for-user")]
    public async Task<ActionResult> AddPasswordForUser([FromBody] AddPasswordForUserDto dto)
    {
        var result = await _userService.AddPasswordForUserAsync(dto.Username, dto.Password);
        if (result.Succeeded)
        {
            return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        }

        return BadRequest(new ApiResult(string.Join(", ", result.Errors.Select(s => s.Description)),
            ApiResultStatusCode.BadRequest, false));
    }

    [HasPermission(PermissionsConstants.UpdateSecurityStampByUsername)]
    [HttpPost("update-security-stamp-by-username")]
    public async Task<ActionResult> UpdateSecurityStampByUsername(string username)
    {
        var result = await _userService.UpdateSecurityStampByUsernameAsync(username);
        if (result.Succeeded)
        {
            return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        }

        return BadRequest(new ApiResult(string.Join(", ", result.Errors.Select(s => s.Description)),
            ApiResultStatusCode.BadRequest, false));
    }

    [HasPermission(PermissionsConstants.UpdateSecurityStampByUserId)]
    [HttpPost("update-security-stamp-by-user-id")]
    public async Task<ActionResult> UpdateSecurityStampByUserId(string id)
    {
        var result = await _userService.UpdateSecurityStampByUserIdAsync(id);
        if (result.Succeeded)
        {
            return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        }

        return BadRequest(new ApiResult(string.Join(", ", result.Errors.Select(s => s.Description)),
            ApiResultStatusCode.BadRequest, false));
    }

    // [AllowAnonymous]
    // [HttpPost("update-lockout-date")]
    // public async Task<ApiResult> UpdateLockoutDate(string userId, DateTimeOffset dateTimeOffset)
    // {
    //     var user = await _userService.GetUserByIdAsync(userId);
    //     if (user != null)
    //     {
    //         
    //         var result = await _userService.ChangeLockoutDateAsync(user, DateTimeOffset.UtcNow);
    //         if (result.Succeeded)
    //         {
    //             return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    //         }
    //         return new ApiResult(string.Join(", ", result.Errors.Select(s => s.Description)),
    //             ApiResultStatusCode.ServerError, false);
    //     }
    //
    //     return new ApiResult(string.Empty, ApiResultStatusCode.NotFound, false);
    // }

    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<ActionResult> ForgotPassword(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        if (user == null)
        {
            return NotFound(new ApiResult(string.Empty, ApiResultStatusCode.NotFound, false));
        }

        if (user.IsActive)
        {
            user.ConfirmCode = GenerateCode.Number(5);
            user.ConfirmCodeExpireTime = DateTime.UtcNow.AddMinutes(2);
            var identityResult = await _userService.UpdateUserAsync(user);
            if (identityResult.Succeeded)
            {
                await _emailSender.SenderAsync(user.ConfirmCode, "O2FIT", user.UserName, LanguageName)
                    .ConfigureAwait(false);
                return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
            }

            return BadRequest(new ApiResult(string.Empty, ApiResultStatusCode.BadRequest));
        }

        return BadRequest(new ApiResult(string.Empty, ApiResultStatusCode.BadRequest));
    }

    [AllowAnonymous]
    [HttpPost("reset-password-token")]
    public async Task<ActionResult> ResetPasswordToken(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        if (user == null)
            return NotFound(new ApiResult<string>(string.Empty, string.Empty, ApiResultStatusCode.NotFound, false));
        var code = await _userService.GeneratePasswordResetTokenAsync(user);

        return Ok(new ApiResult<string>(code, string.Empty, ApiResultStatusCode.Success));
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        var user = await _userService.GetUserByUsernameAsync(dto.Username);

        if (user == null)
            return NotFound(new ApiResult(string.Empty, ApiResultStatusCode.NotFound, false));

        if (user.ConfirmCodeExpireTime < DateTime.UtcNow)
        {
            var result = await _userService.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
            }

            return BadRequest(new ApiResult(string.Join(", ", result.Errors.Select(s => s.Description)),
                ApiResultStatusCode.BadRequest, false));
        }

        return BadRequest(new ApiResult("token expire", ApiResultStatusCode.BadRequest, false));
    }

    [AllowAnonymous]
    [HttpPost("generate-forgot-password-code")]
    public async Task<ActionResult> GenerateForgotPasswordCode([FromBody] ConfirmCodeDto dto)
    {
        var user = await _userService.GetUserByUsernameAsync(dto.Username);
        if (user == null)
            return NotFound(new ApiResult<string>(string.Empty, string.Empty, ApiResultStatusCode.NotFound, false));

        if (user.ConfirmCodeExpireTime > DateTime.UtcNow)
        {
            var code = await _userService.GeneratePasswordResetTokenAsync(user);
            user.ConfirmCodeExpireTime = DateTime.UtcNow.AddMinutes(2);
            await _userService.UpdateUserAsync(user);
            return Ok(new ApiResult<string>(code, string.Empty, ApiResultStatusCode.Success));
        }

        return BadRequest(new ApiResult<string>(string.Empty, "You have code", ApiResultStatusCode.BadRequest, false));
    }

    [AllowAnonymous]
    [HttpPost("forgot-password-change")]
    public async Task<ActionResult> ForgotPasswordChange([FromBody] ForgotPasswordDto dto)
    {
        var user = await _userService.GetUserByUsernameAsync(dto.Username);
        if (user == null)
            return NotFound(new ApiResult(string.Empty, ApiResultStatusCode.NotFound, false));

        if (user.ConfirmCodeExpireTime > DateTime.UtcNow)
        {
            if (user.ConfirmCode == dto.Token)
            {
                var result = await _userService.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
                }
            }

            return BadRequest(new ApiResult("Invalid code", ApiResultStatusCode.BadRequest, false));
        }

        return BadRequest(new ApiResult("Code is expired", ApiResultStatusCode.BadRequest, false));
    }

    [HasPermission(PermissionsConstants.AssignReferralCode)]
    [HttpPost("assign-referral-code")]
    public async Task<ActionResult> AssignReferralCode([FromBody] CheckReferralCodeAndUserIdIsValidQuery query,
        CancellationToken cancellationToken)
    {
        var isExists = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);

        if (!isExists)
            return NotFound(new ApiResult(string.Empty, ApiResultStatusCode.NotFound, false));

        var user = await _userService.GetUserByIdAsync(query.UserId);
        if (user == null)
        {
            return NotFound(new ApiResult(string.Empty, ApiResultStatusCode.NotFound, false));
        }

        user.ReferralInviter = query.Code;
        await _userService.UpdateUserAsync(user);

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.UpdateStateIdAndCityId)]
    [HttpPatch("update-state-id-and-city-id")]
    public async Task<ActionResult> UpdateStateIdAndCityId([FromBody] UpdateStateIdAndCityIdCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.ChangeUserStatusByUserId)]
    [HttpPatch("change-user-status-by-user-id")]
    public async Task<ActionResult> ChangeUserStatusByUserId([FromBody] ChangeUserStatusByUserIdCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    // [HttpPost("forgot-password-confirm-code")]
    // [AllowAnonymous]
    // public async Task<ApiResult<string>> ForgotPasswordConfirmCode(
    //     [FromBody] CheckReferralCodeAndUserIdIsValidQuery query,
    //     CancellationToken cancellationToken)
    // {
    //     var isExists = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
    //     if (!isExists)
    //         return new(string.Empty, "User with code not found", ApiResultStatusCode.NotFound, false);
    //
    //     var user = await _userService.GetUserByIdAsync(query.UserId);
    //     if (user == null)
    //     {
    //         return new(string.Empty, "User not found", ApiResultStatusCode.NotFound, false);
    //     }
    //
    //     if (user.ConfirmCodeExpireTime > DateTime.UtcNow)
    //     {
    //         
    //     }
    // }
}