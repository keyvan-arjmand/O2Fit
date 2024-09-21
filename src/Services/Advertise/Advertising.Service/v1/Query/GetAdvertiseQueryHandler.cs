using Advertising.Domain.Entities.Advertise;
using Common;
using Data.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Advertising.Service.v1.Query
{
    public class GetAdvertiseQueryHandler : IRequestHandler<GetAdvertiseQuery, AdSelect>, IScopedDependency
    {
        private readonly IRepository<Advertise> _repository;
        private readonly IRepositoryRedis<List<Advertise>> _repositoryRedis;
        private readonly IRepository<AdvertiseCountry> _repositoryCountry;

        public GetAdvertiseQueryHandler(IRepository<Advertise> repository, IRepositoryRedis<List<Advertise>> repositoryRedis, IRepository<AdvertiseCountry> repositoryCountry)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
            _repositoryCountry = repositoryCountry;
        }

        public async Task<AdSelect> Handle(GetAdvertiseQuery request, CancellationToken cancellationToken)
        {
            Random rd = new Random();

            AdSelect adSelect = new AdSelect();

            List<Advertise> _ListBackAds = new List<Advertise>();

            _ListBackAds = await _repositoryRedis.GetAsync($"Ads_Country_{request.CountryId}");

            if (_ListBackAds == null)
            {
                _ListBackAds = await _repositoryCountry.Table
                                                       .Include(a => a.Advertise)
                                                       .Include(a => a.Advertise.TranslationTitle)
                                                       .Include(a => a.Advertise.TranslationDescription)
                                                       .Include(a => a.Advertise.TranslationShortDescription)
                                                       .Include(a => a.Advertise.AdvertiseCountries)
                                                       .Where(a => a.Advertise.IsActive == true && a.CountryId == request.CountryId)
                                                       .Select(a => a.Advertise)
                                                       .ToListAsync(cancellationToken);

                if (_ListBackAds.Count > 0)
                {
                    foreach (var item in _ListBackAds)
                    {
                        _repository.Detach(item);
                    }
                }

                await _repositoryRedis.UpdateDisableLoopAsync($"Ads_Country_{request.CountryId}", _ListBackAds);
            }

            if (_ListBackAds.Count > 0)
            {
                Advertise _value = _ListBackAds[rd.Next(_ListBackAds.Count)];

                adSelect = new AdSelect()
                {
                    Id = _value.Id,
                    Url = _value.Url,
                    ImageUri = (_value.ImageUri != null) ? Common.CommonStrings.CommonUrl + "AdvrtiseImage/" + _value.ImageUri : null,
                    BannerUri = (_value.ImageUri != null) ? Common.CommonStrings.CommonUrl + "AdvrtiseBanner/" + _value.BannerUri : null,
                };

                switch (request.Language)
                {
                    case "Persian":
                        {
                            adSelect.Title = _value.TranslationTitle.Persian;
                            adSelect.Description = _value.TranslationDescription.Persian;
                            adSelect.ShortDescription = _value.TranslationShortDescription.Persian;
                            break;
                        }
                    case "English":
                        {
                            adSelect.Title = _value.TranslationTitle.English;
                            adSelect.Description = _value.TranslationDescription.English;
                            adSelect.ShortDescription = _value.TranslationShortDescription.English;
                            break;
                        }
                    case "Arabic":
                        {
                            adSelect.Title = _value.TranslationTitle.Arabic;
                            adSelect.Description = _value.TranslationDescription.Arabic;
                            adSelect.ShortDescription = _value.TranslationShortDescription.Arabic;
                            break;
                        }
                    default:
                        break;
                }
            }
            else { return null; } 

            return adSelect;
        }
    }
}
