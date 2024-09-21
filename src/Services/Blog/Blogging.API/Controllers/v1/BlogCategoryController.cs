using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blogging.API.Data;
using Blogging.API.Models.V1;
using Blogging.Domain.Entities.Translation;
using Blogging.Service.BlogCategories.V1.Commands.InsertCategoryBlog;
using Blogging.Service.BlogCategories.V1.Commands.SoftDeleteCategory;
using Blogging.Service.BlogCategories.V1.Commands.UpdateCategoryBlog;
using Blogging.Service.BlogCategories.V1.Queries.GetAllCategory;
using Blogging.Service.BlogCategories.V1.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service.v1.Command;
using WebFramework.Api;
using CategoryDto = Blogging.Service.Dtos.CategoryDto;

namespace Blogging.API.Controllers.v1
{
    [ApiVersion("1")]
    public class BlogCategoryController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogCategoryController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("insert-category")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromForm] CreateCategory request,
            CancellationToken cancellationToken)
        {
            var up = new Upload(_webHostEnvironment);
            var title = await _mediator.Send(new CraeteTranslationCommand
            {
                Translation = new Translation
                {
                    Arabic = request.Title.Arabic,
                    English = request.Title.English,
                    Persian = request.Title.Persian,
                    IsDelete = false
                }
            }, cancellationToken);
           
            var command = new InsertCategoryBlogCommand
            {
                AltImage = request.AltImage,
                TitleId = title.Id,
                Type = request.Type
            };
            if (request.Image != null)
            {
                command.ImageName = up.Uploadfile(request.Image, "CategoryImage");
            }

            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPatch("update-category")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Put([FromForm] UpdateCategory request,
            CancellationToken cancellationToken)
        {
            var up = new Upload(_webHostEnvironment);
            var title = await _mediator.Send(new UpdateTranslationCommand
            {
                Translation = new Translation
                {
                    Id = request.Title.Id,
                    Arabic = request.Title.Arabic,
                    English = request.Title.English,
                    Persian = request.Title.Persian,
                    IsDelete = false
                }
            }, cancellationToken);
           
            var command = new UpdateCategoryBlogCommand
            {
                Id = request.Id,
                AltImage = request.AltImage,
                ImageName = request.Image == null ? request.ImageName : up.Uploadfile(request.Image, "CategoryImage"),
                Type = request.Type
            };

            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("soft-delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> SoftDelete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new SoftDeleteCategoryCommand
            {
                Id = id
            }, cancellationToken);
            return Ok();
        }

        [HttpGet("get-all")]
        public async Task<ApiResult<List<CategoryDto>>> GetAll(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllCategoryQuery(), cancellationToken);
        }

        [HttpGet("get-by-id")]
        public async Task<ApiResult<CategoryDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetCategoryByIdQuery
            {
                Id = id
            }, cancellationToken);
        }
    }
}