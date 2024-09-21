using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace FoodStuff.Service.v1.Query
{
    public class GetAllActiveDietPackQueryHandler : IRequestHandler<GetAllActiveDietPackQuery, List<DietCategoryResultDto>>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Diet.DietCategory> _repository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly IWebHostEnvironment _environment;

        public GetAllActiveDietPackQueryHandler(IRepository<Domain.Entities.Diet.DietCategory> repository, IMapper mapper, IRedisCacheClient redisCacheClient, IWebHostEnvironment environment)
        {
            _repository = repository;
            _mapper = mapper;
            _redisCacheClient = redisCacheClient;
            _environment = environment;
        }

        public async Task<List<DietCategoryResultDto>> Handle(GetAllActiveDietPackQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.TableNoTracking
                .Include(x => x.NameTranslation)
                .Include(x => x.DescriptionTranslation)
                .Where(x => x.IsActive).ToListAsync(cancellationToken).ConfigureAwait(false);

            var result = _mapper.Map<List<Domain.Entities.Diet.DietCategory>, List<DietCategoryResultDto>>(categories);

            //foreach (var dietCategory in result.Where(x => x.ParentId != null))
            //{
            //    if (dietCategory.ParentId > 0 || dietCategory.ParentId != null)
            //    {
            //        dietCategory.ParentCategory = _mapper.Map<Domain.Entities.Diet.DietCategory, DietCategoryResultDto>(
            //            await
            //                _repository.TableNoTracking
            //                    .Include(t => t.NameTranslation)
            //                    .Include(a => a.DescriptionTranslation)
            //                    .FirstOrDefaultAsync(x => x.Id == dietCategory.ParentId,
            //                    cancellationToken));
            //    }
            //}
            await _redisCacheClient.Db12.ReplaceAsync("DietCategory", result, expiresIn: TimeSpan.FromDays(30));
            return result;
            #region Old

            //foreach (var dto in result)
            //{
            //    string base64Image = string.Empty;
            //    if (!string.IsNullOrEmpty(dto.Image))
            //    {
            //        var path = Path.Combine(_environment.WebRootPath, "DietCategoryImg") + $@"/{dto.Image}";
            //        byte[] imageArray = await System.IO.File.ReadAllBytesAsync(path, cancellationToken);
            //        base64Image = Convert.ToBase64String(imageArray);
            //    }
            //    dto.Image = base64Image;

            //    string base64BannerImage = string.Empty;
            //    if (!string.IsNullOrEmpty(dto.BannerImage))
            //    {
            //        var path = Path.Combine(_environment.WebRootPath, "DietCategoryImg") + $@"/{dto.BannerImage}";
            //        byte[] imageArray = await System.IO.File.ReadAllBytesAsync(path, cancellationToken);
            //        base64BannerImage = Convert.ToBase64String(imageArray);
            //    }

            //    dto.BannerImage = base64BannerImage;
            //}
            //await _redisCacheClient.Db12.ReplaceAsync("DietCategory", result,expiresIn: TimeSpan.FromDays(7));

            #endregion

        }
    }
}