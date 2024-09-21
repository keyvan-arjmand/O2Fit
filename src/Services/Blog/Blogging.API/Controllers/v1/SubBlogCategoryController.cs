using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blogging.API.Data;
using Blogging.Domain.Entities.Translation;
using Blogging.Service.Dtos;
using Blogging.Service.Models;
using Blogging.Service.SubCategories.V1.Commands.InsertSubBlog;
using Blogging.Service.SubCategories.V1.Commands.SoftDeleteSubBlog;
using Blogging.Service.SubCategories.V1.Commands.UpdateSubBlog;
using Blogging.Service.SubCategories.V1.Queries.GetAllSubCategory;
using Blogging.Service.SubCategories.V1.Queries.GetByCategoryId;
using Blogging.Service.SubCategories.V1.Queries.GetSubCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service.v1.Command;
using WebFramework.Api;

namespace Blogging.API.Controllers.v1
{
    [ApiVersion("1")]
    public class SubBlogCategoryController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubBlogCategoryController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("insert-sub-category")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromForm] CreateSubCategoryDto request,
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
            var command = new InsertSubBlogCommand
            {
                AltImage = request.AltImage,
                TitleId = title.Id,
                CategoryId = request.CategoryId,
            };
            if (request.Image != null)
            {
                command.ImageName = up.Uploadfile(request.Image, "SubCategoryImage");
            }

            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPatch("update-sub-category")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Put([FromForm] UpdateSubBlogDto request,
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
                }
            }, cancellationToken);

            var command = new UpdateSubBlogCommand
            {
                AltImage = request.AltImage,
                CategoryId = request.CategoryId,
                ImageName =
                    request.Image == null ? request.ImageName : up.Uploadfile(request.Image, "SubCategoryImage"),
                Id = request.Id
            };
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("soft-delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> SoftDelete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new SoftDeleteSubBlogCommand
            {
                Id = id
            }, cancellationToken);
            return Ok();
        }

        [HttpGet("get-all")]
        public async Task<ApiResult<List<SubCategoryBlogDto>>> GetAll(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllSubCategoryQuery(), cancellationToken);
        }

        [HttpGet("get-by-id")]
        public async Task<ApiResult<SubCategoryBlogDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetSubCategoryByIdQuery
            {
                Id = id
            }, cancellationToken);
        }

        [HttpGet("get-by-categoryId")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<List<SubCategoryBlogDto>>> GetByCategoryId(int id,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetByCategoryIdQuery
            {
                Id = id
            }, cancellationToken);
        }
    }
}