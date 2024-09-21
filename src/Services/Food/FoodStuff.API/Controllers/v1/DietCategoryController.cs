using System;
using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.API.Models;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Service.Contracts;
using FoodStuff.Service.Models;
using FoodStuff.Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.v1.Command;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;
using FoodStuff.Service.Models;
using FoodStuff.Data.Migrations;
using FoodStuff.Service.v1.Query;
using MongoDB.Bson;
using FoodStuff.Service.v1.Query.DietCategory;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    public class DietCategoryController : BaseController
    {
        private readonly IRepository<DietCategory> _repository;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;
        private readonly IRepository<FoodDietCategory> _foodDietCategoryRepository;
        private readonly IRepository<DietPackDietCategory> _dietPackDietCategoryRepository;
        private readonly IRepository<Translation> _translationRepository;
        private readonly IRedisCacheClient _redisCacheClient;


        public DietCategoryController(IRepository<DietCategory> repository,
            IMediator mediator, IMapper mapper,
            IWebHostEnvironment environment, IFileService fileService,
            IRepository<FoodDietCategory> foodDietCategoryRepository,
            IRepository<DietPackDietCategory> dietPackDietCategoryRepository,
            IRepository<Translation> translationRepository, IRedisCacheClient redisCacheClient)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _environment = environment;
            _fileService = fileService;
            _foodDietCategoryRepository = foodDietCategoryRepository;
            _dietPackDietCategoryRepository = dietPackDietCategoryRepository;
            _translationRepository = translationRepository;
            _redisCacheClient = redisCacheClient;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllAsync")]
        public async Task<PageResult<DietCategoryResultDto>> GetAllAsync(int? page, int pageSize
          , CancellationToken cancellationToken)
        {

            return await _mediator.Send(new GetAllAdminQuery
            {
                Page = page,
                PageSize = pageSize
            }, cancellationToken);

            #region Old

            //var listCategories = await _repository.Table.Include(
            //      m => m.DescriptionTranslation)
            //  .Include(a => a.NameTranslation)
            //  .OrderBy(i => i.Id).ToListAsync(cancellationToken);

            //var countDetails = listCategories.Count();
            //List<DietCategoryResultDto> paging =
            //    _mapper.Map<List<DietCategory>, List<DietCategoryResultDto>>(listCategories);

            //if (listCategories.Count > 0)
            //{
            //    foreach (var item in listCategories)
            //    {
            //        string base64Image = string.Empty;
            //        if (!string.IsNullOrEmpty(item.Image))
            //        {
            //            var path = Path.Combine(_environment.WebRootPath, "DietCategoryImg") + $@"/{item.Image}";
            //            byte[] imageArray = await System.IO.File.ReadAllBytesAsync(path, cancellationToken);
            //            base64Image = Convert.ToBase64String(imageArray);
            //        }

            //        string base64BannerImage = string.Empty;
            //        if (!string.IsNullOrEmpty(item.BannerImage))
            //        {
            //            var path = Path.Combine(_environment.WebRootPath, "DietCategoryImg") + $@"/{item.BannerImage}";
            //            byte[] imageArray = await System.IO.File.ReadAllBytesAsync(path, cancellationToken);
            //            base64BannerImage = Convert.ToBase64String(imageArray);
            //        }
            //        DietCategoryDTO invoicePaging = new DietCategoryDTO()
            //        {

            //            Name = new TranslationDto()
            //            {
            //                Id = item.NameTranslation.Id,
            //                Arabic = item.NameTranslation.Arabic,
            //                English = item.NameTranslation.English,
            //                Persian = item.NameTranslation.Persian
            //            },
            //            Description = new TranslationDto()
            //            {
            //                Id = item.DescriptionTranslation.Id,
            //                Arabic = item.DescriptionTranslation.Arabic,
            //                English = item.DescriptionTranslation.English,
            //                Persian = item.DescriptionTranslation.Persian
            //            },

            //            Image = base64Image,
            //            BannerImage = base64BannerImage,
            //            Id = item.Id,
            //            ParentId = item.ParentId,
            //            IsActive = item.IsActive,
            //            IsPromote = item.IsPromote
            //        };

            //        paging.Add(invoicePaging);
            //    }
            //}

            //var result = new PageResult<DietCategoryDTO>
            //{
            //    Count = countDetails,
            //    PageIndex = page ?? 1,
            //    PageSize = pageSize,
            //    Items = paging
            //};

            //return result;

            #endregion
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(DietCategoryDTO dietCategoryDto, CancellationToken cancellationToken)
        {
            DietCategory dietCategory = new DietCategory();

            if (!string.IsNullOrEmpty(dietCategoryDto.Image))
            {
                var fileName = _fileService.AddImage(dietCategoryDto.Image, "DietCategoryImg", dietCategoryDto.Name.English.Trim());

                dietCategory.Image = fileName;
            }

            if (!string.IsNullOrEmpty(dietCategoryDto.BannerImage))
            {
                var fileName = _fileService.AddImage(dietCategoryDto.BannerImage, "DietCategoryImg",
                    $"{dietCategoryDto.Name.English.Trim()} Banner");

                dietCategory.BannerImage = fileName;
            }

            var name = await _mediator.Send(new CreateTranslationCommand
            {
                Translation = dietCategoryDto.Name.ToEntity(_mapper)
            }, cancellationToken);

            var des = await _mediator.Send(new CreateTranslationCommand
            {
                Translation = dietCategoryDto.Description.ToEntity(_mapper)
            }, cancellationToken);


            dietCategory.NameId = name.Id;
            dietCategory.DescriptionId = des.Id;
            dietCategory.ParentId = dietCategoryDto.ParentId;
            dietCategory.IsActive = dietCategoryDto.IsActive;
            dietCategory.IsPromote = dietCategoryDto.IsPromote;

            await _repository.AddAsync(dietCategory, cancellationToken);



            await _mediator.Send(new GetAllActiveDietPackQuery(), cancellationToken);
            return Ok();

        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult> PutAsync(DietCategoryDTO dietCategoryDto, CancellationToken cancellationToken)
        {

            var oldDietCategory = await _repository.Table.FirstOrDefaultAsync(c => c.Id == dietCategoryDto.Id, cancellationToken);

            if (!string.IsNullOrEmpty(dietCategoryDto.Image) && string.IsNullOrEmpty(dietCategoryDto.ImageUri))
            {

                var imageFolderName = Path.Combine("wwwroot", "DietCategoryImg");


                if (oldDietCategory.Image != null)
                {
                    DeleteFile delete = new DeleteFile(_environment);
                    delete.DeleteFiles(oldDietCategory.Image, imageFolderName);
                }

                var fileName = _fileService.AddImage(dietCategoryDto.Image, "DietCategoryImg", dietCategoryDto.Name.English.Trim().Replace(':','-'));

                oldDietCategory.Image = fileName;
            }
            else if(string.IsNullOrEmpty(dietCategoryDto.ImageUri))
            {
                var imageFolderName = Path.Combine("wwwroot", "DietCategoryImg");


                if (oldDietCategory.Image != null)
                {
                    DeleteFile delete = new DeleteFile(_environment);
                    delete.DeleteFiles(oldDietCategory.Image, imageFolderName);
                }

                oldDietCategory.Image = null;
            }

            if (!string.IsNullOrEmpty(dietCategoryDto.BannerImage) && string.IsNullOrEmpty(dietCategoryDto.BannerImageUri))
            {

                var imageFolderName = Path.Combine("wwwroot", "DietCategoryImg");


                if (oldDietCategory.BannerImage != null)
                {
                    DeleteFile delete = new DeleteFile(_environment);
                    delete.DeleteFiles(oldDietCategory.BannerImage, imageFolderName);
                }

                var fileName = _fileService.AddImage(dietCategoryDto.BannerImage, "DietCategoryImg",
                    $"{dietCategoryDto.Name.English.Trim().Replace(':', '-')} Banner");

                oldDietCategory.BannerImage = fileName;
            }
            else if(string.IsNullOrEmpty(dietCategoryDto.BannerImageUri))
            {
                var imageFolderName = Path.Combine("wwwroot", "DietCategoryImg");


                if (oldDietCategory.BannerImage != null)
                {
                    DeleteFile delete = new DeleteFile(_environment);
                    delete.DeleteFiles(oldDietCategory.BannerImage, imageFolderName);
                }

                oldDietCategory.BannerImage = null;
            }

            var name = await _mediator.Send(new UpdateTranslationCommand
            {
                Translation = dietCategoryDto.Name.ToEntity(_mapper)
            }, cancellationToken);

            var des = await _mediator.Send(new UpdateTranslationCommand
            {
                Translation = dietCategoryDto.Description.ToEntity(_mapper)
            }, cancellationToken);
            oldDietCategory.NameId = name.Id;
            oldDietCategory.DescriptionId = des.Id;
            oldDietCategory.ParentId = dietCategoryDto.ParentId;
            oldDietCategory.Id = dietCategoryDto.Id;
            oldDietCategory.IsActive = dietCategoryDto.IsActive;
            oldDietCategory.IsPromote = dietCategoryDto.IsPromote;
            await _repository.UpdateAsync(oldDietCategory, cancellationToken);



            await _mediator.Send(new GetAllActiveDietPackQuery(), cancellationToken);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetById")]
        public async Task<ApiResult<DietCategoryResultDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetByIdQuery
            {
                Id = id
            }, cancellationToken);
            #region Old

            //   var cat = await _repository.Table
            //.Include(t => t.NameTranslation)
            //.Include(a => a.DescriptionTranslation)
            //.Where(m => m.Id == id).SingleOrDefaultAsync(cancellationToken);



            //   string base64Image = string.Empty;
            //   if (!string.IsNullOrEmpty(cat.Image))
            //   {
            //       var path = Path.Combine(_environment.WebRootPath, "DietCategoryImg") + $@"/{cat.Image}";
            //       byte[] imageArray = await System.IO.File.ReadAllBytesAsync(path, cancellationToken);
            //       base64Image = Convert.ToBase64String(imageArray);
            //   }

            //   string base64BannerImage = string.Empty;
            //   if (!string.IsNullOrEmpty(cat.BannerImage))
            //   {
            //       var path = Path.Combine(_environment.WebRootPath, "DietCategoryImg") + $@"/{cat.BannerImage}";
            //       byte[] imageArray = await System.IO.File.ReadAllBytesAsync(path, cancellationToken);
            //       base64BannerImage = Convert.ToBase64String(imageArray);
            //   }

            //   var result = _mapper.Map<DietCategory, DietCategoryResultDto>(cat);
            //   result.BannerImage = base64BannerImage;
            //   result.Image = base64Image;
            //   DietCategoryDTO result = new DietCategoryDTO()
            //   {
            //       Name = new TranslationDto
            //       {
            //           Id = cat.NameTranslation.Id,
            //           Arabic = cat.NameTranslation.Arabic,
            //           English = cat.NameTranslation.English,
            //           Persian = cat.NameTranslation.Persian
            //       },
            //       Description = new TranslationDto()
            //       {
            //           Id = cat.DescriptionTranslation.Id,
            //           Arabic = cat.DescriptionTranslation.Arabic,
            //           English = cat.DescriptionTranslation.English,
            //           Persian = cat.DescriptionTranslation.Persian
            //       },
            //       Image = base64Image,
            //       Id = cat.Id,
            //       ParentId = cat.ParentId,
            //       BannerImage = base64BannerImage,
            //       IsPromote = cat.IsPromote,
            //       IsActive = cat.IsActive,
            //   };

            //   if (cat.ParentId != 0)
            //   {
            //       var parent = await _repository.Table
            //           .Include(t => t.NameTranslation)
            //           .Include(a => a.DescriptionTranslation)
            //           .Where(m => m.Id == cat.ParentId).SingleOrDefaultAsync(cancellationToken);

            //       string parentBase64Image = string.Empty;
            //       if (!string.IsNullOrEmpty(cat.Image))
            //       {
            //           var path = Path.Combine(_environment.WebRootPath, "DietCategoryImg") + $@"/{cat.Image}";
            //           byte[] imageArray = await System.IO.File.ReadAllBytesAsync(path, cancellationToken);
            //           parentBase64Image = Convert.ToBase64String(imageArray);
            //       }

            //       string parentBase64BannerImage = string.Empty;
            //       if (!string.IsNullOrEmpty(cat.BannerImage))
            //       {
            //           var path = Path.Combine(_environment.WebRootPath, "DietCategoryImg") + $@"/{cat.BannerImage}";
            //           byte[] imageArray = await System.IO.File.ReadAllBytesAsync(path, cancellationToken);
            //           parentBase64BannerImage = Convert.ToBase64String(imageArray);
            //       }

            //       var parentResult = _mapper.Map<DietCategory, DietCategoryResultDto>(parent);
            //       parentResult.BannerImage = parentBase64BannerImage;
            //       parentResult.Image = parentBase64Image;

            //       var parentResult = new ParentDietCategoryDto()
            //       {
            //           Name = new TranslationResultDto()
            //           {
            //               Id = parent.NameTranslation.Id,
            //               Arabic = parent.NameTranslation.Arabic,
            //               English = parent.NameTranslation.English,
            //               Persian = parent.NameTranslation.Persian
            //           },
            //           Description = new TranslationResultDto()
            //           {
            //               Id = parent.DescriptionTranslation.Id,
            //               Arabic = parent.DescriptionTranslation.Arabic,
            //               English = parent.DescriptionTranslation.English,
            //               Persian = parent.DescriptionTranslation.Persian
            //           },
            //           Image = parentBase64Image,
            //           Id = parent.Id,
            //           ParentId = parent.ParentId,
            //           BannerImage = parentBase64BannerImage,
            //           IsPromote = parent.IsPromote,
            //           IsActive = parent.IsActive,
            //       };
            //       result.ParentCategory = parentResult;
            //   }
            //   return result;

            #endregion
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            DeleteFile deleteFile = new DeleteFile(_environment);
            DietCategory dietCategory = await _repository.Table
                .Include(a => a.NameTranslation)
                .Include(a => a.DescriptionTranslation)
                .Include(fdc => fdc.FoodDietCategories)
                .Include(dpdc => dpdc.DietPackDietCategories)
                .FirstOrDefaultAsync(a => a.Id == id,
                    cancellationToken: cancellationToken);


            await _foodDietCategoryRepository.DeleteRangeAsync(dietCategory.FoodDietCategories, cancellationToken);

            await _dietPackDietCategoryRepository.DeleteRangeAsync(dietCategory.DietPackDietCategories, cancellationToken);

            List<Translation> translations = new List<Translation>
            {
                dietCategory.NameTranslation,
                dietCategory.DescriptionTranslation
            };

            await _repository.DeleteAsync(dietCategory, cancellationToken);

            await _translationRepository.DeleteRangeAsync(translations, cancellationToken);


            if (dietCategory.Image != null)
            {
                deleteFile.DeleteFiles(dietCategory.Image, "DietCategoryImg");
            }
            if (!string.IsNullOrEmpty(dietCategory.BannerImage))
            {
                deleteFile.DeleteFiles(dietCategory.BannerImage, "DietCategoryImg");
            }
            return Ok();
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("GetAllActive")]
        public async Task<ApiResult<List<DietCategoryResultDto>>> GetAllActive(CancellationToken cancellationToken)
        {
    
            if (await _redisCacheClient.Db12.GetAsync<List<DietCategoryResultDto>>("DietCategory") != null)
            {
                return new ApiResult<List<DietCategoryResultDto>>(true, ApiResultStatusCode.Success,
                    await _redisCacheClient.Db12.GetAsync<List<DietCategoryResultDto>>("DietCategory"));
            }
            var result = await _mediator.Send(new GetAllActiveDietPackQuery(), cancellationToken);

            return new ApiResult<List<DietCategoryResultDto>>(true, ApiResultStatusCode.Success, result);

        }


    }
}
