using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading;
using System.Threading.Tasks;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Service.Models;
using FoodStuff.Service.v1.Query.GetIngredients;
using FoodStuff.Service.v2.Query.Ingredient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v2
{
    [ApiVersion("2")]

    public class IngredientController : BaseController
    {
        private readonly IMediator _mediator;

        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("SearchAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<IngredientSearchAdminResultDto>> SearchAdmin(string name, int? page, int pageSize
            , CancellationToken cancellationToken)
        {
            List<IngredientSearchAdminResultDto> _paging = await _mediator.Send(new GetIngredientSearchAdminQuery
            {
                Name = name,
                Page = page,
                PageSize = pageSize,
            }, cancellationToken);
            var result = new PageResult<IngredientSearchAdminResultDto>
            {
                PageIndex = page ?? 1,
                PageSize = pageSize,
                Items = _paging
            };
            return result;
        }

    }
}
