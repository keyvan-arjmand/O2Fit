using AutoMapper;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMessaging.API.Models;
using SocialMessaging.Domain.Entities.App;
using SocialMessaging.Domain.Enum;
using SocialMessaging.Service.v1.Command;
using SocialMessaging.Service.v1.Query;
using SocialMessaging.Service.ViewModels;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace SocialMessaging.API.Controllers.v1
{

    [ApiVersion("1")]
    public class AppVersionController : BaseController
    {

        private readonly IRepository<AppVersion> _appVersionRepository;
        private readonly IRepository<AppVersionMarketType> _appVersionMarketTypeRepository;
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AppVersionController(IRepository<AppVersion> appVersionRepository,
            IRepository<AppVersionMarketType> appVersionMarketTypeRepository, IMediator mediator, IRedisCacheClient redisCacheClient, IMapper mapper)
        {
            _appVersionRepository = appVersionRepository;
            _appVersionMarketTypeRepository = appVersionMarketTypeRepository;
            _mediator = mediator;
            _redisCacheClient = redisCacheClient;
            _mapper = mapper;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Post(CreateAppVersionDTO createAppVersionDTO, CancellationToken cancellationToken)
        {

            int adminId = int.Parse(User.Claims.First(i => i.Type == "UserId").Value);

            var des = await _mediator.Send(new CreateTranslationCommand
            {
                Translation = createAppVersionDTO.Description.ToEntity(_mapper)
            }, cancellationToken);
            
            var appVersion = new AppVersion
            {
                AdminId = adminId,
                DateCreate = DateTime.Now,
                IsForced = createAppVersionDTO.IsForced,
                Version = createAppVersionDTO.Version,
                DescId = des.Id
            };
            await _appVersionRepository.AddAsync(appVersion, cancellationToken);

            foreach (var item in createAppVersionDTO.MarketTypesDTO)
            {
                await _appVersionMarketTypeRepository.AddAsync(new AppVersionMarketType
                {
                    AppVersionId = appVersion.Id,
                    MarketType = item.MarketType,
                    Link = item.Link,
                }, cancellationToken);
            }

            await _redisCacheClient.Db10.FlushDbAsync();

            return Ok();

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Update(UpdateAppVersionDTO updateAppVersionDTO, CancellationToken cancellationToken)
        {
            int adminId = int.Parse(User.Claims.First(i => i.Type == "UserId").Value);

            var pastAppVersion = await _appVersionRepository.Table
                .Include(appMar => appMar.appVersionMarketTypes)
                .Where(a => a.Id == updateAppVersionDTO.Id).FirstAsync();

            var des = await _mediator.Send(new UpdateTranslationCommand
            {
                Translation = updateAppVersionDTO.Description.ToEntity(_mapper)
            }, cancellationToken);

            pastAppVersion.AdminId = adminId;
            pastAppVersion.Version = updateAppVersionDTO.Version;
            pastAppVersion.IsForced = updateAppVersionDTO.IsForced;
            pastAppVersion.DescId = des.Id;

            await _appVersionRepository.UpdateAsync(pastAppVersion, cancellationToken);

            if (pastAppVersion.appVersionMarketTypes.Any())
            {
                _appVersionMarketTypeRepository.DeleteRange(pastAppVersion.appVersionMarketTypes);
            }

            foreach (var item in updateAppVersionDTO.MarketTypesDTO)
            {
                await _appVersionMarketTypeRepository.AddAsync(new AppVersionMarketType
                {
                    AppVersionId = pastAppVersion.Id,
                    MarketType = item.MarketType,
                    Link = item.Link,
                }, cancellationToken);
            }


            await _redisCacheClient.Db10.FlushDbAsync();

            return Ok();
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<List<AppVersion>> GetAll(int? page, int pageSize, CancellationToken cancellationToken)
        {
            var appVersions = await _appVersionRepository.TableNoTracking
                .Include(am => am.appVersionMarketTypes)
                .Include(x => x.Description)
                 .Skip((page - 1 ?? 0) * pageSize)
                .Take(pageSize)
                .OrderByDescending(a => a.Id)
                .ToListAsync(cancellationToken);
            List<AppVersion> result = new List<AppVersion>();
            if (appVersions.Count > 0)
            {
                foreach (var i in appVersions)
                {
                    AppVersion appVersion = new AppVersion()
                    {
                        AdminId = i.AdminId,
                        appVersionMarketTypes = i.appVersionMarketTypes,
                        Id = i.Id,
                        DateCreate = i.DateCreate,
                        DescId = i.DescId,
                        Description = new Domain.Entities.Translation.TranslationDto
                        {
                            Id = i.Description.Id,
                            Arabic = i.Description.Arabic,
                            English = i.Description.English,
                            Persian = i.Description.Persian
                        },
                        IsForced = i.IsForced,
                        Version = i.Version,
                    };
                    result.Add(appVersion);
                }

            }
            return result;
        }

        [HttpGet("GetAppVersion")]
        [Authorize]
        public async Task<ApiResult<AppVersionViewModel>> GetAppVersion(MarketType marketType, string curentAppVersion, CancellationToken cancellationToken)
        {
            if (await _redisCacheClient.Db10.ExistsAsync($"AppVersion_{marketType}_{curentAppVersion}"))
            {
                AppVersionViewModel result = await _redisCacheClient.Db10.GetAsync<AppVersionViewModel>
                    ($"AppVersion_{marketType}_{curentAppVersion}");
                return Ok(result);
            }
            else
            {
                AppVersionViewModel result = await _mediator.Send(new GetAppVersionQuery
                {
                    MarketType = marketType,
                    AppVersion = curentAppVersion,
                });
                result.Description = await _appVersionRepository.TableNoTracking
                       .Where(x => x.Version == curentAppVersion)
                       .Select(x => new Domain.Entities.Translation.TranslationDto
                       {
                           Arabic = x.Description.Arabic,
                           English = x.Description.English,
                           Persian = x.Description.Persian,
                           Id = x.Id
                       })
                       .FirstOrDefaultAsync();

                await _redisCacheClient.Db10.AddAsync($"AppVersion_{marketType}_{curentAppVersion}",
                    result == null ? null : new AppVersionViewModel
                    {
                        IsForced = result.IsForced,
                        Link = result.Link,
                        Description = result.Description,
                    });

                return Ok(result);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var appVersion = await _appVersionRepository.TableNoTracking
                .Include(appMar => appMar.appVersionMarketTypes)
                .Where(a => a.Id == Id).FirstAsync();

            await _appVersionMarketTypeRepository.DeleteRangeAsync(appVersion.appVersionMarketTypes, cancellationToken);

            await _appVersionRepository.DeleteAsync(appVersion, cancellationToken);

            await _redisCacheClient.Db10.FlushDbAsync();

            return Ok();
        }
    }
}
