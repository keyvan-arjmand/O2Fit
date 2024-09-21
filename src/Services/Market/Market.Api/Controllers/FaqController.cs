using Market.Application.Dtos;
using Market.Application.Faqs.V1.Commands.InsertFaq;
using Market.Application.Faqs.V1.Commands.SoftDeleteFaq;
using Market.Application.Faqs.V1.Commands.UpdateFaq;
using Market.Application.Faqs.V1.Commands.UpdateUsefulFaq;
using Market.Application.Faqs.V1.Queries.GetAllFaq;
using Market.Application.Faqs.V1.Queries.GetByIdFaq;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers;

[ApiVersion("1")]
public class FaqController : BaseApiController
{
    private readonly IMediator _mediator;

    public FaqController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get-all-faq")]
    public async Task<ActionResult<List<FaqDto>>> GetAll(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllFaqQuery(), cancellationToken);
    }

    [HttpGet("get-by-id-faq")]
    public async Task<ActionResult<FaqDto>> GetAppVersion(string id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetByIdFaqQuery(id), cancellationToken);
    }

    [HttpPost("insert-faq")]
    public async Task<ActionResult> Post(InsertFaqCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpPatch("update-useful-faq")]
    public async Task<ActionResult> UpdateUsefulFaq(UpdateUsefulFaqCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }
    [HttpPut("update-faq")]
    public async Task<ActionResult> Update(UpdateFaqCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpDelete("soft-delete-faq")]
    public async Task<ActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteFaqCommand(id), cancellationToken);
        return Ok();
    }
}