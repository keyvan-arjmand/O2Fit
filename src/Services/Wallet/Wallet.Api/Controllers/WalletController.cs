using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Dtos;
using Wallet.Application.Transactions.V1.Commands.PaymentWithWallet;
using Wallet.Application.Wallets.V1.Command.CreateWallet;
using Wallet.Application.Wallets.V1.Command.SubtractWallet;
using Wallet.Application.Wallets.V1.Query.GetWalletById;
using Wallet.Application.Wallets.V1.Query.GetWalletByUserId;

namespace Wallet.Api.Controllers;
[ApiVersion("1")]
public class WalletController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public WalletController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }
    [HttpPost("create-wallet")]
    [HasPermission(PermissionsConstants.CreateWallet)]
    public async Task<ApiResult<string>> CreateWallet(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return new ApiResult<string>(result, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
    }
    [HttpGet("get-wallet-by-id")]
    [HasPermission(PermissionsConstants.GetWalletById)]
    public async Task<ApiResult<WalletDto>> GetWalletById(GetWalletByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return new ApiResult<WalletDto>(result, string.Empty, ApiResultStatusCode.Success, true);
    }

    [HttpPost("payment-wallet")]
    [HasPermission(PermissionsConstants.PaymentWithWallet)]
    public async Task<ApiResult<bool>> PaymentWithWallet(PaymentWithWalletCommand request, CancellationToken cancellationToken)
    {
        request.CountryId = Convert.ToInt32(_currentUserService.OldSystemCountryId);
        request.UserId = _currentUserService.UserId;
        request.UserName = _currentUserService.Username;
        var trId = await _mediator.Send(request, cancellationToken);
        return new ApiResult<bool>(true, "عملیات با موفقیت انجام شد", ApiResultStatusCode.Success, true);
    }
}