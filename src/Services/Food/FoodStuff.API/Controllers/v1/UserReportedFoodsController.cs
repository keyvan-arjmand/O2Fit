using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.Food;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    public class UserReportedFoodsController : BaseController
    {
        private readonly IRepository<UserReportedFoods> _repository;
        private readonly IWebHostEnvironment _environment;

        public UserReportedFoodsController(IRepository<UserReportedFoods> repository,
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        [HttpGet]
        public async Task<PageResult<UserReportedFoods>> GetAsync(int? Page, int PageSize
           , CancellationToken cancellationToken)
        {
            List<UserReportedFoods> userReportedFoods = await _repository.TableNoTracking.OrderByDescending(i => i.Id)
                                        .Skip((Page - 1 ?? 0) * PageSize)
                                        .Take(PageSize)
                                       .ToListAsync(cancellationToken);

            var countDetails = _repository.TableNoTracking.Count();


            var result = new PageResult<UserReportedFoods>
            {
                Count = countDetails,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = userReportedFoods
            };

            return result;

        }



        [HttpPost]
        public async Task<IActionResult> Post(UserReportedFoods userReportedFoods, CancellationToken cancellationToken)
        {
            userReportedFoods.UserId = int.Parse(User.Claims.First(i => i.Type == "UserId").Value);
            userReportedFoods.DateCreate = DateTime.Now;
            UploadedFileBase64 uploadedFileBase64 = new UploadedFileBase64(_environment);


            if (userReportedFoods.FirstImage != null)
            {
                userReportedFoods.FirstImage = await uploadedFileBase64.InsertFiles(userReportedFoods.FirstImage, "UserReportedImages");
            }

            if (userReportedFoods.SecendImage != null)
            {
                userReportedFoods.SecendImage = await uploadedFileBase64.InsertFiles(userReportedFoods.SecendImage, "UserReportedImages");
            }

            if (userReportedFoods.ThirdImage != null)
            {
                userReportedFoods.ThirdImage = await uploadedFileBase64.InsertFiles(userReportedFoods.ThirdImage, "UserReportedImages");
            }

            await _repository.AddAsync(userReportedFoods, cancellationToken);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var userreported = await _repository.GetByIdAsync(cancellationToken, id);
            await _repository.DeleteAsync(userreported, cancellationToken);
            UploadedFileBase64 uploadedFileBase64 = new UploadedFileBase64(_environment);

            if (userreported.FirstImage != null)
            {
                uploadedFileBase64.DeleteFiles(userreported.FirstImage, "UserReportedImages");
            }

            if (userreported.SecendImage != null)
            {
                uploadedFileBase64.DeleteFiles(userreported.SecendImage, "UserReportedImages");
            }

            if (userreported.ThirdImage != null)
            {
                uploadedFileBase64.DeleteFiles(userreported.ThirdImage, "UserReportedImages");
            }


            return Ok();
        }
    }
}
