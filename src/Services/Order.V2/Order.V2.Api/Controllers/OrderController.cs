using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.V2.Application.Dtos;
using Order.V2.Application.Orders.V1.Command.InsertOrder;
using Order.V2.Application.Orders.V1.Command.UpdateOrder;
using Order.V2.Application.Orders.V1.Query.GetAllOrder;
using Order.V2.Application.Orders.V1.Query.GetById;

namespace Order.V2.Api.Controllers
{
    [ApiVersion("1")]
    public class OrderController : BaseApiController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HasPermission(PermissionsConstants.PostUserTrackSpecification)]
        [HttpPut]
        public async Task<ActionResult> UpdateOrder(UpdateOrderCommand req)
        {
            await _mediator.Send(req);
            return Ok();
        }
        [HasPermission(PermissionsConstants.PostUserTrackSpecification)]
        [HttpPost("insert-fake-order")]
        public async Task<ActionResult> Post(InsertOrderCommand req)
        {
            await _mediator.Send(req);
            return Ok();
        }
        //[HttpGet("get-all-order")]
        //public async Task<ApiResult<List<OrderDto>>> Get()
        //{
        //    var result = await _mediator.Send(new GetAllOrderQuery());
        //    return new ApiResult<List<OrderDto>>(result, string.Empty, ApiResultStatusCode.Success, true);
        //}

        //[HttpGet("get-by-id-order")]
        //public async Task<ApiResult<OrderDto>> GetById(string id, CancellationToken cancellationToken)
        //{
        //    var result = await _mediator.Send(new GetByIdQuery { Id = id }, cancellationToken);

        //    return new ApiResult<OrderDto>(result, string.Empty, ApiResultStatusCode.Success, true);
        //}
        
        
    }
}