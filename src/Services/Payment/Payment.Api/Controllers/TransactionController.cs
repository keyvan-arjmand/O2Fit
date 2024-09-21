using EventBus.Messages.Contracts.Services.Track.Specification;
using MediatR;
using Payment.Application.Transactions.V1.Command.CreateTransaction;
using Payment.Application.Transactions.V1.Command.CreateTransactionWallet;
using Payment.Application.Transactions.V1.Command.UpdateTransactionCafeBazar;
using Payment.Application.Transactions.V1.Command.UpdateTransactionMellat;
using Payment.Application.Transactions.V1.Command.UpdateTransactionMyket;
using Payment.Application.Transactions.V1.Command.UpdateTransactionSaman;

namespace Payment.Api.Controllers
{
    [ApiVersion("1")]
    public class TransactionController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public TransactionController(IMapper mapper, IMediator mediator, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HasPermission(PermissionsConstants.CreateTransaction)]
        [HttpPost("create-transaction-payment")]
        public async Task<long> CreateTransaction(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            request.UserId = _currentUserService.UserId;
            request.CountryId = int.Parse(_currentUserService.OldSystemCountryId!);
            return await _mediator.Send(request, cancellationToken);
        }

        [HasPermission(PermissionsConstants.CreateTransactionWallet)]
        [HttpPost("create-transaction-wallet")]
        public async Task<long> PostWallet(CreateTransactionWalletCommand request, CancellationToken cancellationToken)
        {
            request.UserId = _currentUserService.UserId;
            return await _mediator.Send(request, cancellationToken);
        }

        [HasPermission(PermissionsConstants.UpdateTransactionCafeBazar)]
        [HttpPatch("cafe-bazar")]
        public async Task<IActionResult> PatchCafeBazar(UpdateTransactionCafeBazarCommand request,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HasPermission(PermissionsConstants.UpdateTransactionMyket)]
        [HttpPatch("Myket")]
        public async Task<IActionResult> PatchPut(UpdateTransactionMyketCommand request,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HasPermission(PermissionsConstants.UpdateTransactionMyket)]
        [HttpPatch("mellat-test")]
        public async Task<IActionResult> PatchMellat(UpdateTransactionMellatCommand request,
            CancellationToken cancellationToken)
        {
            request.UserName = _currentUserService.Username;
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HasPermission(PermissionsConstants.UpdateTransactionMyket)]
        [HttpPatch("saman-test")]
        public async Task<IActionResult> PatchSaman(UpdateTransactionSamanCommand request,
            CancellationToken cancellationToken)
        {
            request.UserName = _currentUserService.Username;
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }
    }
}