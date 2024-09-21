using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blogging.Common.Utilities;
using Blogging.Domain.Entities.Blogs;
using Blogging.Service.Dtos;
using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Service.Blogs.V1.Queries.GetBlogBySubCategory
{
    public class GetBlogBySubCategoryQueryHandler : IRequestHandler<GetBlogBySubCategoryQuery, List<BlogDto>>, ITransientDependency
    {
        private readonly IRepository<Blog> _repository;

        public GetBlogBySubCategoryQueryHandler(IRepository<Blog> repository)
        {
            _repository = repository;
        }

        public async Task<List<BlogDto>> Handle(GetBlogBySubCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _repository.TableNoTracking
                .Include(x => x.Description)
                .Include(x => x.Title)
                .Include(x => x.ShortDescription)
                .Where(x => x.SubCategoryId == request.Id)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new BlogDto
                {
                    Id = x.Id,
                    AltBanner = x.AltBanner,
                    AltThumb = x.AltThumb,
                    SubCategoryId = x.SubCategoryId,
                    Description = new TranslationDto
                    {
                        Persian = x.Description.Persian,
                        Arabic = x.Description.Arabic,
                        English = x.Description.English
                    },
                    Title = new TranslationDto
                    {
                        Persian = x.Title.Persian,
                        Arabic = x.Title.Arabic,
                        English = x.Title.English
                    },
                    ShortDescription = new TranslationDto
                    {
                        Persian = x.ShortDescription.Persian,
                        Arabic = x.ShortDescription.Arabic,
                        English = x.ShortDescription.English
                    },
                    AltImage = x.AltImage,
                    Like = x.Like,
                    Status = x.Status,
                    View = x.View,
                    BannerName = x.BannerName,
                    ImageName = x.AltImage,
                    ThumbName = x.ThumbName,
                    InsertDate = x.InsertDate.ToPersianTime(),
                    FirstPagePost = x.FirstPagePost,
                    KeyWords = x.KeyWords
                }).ToListAsync(cancellationToken);
        }
    }
}