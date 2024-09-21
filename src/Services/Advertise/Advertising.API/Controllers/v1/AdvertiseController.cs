using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertising.Domain.Entities.Translation;
using Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Advertising.Domain.Entities.Advertise;
using WebFramework.Api;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Advertise.API.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Advertising.Common.Utilities;
using AutoMapper;
using MediatR;
using Advertising.Domain.Entities;
using Service.v1.Command;
using Advertising.Data.Contracts;
using Advertising.Service.v1.Query;
using Advertising.API.Models;
using Advertising.Service.v1.Command;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Advertising.API.Controllers.v1
{
    [ApiVersion("1")]
    public class AdvertiseController : BaseController
    {
        private readonly IAdvertiseRepository _repository;
        private readonly IWebHostEnvironment _environment;
        private readonly IRepository<Translation> reptranslation;
        private readonly IRepository<AdvertiseCountry> _advertiseCountryRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AdvertiseController(IAdvertiseRepository repository,
            IRepository<AdvertiseCountry> advertiseCountryRepository, IWebHostEnvironment environment,
            IMediator mediator, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _environment = environment;
            _advertiseCountryRepository = advertiseCountryRepository;
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Post(CancellationToken cancellationToken)
        {
            var json = Request.Form;
            //var imgFile = json.Files[0];
            //var bannerFile = json.Files[1];

            var _AdvertiseDTO = Request.Form["JsonDetails"];
            AdvertiseDTO advertiseDTO = JsonConvert.DeserializeObject<AdvertiseDTO>(_AdvertiseDTO);

            Advertising.Domain.Entities.Advertise.Advertise Advertise = new Advertising.Domain.Entities.Advertise.Advertise();
            var imgFile = json.Files.Where(i => i.Name == "FileUpload.ImageUri").FirstOrDefault();
            var bannerFile = json.Files.Where(i => i.Name == "FileUpload.BannerUri").FirstOrDefault();
            if (imgFile.Length > 0)
            {
                var imgFolderName = Path.Combine("wwwroot", "AdvrtiseImage");
                var imgSavePath = Path.Combine(Directory.GetCurrentDirectory(), imgFolderName);
                string imgName = Guid.NewGuid() + Path.GetExtension(imgFile.FileName); //string.Format(@"{0}.jpg", DateTime.Now.Ticks);
                var fullPath = Path.Combine(imgSavePath, imgName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    imgFile.CopyTo(stream);
                }
                Advertise.ImageUri = imgName;
            }
            if (bannerFile.Length > 0)
            {
                var bannerFolderName = Path.Combine("wwwroot", "AdvrtiseBanner");
                var bannerSavePath = Path.Combine(Directory.GetCurrentDirectory(), bannerFolderName);
                string bannerName = Guid.NewGuid() + Path.GetExtension(bannerFile.FileName);
                var fullPath = Path.Combine(bannerSavePath, bannerName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    bannerFile.CopyTo(stream);
                }
                Advertise.BannerUri = bannerName;
            }

            var description = await _mediator.Send(new CraeteTranslationCommand
            {
                Translation = advertiseDTO.Description.ToEntity(_mapper)
            });
            Advertise.DescriptionId = description.Id;


            var ShortDescription = await _mediator.Send(new CraeteTranslationCommand
            {
                Translation = advertiseDTO.ShortDescription.ToEntity(_mapper)
            });
            Advertise.ShortDescriptionId = ShortDescription.Id;


            var Title = await _mediator.Send(new CraeteTranslationCommand
            {
                Translation = advertiseDTO.Title.ToEntity(_mapper)
            });
            Advertise.TitleId = Title.Id;

            Advertise.StartDate = advertiseDTO.StartDate;
            Advertise.EndDate = Advertise.EndDate;
            Advertise.ClickCount = advertiseDTO.ClickCount;
            Advertise.ClickPrice = advertiseDTO.ClickPrice;
            Advertise.ViewCount = advertiseDTO.ViewCount;
            Advertise.ViewPrice = advertiseDTO.ViewPrice;
            Advertise.Url = advertiseDTO.Url;
            Advertise.ChargeAmount = advertiseDTO.ChargeAmount;
            Advertise.IsActive = advertiseDTO.IsActive;

            Advertise = await _repository.AddAsync(Advertise, cancellationToken);

            List<AdvertiseCountry> advertiseCountries = new List<AdvertiseCountry>();

            List<string> _keys = new List<string>();

            foreach (var item in advertiseDTO.CountrieIds)
            {
                AdvertiseCountry advCountry = new AdvertiseCountry()
                {
                    AdvertiseId = Advertise.Id,
                    CountryId = item
                };
                advertiseCountries.Add(advCountry);

                _keys.Add($"Ads_Country_{item}");
            }

            await _advertiseCountryRepository.AddRangeAsync(advertiseCountries, cancellationToken);

            await _mediator.Send(new DeleteAdvertiseCountryCommand { Keys = _keys });

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<AdSelect>> GetAsync(int countryId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAdvertiseQuery { CountryId = countryId, Language = LanguageName ?? "English" });
        }

        [HttpPost("AdState")]
        [AllowAnonymous]
        public async Task<ApiResult> AdState(AdStateDto adStateDto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateAdvertiseByIdCommand { Id = adStateDto.Id, Click = adStateDto.Click, View = adStateDto.View, Hint = adStateDto.Hint });
            return Ok();
        }


        //[HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ApiResult> Update(CancellationToken cancellationToken)
        //{
        //    DeleteFile deleteFile = new DeleteFile(_environment);
        //    var json = Request.Form;
        //    var imgFile = json.Files.Where(i => i.Name == "FileUpload.ImageUri").FirstOrDefault();
        //    var bannerFile = json.Files.Where(i => i.Name == "FileUpload.BannerUri").FirstOrDefault();

        //    var _AdvertiseDTO = Request.Form["JsonDetails"];
        //    AdvertiseDTO advertiseDTO = JsonConvert.DeserializeObject<AdvertiseDTO>(_AdvertiseDTO);

        //    Advertising.Domain.Entities.Advertise.Advertise Advertise = await _repository.Table.Include(a => a.AdvertiseCountries).SingleOrDefaultAsync(a => a.Id == advertiseDTO.Id);
        //    _repository.Detach(Advertise);
        //   if (imgFile.Length > 0)
        //    {
        //        var imgFolderName = Path.Combine("wwwroot", "AdvrtiseImage");
        //        var imgSavePath = Path.Combine(Directory.GetCurrentDirectory(), imgFolderName);
        //        if (Advertise.ImageUri!=null)
        //        {
        //            deleteFile.DeleteFiles();
        //        }
        //        string imgName = Guid.NewGuid() + Path.GetExtension(imgFile.FileName);
        //        var fullPath = Path.Combine(imgSavePath, imgName);
        //        using (var stream = new FileStream(fullPath, FileMode.Create))
        //        {
        //            imgFile.CopyTo(stream);
        //        }
        //        Advertise.ImageUri = imgName;
        //    }
        //    if (bannerFile.Length > 0)
        //    {
        //        var bannerFolderName = Path.Combine("wwwroot", "AdvrtiseBanner");
        //        var bannerSavePath = Path.Combine(Directory.GetCurrentDirectory(), bannerFolderName);
        //        string bannerName = Guid.NewGuid() + Path.GetExtension(bannerFile.FileName);
        //        var fullPath = Path.Combine(bannerSavePath, bannerName);
        //        using (var stream = new FileStream(fullPath, FileMode.Create))
        //        {
        //            bannerFile.CopyTo(stream);
        //        }
        //        Advertise.BannerUri = bannerName;
        //    }



        //    Advertise.DescriptionId = Advertise.DescriptionId;
        //    await _mediator.Send(new UpdateTranslationCommand
        //    {
        //        Translation = Advertisedto.Description.ToEntity(_mapper)
        //    });
        //    Advertisedto.ShortDescription.Id = Advertise.ShortDescriptionId;
        //    await _mediator.Send(new UpdateTranslationCommand
        //    {
        //        Translation = Advertisedto.ShortDescription.ToEntity(_mapper)
        //    });
        //    Advertisedto.Title.Id = Advertise.TitleId;
        //    await _mediator.Send(new UpdateTranslationCommand
        //    {
        //        Translation = Advertisedto.Title.ToEntity(_mapper)
        //    });

        //    Advertise.IsActive = Advertisedto.IsActive;

        //    Advertise.StartDate = Advertisedto.StartDate;
        //    Advertise.EndDate = Advertise.EndDate;

        //    Advertise.ClickCount = Advertisedto.ClickCount;
        //    Advertise.ViewCount = Advertisedto.ViewCount;
        //    Advertise.HintCount = Advertisedto.HintCount;

        //    Advertise.ClickPrice = Advertisedto.ClickPrice;
        //    Advertise.ViewPrice = Advertisedto.ViewPrice;
        //    Advertise.ChargeAmount = Advertisedto.ChargeAmount;
        //    Advertise.Url = Advertisedto.Url;
        //    await _repository.UpdateAsync(Advertise, cancellationToken);

        //    await _mediator.Send(new DeleteAdvertiseCommand { Id = Advertise.Id });

        //    List<string> _keys = new List<string>();

        //    foreach (var item in Advertise.AdvertiseCountries)
        //    {
        //        _keys.Add($"Ads_Country_{item.CountryId}");
        //    }

        //    await _advertiseCountryRepository.DeleteRangeAsync(Advertise.AdvertiseCountries, cancellationToken);

        //    List<AdvertiseCountry> advertiseCountries = new List<AdvertiseCountry>();

        //    if (Advertisedto.CountrieIds.Count > 0)
        //    {
        //        foreach (var item in Advertisedto.CountrieIds)
        //        {
        //            AdvertiseCountry advCountry = new AdvertiseCountry()
        //            {
        //                AdvertiseId = Advertise.Id,
        //                CountryId = item
        //            };
        //            advertiseCountries.Add(advCountry);

        //            _keys.Add($"Ads_Country_{item}");
        //        }

        //        await _advertiseCountryRepository.AddRangeAsync(advertiseCountries, cancellationToken);

        //    }

        //    await _mediator.Send(new DeleteAdvertiseCountryCommand { Keys = _keys });

        //    return Ok();
        //}

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<AdvertiseDTO>> GetAll(int? Page, int PageSize, CancellationToken cancellationToken)
        {
            var _advertises = await _repository.Table
                  .Include(a => a.TranslationTitle)
                  .Include(a => a.TranslationShortDescription)
                  .Include(a => a.TranslationDescription)
                  .Include(a => a.AdvertiseCountries)
                  .OrderByDescending(o => o.Id)
                  .Skip((Page - 1 ?? 0) * PageSize).Take(PageSize).ToListAsync(cancellationToken);
            List<AdvertiseDTO> _Paging = _advertises.Select(a => new AdvertiseDTO
            {
                Id = a.Id,
                Title = a.TitleId > 0 ? new TranslationDto
                {
                    Id = a.TranslationTitle.Id,
                    Arabic = a.TranslationTitle.Arabic,
                    English = a.TranslationTitle.English,
                    Persian = a.TranslationTitle.Persian,
                } : null,
                ShortDescription = a.ShortDescriptionId > 0 ? new TranslationDto
                {
                    Id = a.TranslationShortDescription.Id,
                    Arabic = a.TranslationShortDescription.Arabic,
                    English = a.TranslationShortDescription.English,
                    Persian = a.TranslationShortDescription.Persian,
                } : null,
                Description = a.DescriptionId > 0 ? new TranslationDto
                {
                    Id = a.TranslationDescription.Id,
                    Arabic = a.TranslationDescription.Arabic,
                    English = a.TranslationDescription.English,
                    Persian = a.TranslationDescription.Persian,
                } : null,
                Url = a.Url,
                ImageUri = (a.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "AdvrtiseImage/" + a.ImageUri,
                BannerUri = (a.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "AdvrtiseBanner/" + a.BannerUri,
                ChargeAmount = a.ChargeAmount,
                ClickCount = a.ClickCount,
                ClickPrice = a.ClickPrice,
                EndDate = a.EndDate,
                StartDate = a.StartDate,
                HintCount = a.HintCount,
                ViewCount = a.ViewCount,
                ViewPrice = a.ViewPrice,
                IsActive = a.IsActive,
                CountrieIds = a.AdvertiseCountries.Select(c => c.CountryId).ToList(),
            }).ToList();
            PageResult<AdvertiseDTO> pageResult = new PageResult<AdvertiseDTO>()
            {
                Count = _Paging.Count(),
                Items = _Paging,
                PageIndex = Page ?? 1,
                PageSize = PageSize
            };
            return pageResult;
        }


        [HttpGet("GetFullById")]
        [Authorize(Roles = "Admin")]
        public async Task<AdvertiseDTO> GetFullById(int Id, CancellationToken cancellationToken)
        {
            var _advertise = await _repository.Table
                  .Include(a => a.TranslationTitle)
                  .Include(a => a.TranslationShortDescription)
                  .Include(a => a.TranslationDescription)
                  .Include(a => a.AdvertiseCountries).Where(d=>d.Id==Id).FirstAsync(cancellationToken);
            AdvertiseDTO advertise = new AdvertiseDTO
            {
                Id = _advertise.Id,
                Title = _advertise.TitleId > 0 ? new TranslationDto
                {
                    Id = _advertise.TranslationTitle.Id,
                    Arabic = _advertise.TranslationTitle.Arabic,
                    English = _advertise.TranslationTitle.English,
                    Persian = _advertise.TranslationTitle.Persian,
                } : null,
                ShortDescription = _advertise.ShortDescriptionId > 0 ? new TranslationDto
                {
                    Id = _advertise.TranslationShortDescription.Id,
                    Arabic = _advertise.TranslationShortDescription.Arabic,
                    English = _advertise.TranslationShortDescription.English,
                    Persian = _advertise.TranslationShortDescription.Persian,
                } : null,
                Description = _advertise.DescriptionId > 0 ? new TranslationDto
                {
                    Id = _advertise.TranslationDescription.Id,
                    Arabic = _advertise.TranslationDescription.Arabic,
                    English = _advertise.TranslationDescription.English,
                    Persian = _advertise.TranslationDescription.Persian,
                } : null,
                Url = _advertise.Url,
                ImageUri = (_advertise.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "AdvrtiseImage/" + _advertise.ImageUri,
                BannerUri = (_advertise.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "AdvrtiseBanner/" + _advertise.BannerUri,
                ChargeAmount = _advertise.ChargeAmount,
                ClickCount = _advertise.ClickCount,
                ClickPrice = _advertise.ClickPrice,
                EndDate = _advertise.EndDate,
                StartDate = _advertise.StartDate,
                HintCount = _advertise.HintCount,
                ViewCount = _advertise.ViewCount,
                ViewPrice = _advertise.ViewPrice,
                IsActive = _advertise.IsActive,
                CountrieIds = _advertise.AdvertiseCountries.Select(c => c.CountryId).ToList(),
            };
            return advertise;
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            Advertising.Domain.Entities.Advertise.Advertise Advertise = await _repository.Table.Include(a => a.AdvertiseCountries).SingleOrDefaultAsync(a => a.Id == id, cancellationToken);

            List<AdvertiseCountry> _adsCountry = Advertise.AdvertiseCountries.ToList();

            List<string> _keys = new List<string>();

            foreach (var item in _adsCountry)
            {
                _keys.Add($"Ads_Country_{item.CountryId}");
            }
            DeleteFile deleteFile = new DeleteFile(_environment);
            if (Advertise.ImageUri != null)
            {
                deleteFile.DeleteFiles(Advertise.ImageUri, "AdvrtiseImg");
            }


            if (Advertise.BannerUri != null)
            {
                deleteFile.DeleteFiles(Advertise.BannerUri, "AdvrtiseBanner");
            };

            List<int> _list = new List<int>();

            _list.Add(Advertise.TitleId);
            _list.Add(Advertise.DescriptionId);
            _list.Add(Advertise.ShortDescriptionId);

            await _mediator.Send(new DeleteAdvertiseCommand { Id = Advertise.Id });

            await _advertiseCountryRepository.DeleteRangeAsync(_adsCountry, cancellationToken);

            await _repository.DeleteAsync(Advertise, cancellationToken);

            await _mediator.Send(new DeleteTranslationCommand
            {
                Ids = _list
            });

            await _mediator.Send(new DeleteAdvertiseCountryCommand { Keys = _keys });

            return Ok();
        }


    }
}
