using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blogging.Common.Utilities;
using Blogging.Domain.Entities.Blogs;
using Blogging.Service.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Blogging.Web.Models;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Blogging.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _work;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork work)
        {
            _logger = logger;
            _work = work;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.LastBlog = await _work.Repository<Blog>()
                .TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.ShortDescription)
                .Include(x => x.SubCategory).ThenInclude(x => x.Title)
                .Include(x => x.SubCategory).ThenInclude(x => x.Category).ThenInclude(x => x.Title)
                .Take(10).ToListAsync();
            return View();
        }
        public async Task<int> SendMessage(string Email, string Subject, string Message)
        {
            int m = 0;
            ContactUsMessage _message = new ContactUsMessage()
            {
                AdminId = 0,
                CanReply = true,
                Classification = 5,
                ImageUri = null,
                InsertDate = DateTime.Now,
                IsForce = false,
                IsGeneral = false,
                IsRead = false,
                IsReadAdmin = false,
                Language = null,
                Message = $"Email : {Email} <br/>" + Message,
                ReplyToMessage = 0,
                Title = Subject,
                ToAdmin = true,
                Url = null,
                UserId = 0
            };

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://social.o2fitt.com/api/");
            var apiuSerilize = JsonConvert.SerializeObject(_message);
            var content = new StringContent(apiuSerilize, Encoding.UTF8, "application/json");
            var comp = await client.PostAsync($"v1/ContactUs/WebMessage", content);
            var json = await comp.Content.ReadAsStringAsync();
            GlobalResult messur = JsonConvert.DeserializeObject<GlobalResult>(json);
            if (messur.message == "عملیات با موفقیت انجام شد")
                m = 1;
            return m;
        }
        public async Task<ActionResult> Privacy()
        {
            return View();
        }

        public async Task<ActionResult> TermsAndConditions()
        {
            return View();
        }
        public async Task<ActionResult> FAQ()
        {
            return View();
        }
        public async Task<ActionResult> AboutUs()
        {
            return View();
        }
        public async Task<ActionResult> Error()
        {
            ViewBag.header = await _work.Repository<Category>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.SubCategories).ThenInclude(x => x.Title)
                .ToListAsync();
            ViewBag.lastBlog = await _work.Repository<Blog>().TableNoTracking
                .Include(x => x.Title)
                .Include(x => x.Description)
                .Include(x => x.ShortDescription)
                .Include(x => x.SubCategory)
                .OrderBy(x => x.InsertDate)
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
                    ThumbName = x.ThumbName,
                    KeyWords = x.KeyWords
                })
                .ToListAsync();
            return View();
        }
    }
}