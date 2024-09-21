using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using FoodStuff.Common.Utilities;
using FoodStuff.Service.Models;
using FoodStuff.Service.v1.Command.Notes;
using FoodStuff.Service.v1.Query.Notes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    [Authorize(Roles = "Admin,Customer")]
    public class NoteController : BaseController
    {
        private readonly IMediator _mediator;

        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResult<PageResult<NoteDto>>> Get([FromQuery] GetAllPaginatedWithUserIdQuery query, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(query, cancellationToken);
           return new ApiResult<PageResult<NoteDto>>(true, ApiResultStatusCode.Success, data);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] List<CreateNoteCommand> commands, CancellationToken cancellationToken)
        {
            if (commands.Count > 0)
            {
                foreach (var cmd in commands)
                {
                    await _mediator.Send(cmd, cancellationToken);
                }
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteNoteByIdCommand
            {
                AppId = id
            }, cancellationToken);
            return Ok();
        }
    }
}