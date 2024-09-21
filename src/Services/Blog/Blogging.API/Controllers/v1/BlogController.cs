using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blogging.API.Data;
using Blogging.API.Models.V1;
using Blogging.Domain.Entities.Translation;
using Blogging.Domain.Enum;
using Blogging.Service.Blogs.V1.Commands.InsertBlog;
using Blogging.Service.Blogs.V1.Commands.SoftDeleteBlog;
using Blogging.Service.Blogs.V1.Commands.UpdateBlog;
using Blogging.Service.Blogs.V1.Commands.UpdateBlogStatus;
using Blogging.Service.Blogs.V1.Queries.GetAllBlog;
using Blogging.Service.Blogs.V1.Queries.GetBlogById;
using Blogging.Service.Blogs.V1.Queries.GetBlogBySubCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.v1.Command;
using WebFramework.Api;
using BlogDto = Blogging.Service.Dtos.BlogDto;

namespace Blogging.API.Controllers.v1
{
    [ApiVersion("1")]
    public class BlogController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        [HttpPost("insert-image-to-directory")]
        public async Task<string> InsertImage(IFormFile image)
        {
            if (image == null)
            {
                return string.Empty;
            }

            var up = new Upload(_webHostEnvironment);
            return "https://blog.o2fitt.com/BlogDirectory/" + up.Uploadfile(image, "BlogDirectory");
        }

        [HttpPost("insert-blog")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Post([FromForm] CreateBlog request, CancellationToken cancellationToken)
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
            var desc = await _mediator.Send(new CraeteTranslationCommand
            {
                Translation = new Translation
                {
                    Arabic = request.Description.Arabic,
                    English = request.Description.English,
                    Persian = request.Description.Persian,
                    IsDelete = false
                }
            }, cancellationToken);
            var shortDesc = await _mediator.Send(new CraeteTranslationCommand
            {
                Translation = new Translation
                {
                    Arabic = request.ShortDescription.Arabic,
                    English = request.ShortDescription.English,
                    Persian = request.ShortDescription.Persian,
                    IsDelete = false
                }
            }, cancellationToken);
            var command = new InsertBlogCommand
            {
                AltImage = request.AltImage,
                DescriptionId = desc.Id,
                TitleId = title.Id,
                Status = request.Status,
                SubCategoryId = request.SubCategoryId,
                ShortDescriptionId = shortDesc.Id,
                AltBanner = request.AltBanner,
                AltThumb = request.AltThumb,
                Like = request.Like,
                FirstPagePost = request.FirstPagePost,
                KeyWords = request.KeyWords
            };
            if (request.Image != null)
            {
                command.Image = up.Uploadfile(request.Image, "BlogImage");
            }

            if (request.Thumb != null)
            {
                command.Thumb = up.Uploadfile(request.Thumb, "BlogImage");
            }

            if (request.Banner != null)
            {
                command.Banner = up.Uploadfile(request.Banner, "BlogImage");
            }

            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPatch("update-blog")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Put([FromForm] UpdateBlog request, CancellationToken cancellationToken)
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
            var desc = await _mediator.Send(new UpdateTranslationCommand
            {
                Translation = new Translation
                {
                    Id = request.Description.Id,
                    Arabic = request.Description.Arabic,
                    English = request.Description.English,
                    Persian = request.Description.Persian,
                }
            }, cancellationToken);
            var shortDesc = await _mediator.Send(new UpdateTranslationCommand
            {
                Translation = new Translation
                {
                    Id = request.ShortDescription.Id,
                    Arabic = request.ShortDescription.Arabic,
                    English = request.ShortDescription.English,
                    Persian = request.ShortDescription.Persian,
                }
            }, cancellationToken);

            var command = new UpdateBlogCommand
            {
                Id = request.Id,
                AltImage = request.AltImage,
                Image = request.Image == null ? request.ImageName : up.Uploadfile(request.Image, "BlogImage"),
                Banner = request.Banner == null ? request.BannerName : up.Uploadfile(request.Banner, "BlogImage"),
                Thumb = request.Thumb == null ? request.ThumbName : up.Uploadfile(request.Thumb, "BlogImage"),
                SubCategoryId = request.SubCategoryId,
                Status = request.Status,
                AltBanner = request.AltBanner,
                AltThumb = request.AltThumb,
                FirstPagePost = request.FirstPagePost,
                KeyWords = request.KeyWords
            };
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpGet("get-by-sub-category-id")]
        public async Task<ApiResult<List<BlogDto>>> GetBySubCategoryId(int page, int pageSize, int id,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetBlogBySubCategoryQuery
            {
                Id = id,
                Page = page,
                PageSize = pageSize,
            }, cancellationToken);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new SoftDeleteBlogCommand
            {
                Id = id
            }, cancellationToken);
            return Ok();
        }

        [HttpGet("get-all")]
        public async Task<ApiResult<List<BlogDto>>> GetAll(int page, int pageSize, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllBlogQuery(), cancellationToken);
        }

        [HttpGet("get-by-id")]
        public async Task<ApiResult<BlogDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBlogByIdQuery { Id = id }, cancellationToken);
            return result;
        }


        [HttpPatch("set-blog-status")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> SetBlogStatus(int id, BlogStatus status, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateBlogStatusCommand
            {
                Id = id,
                Status = status,
            }, cancellationToken);
            return Ok();
        }
    }
}