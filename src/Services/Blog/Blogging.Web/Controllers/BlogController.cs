using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blogging.Common.Utilities;
using Blogging.Domain.Entities.Blogs;
using Blogging.Domain.Entities.Translation;
using Blogging.Domain.Enum;
using Blogging.Service.BlogCategories.V1.Queries.GetAllCategory;
using Blogging.Service.Blogs.V1.Queries.GetSelected;
using Blogging.Service.Dtos;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _work;

        public BlogController(IMediator mediator, IUnitOfWork work)
        {
            _mediator = mediator;
            _work = work;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.header = await _work.Repository<Category>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.SubCategories).ThenInclude(x => x.Title)
                .ToListAsync();
            ViewBag.selectedBlog = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.Description)
                .Include(x => x.ShortDescription)
                .Include(x => x.SubCategory).ThenInclude(x => x.Title)
                .Include(x => x.SubCategory).ThenInclude(x => x.Category).ThenInclude(x => x.Title)
                .Where(x => x.FirstPagePost)
                .Take(3)
                .Select(x => new BlogDto
                {
                    SubCategory = new TranslationDto
                    {
                        Arabic = x.SubCategory.Title.Arabic,
                        English = x.SubCategory.Title.English,
                        Persian = x.SubCategory.Title.Persian
                    },
                    FirstPagePost = x.FirstPagePost,
                    Description = new TranslationDto
                    {
                        Arabic = x.Description.Arabic,
                        English = x.Description.English,
                        Persian = x.Description.Persian
                    },
                    AltBanner = x.AltBanner,
                    AltThumb = x.AltThumb,
                    Like = x.Like,
                    Title = new TranslationDto
                    {
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    ImageName = x.ImageName,
                    AltImage = x.AltImage,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    InsertDate = x.InsertDate.ToPersianTime(),
                    ShortDescription = new TranslationDto
                    {
                        Arabic = x.ShortDescription.Arabic,
                        English = x.ShortDescription.English,
                        Persian = x.ShortDescription.Persian
                    },
                    View = x.View,
                    Id = x.Id,
                    BannerName = x.BannerName,
                    ThumbName = x.ThumbName,
                    KeyWords = x.KeyWords
                })
                .ToListAsync();

            ViewBag.categories = await _work.Repository<Category>().TableNoTracking
                .Include(x => x.Title)
                .Take(5)
                .ToListAsync();

            ViewBag.lastBlog = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.Description)
                .Include(x => x.ShortDescription)
                .Include(x => x.SubCategory)
                .OrderBy(x => x.InsertDate)
                .Take(4)
                .Select(x => new BlogDto
                {
                    SubCategory = new TranslationDto
                    {
                        Arabic = x.SubCategory.Title.Arabic,
                        English = x.SubCategory.Title.English,
                        Persian = x.SubCategory.Title.Persian
                    },
                    FirstPagePost = x.FirstPagePost,
                    Description = new TranslationDto
                    {
                        Arabic = x.Description.Arabic,
                        English = x.Description.English,
                        Persian = x.Description.Persian
                    },
                    AltBanner = x.AltBanner,
                    AltThumb = x.AltThumb,
                    Like = x.Like,
                    Title = new TranslationDto
                    {
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    ImageName = x.ImageName,
                    AltImage = x.AltImage,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    InsertDate = x.InsertDate.ToPersianTime(),
                    ShortDescription = new TranslationDto
                    {
                        Arabic = x.ShortDescription.Arabic,
                        English = x.ShortDescription.English,
                        Persian = x.ShortDescription.Persian
                    },
                    View = x.View,
                    Id = x.Id,
                    BannerName = x.BannerName,
                    ThumbName = x.ThumbName,
                    KeyWords = x.KeyWords
                })
                .ToListAsync();

            ViewBag.mostView = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Title)
                .OrderBy(x => x.InsertDate)
                .Take(6)
                .Select(x => new BlogDto
                {
                    Id = x.Id,
                    Title = new TranslationDto
                    {
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                })
                .ToListAsync();

            ViewBag.DietBlog = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.Description)
                .Include(x => x.ShortDescription)
                .Include(x => x.SubCategory).ThenInclude(x => x.Category)
                .OrderBy(x => x.InsertDate)
                .Where(x => x.SubCategory.Category.Type == CategoryType.Diet)
                .Take(10)
                .Select(x => new BlogDto
                {
                    SubCategory = new TranslationDto
                    {
                        Arabic = x.SubCategory.Title.Arabic,
                        English = x.SubCategory.Title.English,
                        Persian = x.SubCategory.Title.Persian
                    },
                    FirstPagePost = x.FirstPagePost,
                    Description = new TranslationDto
                    {
                        Arabic = x.Description.Arabic,
                        English = x.Description.English,
                        Persian = x.Description.Persian
                    },
                    AltBanner = x.AltBanner,
                    AltThumb = x.AltThumb,
                    Like = x.Like,
                    Title = new TranslationDto
                    {
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    ImageName = x.ImageName,
                    AltImage = x.AltImage,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    InsertDate = x.InsertDate.ToPersianTime(),
                    ShortDescription = new TranslationDto
                    {
                        Arabic = x.ShortDescription.Arabic,
                        English = x.ShortDescription.English,
                        Persian = x.ShortDescription.Persian
                    },
                    View = x.View,
                    Id = x.Id,
                    BannerName = x.BannerName,
                    ThumbName = x.ThumbName,
                    KeyWords = x.KeyWords
                })
                .ToListAsync();

            ViewBag.RecepieisBlog = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.Description)
                .Include(x => x.ShortDescription)
                .Include(x => x.SubCategory).ThenInclude(x => x.Category)
                .OrderBy(x => x.InsertDate)
                .Where(x => x.SubCategory.Category.Type == CategoryType.Recipes)
                .Take(10)
                .Select(x => new BlogDto
                {
                    SubCategory = new TranslationDto
                    {
                        Arabic = x.SubCategory.Title.Arabic,
                        English = x.SubCategory.Title.English,
                        Persian = x.SubCategory.Title.Persian
                    },
                    FirstPagePost = x.FirstPagePost,
                    Description = new TranslationDto
                    {
                        Arabic = x.Description.Arabic,
                        English = x.Description.English,
                        Persian = x.Description.Persian
                    },
                    AltBanner = x.AltBanner,
                    AltThumb = x.AltThumb,
                    Like = x.Like,
                    Title = new TranslationDto
                    {
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    ImageName = x.ImageName,
                    AltImage = x.AltImage,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    InsertDate = x.InsertDate.ToPersianTime(),
                    ShortDescription = new TranslationDto
                    {
                        Arabic = x.ShortDescription.Arabic,
                        English = x.ShortDescription.English,
                        Persian = x.ShortDescription.Persian
                    },
                    View = x.View,
                    Id = x.Id,
                    BannerName = x.BannerName,
                    ThumbName = x.ThumbName,
                    KeyWords = x.KeyWords
                })
                .ToListAsync();

            ViewBag.FitnessBlog = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.Description)
                .Include(x => x.ShortDescription)
                .Include(x => x.SubCategory).ThenInclude(x => x.Category)
                .OrderBy(x => x.InsertDate)
                .Where(x => x.SubCategory.Category.Type == CategoryType.Fitness)
                .Take(10)
                .Select(x => new BlogDto
                {
                    SubCategory = new TranslationDto
                    {
                        Arabic = x.SubCategory.Title.Arabic,
                        English = x.SubCategory.Title.English,
                        Persian = x.SubCategory.Title.Persian
                    },
                    FirstPagePost = x.FirstPagePost,
                    Description = new TranslationDto
                    {
                        Arabic = x.Description.Arabic,
                        English = x.Description.English,
                        Persian = x.Description.Persian
                    },
                    AltBanner = x.AltBanner,
                    AltThumb = x.AltThumb,
                    Like = x.Like,
                    Title = new TranslationDto
                    {
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    ImageName = x.ImageName,
                    AltImage = x.AltImage,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    InsertDate = x.InsertDate.ToPersianTime(),
                    ShortDescription = new TranslationDto
                    {
                        Arabic = x.ShortDescription.Arabic,
                        English = x.ShortDescription.English,
                        Persian = x.ShortDescription.Persian
                    },
                    View = x.View,
                    Id = x.Id,
                    BannerName = x.BannerName,
                    ThumbName = x.ThumbName,
                    KeyWords = x.KeyWords
                })
                .ToListAsync();
            return View("Index");
        }
        public async Task<ActionResult> BlogSubCategory(int id = 1, int page = 1, int pageSize = 10)
        {
            ViewBag.header = await _work.Repository<Category>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.SubCategories).ThenInclude(x => x.Title)
                .ToListAsync();
            var cat = await _work.Repository<SubCategory>().TableNoTracking
                .Include(x => x.Category).ThenInclude(x => x.Title)
                .Include(x => x.Title)
                .FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.SubCat = cat.Title.Persian;
            ViewBag.rootName = cat.Category.Title.Persian;
            ViewBag.root = "صفحه اصلی" + " > " + cat.Category.Title.Persian + " > " + cat.Title.Persian;
            ViewBag.blogCats = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.Description)
                .Include(x => x.ShortDescription)
                .Include(x => x.SubCategory).ThenInclude(x => x.Category)
                .OrderBy(x => x.InsertDate)
                .Where(x => x.SubCategoryId == id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new BlogDto
                {
                    SubCategory = new TranslationDto
                    {
                        Arabic = x.SubCategory.Title.Arabic,
                        English = x.SubCategory.Title.English,
                        Persian = x.SubCategory.Title.Persian
                    },
                    FirstPagePost = x.FirstPagePost,
                    Description = new TranslationDto
                    {
                        Arabic = x.Description.Arabic,
                        English = x.Description.English,
                        Persian = x.Description.Persian
                    },
                    AltBanner = x.AltBanner,
                    AltThumb = x.AltThumb,
                    Like = x.Like,
                    Title = new TranslationDto
                    {
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    ImageName = x.ImageName,
                    AltImage = x.AltImage,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    InsertDate = x.InsertDate.ToPersianTime(),
                    ShortDescription = new TranslationDto
                    {
                        Arabic = x.ShortDescription.Arabic,
                        English = x.ShortDescription.English,
                        Persian = x.ShortDescription.Persian
                    },
                    View = x.View,
                    Id = x.Id,
                    BannerName = x.BannerName,
                    ThumbName = x.ThumbName,
                    KeyWords = x.KeyWords
                })
                .ToListAsync();
            return View("BlogSubCat");
        }

        public async Task<ActionResult> LikeBlog(int id)
        {
            var blog = await _work.Repository<Blog>().Table.FirstOrDefaultAsync(x => x.Id == id);
            blog.Like++;
            await _work.Repository<Blog>().UpdateAsync(blog, new CancellationToken());
            return Ok();
        }
        public async Task<ActionResult> BlogById(int id)
        {
            ViewBag.categories = await _work.Repository<Category>().TableNoTracking
                .Include(x => x.Title)
                .Take(5)
                .ToListAsync();
            ViewBag.header = await _work.Repository<Category>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.SubCategories).ThenInclude(x => x.Title)
                .ToListAsync();
            var blog = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Description)
                .Select(x => new BlogDto
                {
                    SubCategory = new TranslationDto
                    {
                        Arabic = x.SubCategory.Title.Arabic,
                        English = x.SubCategory.Title.English,
                        Persian = x.SubCategory.Title.Persian
                    },
                    FirstPagePost = x.FirstPagePost,
                    Description = new TranslationDto
                    {
                        Arabic = x.Description.Arabic,
                        English = x.Description.English,
                        Persian = x.Description.Persian
                    },
                    AltBanner = x.AltBanner,
                    AltThumb = x.AltThumb,
                    Like = x.Like,
                    Title = new TranslationDto
                    {
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    ImageName = x.ImageName,
                    AltImage = x.AltImage,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    InsertDate = x.InsertDate.ToPersianTime(),
                    ShortDescription = new TranslationDto
                    {
                        Arabic = x.ShortDescription.Arabic,
                        English = x.ShortDescription.English,
                        Persian = x.ShortDescription.Persian
                    },
                    View = x.View,
                    Id = x.Id,
                    BannerName = x.BannerName,
                    ThumbName = x.ThumbName,
                    KeyWords = x.KeyWords,
                    EnDate = x.InsertDate
                })
                .FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.blog = blog;
            ViewBag.lastBlog = await _work.Repository<Blog>().TableNoTracking
                .OrderBy(x => x.InsertDate)
                .Include(x=>x.ShortDescription)
                .Include(x=>x.Title)
                .Take(6)
                .Select(x => new BlogDto
                {
                    SubCategory = new TranslationDto
                    {
                        Arabic = x.SubCategory.Title.Arabic,
                        English = x.SubCategory.Title.English,
                        Persian = x.SubCategory.Title.Persian
                    },
                    FirstPagePost = x.FirstPagePost,
                    Description = new TranslationDto
                    {
                        Arabic = x.Description.Arabic,
                        English = x.Description.English,
                        Persian = x.Description.Persian
                    },
                    AltBanner = x.AltBanner,
                    AltThumb = x.AltThumb,
                    Like = x.Like,
                    Title = new TranslationDto
                    {
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    ImageName = x.ImageName,
                    AltImage = x.AltImage,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    InsertDate = x.InsertDate.ToPersianTime(),
                    ShortDescription = new TranslationDto
                    {
                        Arabic = x.ShortDescription.Arabic,
                        English = x.ShortDescription.English,
                        Persian = x.ShortDescription.Persian
                    },
                    View = x.View,
                    Id = x.Id,
                    BannerName = x.BannerName,
                    ThumbName = x.ThumbName
                }).ToListAsync();
            ViewBag.relatedBlog = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Title)
                .Include(x=>x.ShortDescription)
                .Where(x => x.SubCategoryId == blog.SubCategoryId).Take(3).ToListAsync();

            return View("BlogDetail");
        }

        public async Task<ActionResult> BlogCategory(int id = 1, int page = 1, int pageSize = 10)
        {
            ViewBag.header = await _work.Repository<Category>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.SubCategories).ThenInclude(x => x.Title)
                .ToListAsync();
            var cat = await _work.Repository<Category>().TableNoTracking
                .Include(x => x.Title)
                .FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.rootName = cat.Title.Persian;
            ViewBag.root = "صفحه اصلی" + " > " + cat.Title.Persian;
            ViewBag.blogCats = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.Description)
                .Include(x => x.ShortDescription)
                .Include(x => x.SubCategory).ThenInclude(x => x.Category)
                .OrderBy(x => x.InsertDate)
                .Where(x => x.SubCategory.CategoryId == id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new BlogDto
                {
                    SubCategory = new TranslationDto
                    {
                        Arabic = x.SubCategory.Title.Arabic,
                        English = x.SubCategory.Title.English,
                        Persian = x.SubCategory.Title.Persian
                    },
                    FirstPagePost = x.FirstPagePost,
                    Description = new TranslationDto
                    {
                        Arabic = x.Description.Arabic,
                        English = x.Description.English,
                        Persian = x.Description.Persian
                    },
                    AltBanner = x.AltBanner,
                    AltThumb = x.AltThumb,
                    Like = x.Like,
                    Title = new TranslationDto
                    {
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    ImageName = x.ImageName,
                    AltImage = x.AltImage,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    InsertDate = x.InsertDate.ToPersianTime(),
                    ShortDescription = new TranslationDto
                    {
                        Arabic = x.ShortDescription.Arabic,
                        English = x.ShortDescription.English,
                        Persian = x.ShortDescription.Persian
                    },
                    View = x.View,
                    Id = x.Id,
                    BannerName = x.BannerName,
                    ThumbName = x.ThumbName,
                    KeyWords = x.KeyWords
                })
                .ToListAsync();
            return View("BlogCat");
        }

        public async Task<ActionResult> SearchBlog(string search, int page = 1, int pageSize = 10)
        {
            ViewBag.header = await _work.Repository<Category>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.SubCategories).ThenInclude(x => x.Title)
                .ToListAsync();
            ViewBag.root = search;
            ViewBag.blogCats = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.Description)
                .Include(x => x.ShortDescription)
                .Include(x => x.SubCategory).ThenInclude(x => x.Category).ThenInclude(x => x.Title)
                .OrderBy(x => x.InsertDate)
                .Where(x => x.Title.Persian.Contains(search) || x.ShortDescription.Persian.Contains(search) ||
                            x.SubCategory.Title.Persian.Contains(search) ||
                            x.SubCategory.Category.Title.Persian.Contains(search) || x.KeyWords.Contains(search))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new BlogDto
                {
                    SubCategory = new TranslationDto
                    {
                        Arabic = x.SubCategory.Title.Arabic,
                        English = x.SubCategory.Title.English,
                        Persian = x.SubCategory.Title.Persian
                    },
                    FirstPagePost = x.FirstPagePost,
                    Description = new TranslationDto
                    {
                        Arabic = x.Description.Arabic,
                        English = x.Description.English,
                        Persian = x.Description.Persian
                    },
                    AltBanner = x.AltBanner,
                    AltThumb = x.AltThumb,
                    Like = x.Like,
                    Title = new TranslationDto
                    {
                        Arabic = x.Title.Arabic,
                        English = x.Title.English,
                        Persian = x.Title.Persian
                    },
                    ImageName = x.ImageName,
                    AltImage = x.AltImage,
                    SubCategoryId = x.SubCategoryId,
                    Status = x.Status,
                    InsertDate = x.InsertDate.ToPersianTime(),
                    ShortDescription = new TranslationDto
                    {
                        Arabic = x.ShortDescription.Arabic,
                        English = x.ShortDescription.English,
                        Persian = x.ShortDescription.Persian
                    },
                    View = x.View,
                    Id = x.Id,
                    BannerName = x.BannerName,
                    ThumbName = x.ThumbName,
                    KeyWords = x.KeyWords
                })
                .ToListAsync();
            return View("SearchBlog");
        }
    }
}