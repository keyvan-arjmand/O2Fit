using AutoMapper;
using Food.Application.Common.Interfaces.Persistence.Repositories;
using Food.Application.Common.Interfaces.Services;
using Food.Application.Common.Specification;
using Food.Domain.Entities;
using MediatR;
using Newtonsoft.Json;

namespace Food.Grpc.Services.Query;

public class GetDietUserPackageQueryHandler : IRequestHandler<GetDietUserPackageQuery, ResonseDietPack>
{
    private readonly IGenericRepository<DietPack> _dietPackRepository;
    private readonly IGenericRepository<DietPackAlerge> _alergesRepository;
    private readonly IResponseCacheService _responseCacheService;
    private readonly IMapper _mapper;
    public GetDietUserPackageQueryHandler(IGenericRepository<DietPack> dietPackRepository, IGenericRepository<DietPackAlerge> alergyRepository, IResponseCacheService responseCacheService, IMapper mapper)
    {
        _dietPackRepository = dietPackRepository;
        _alergesRepository = alergyRepository;
        _responseCacheService = responseCacheService;
        _mapper = mapper;
    }

    public async Task<ResonseDietPack> Handle(GetDietUserPackageQuery request, CancellationToken cancellationToken)
    {
        double calorieResult = 0;

        switch (string.Compare(request.DietPacks.DailyCalorie.ToString().Substring(2, 2), "50"))
        {
            case 0:
                calorieResult = request.DietPacks.DailyCalorie;
                break;
            case -1:
                calorieResult = Convert.ToDouble(request.DietPacks.DailyCalorie.ToString().Replace(request.DietPacks.DailyCalorie.ToString().Substring(2, 2), "00"));
                break;
            case 1:
                calorieResult = Convert.ToDouble(request.DietPacks.DailyCalorie.ToString().Replace(request.DietPacks.DailyCalorie.ToString().Substring(2, 2), "50"));
                break;
            default:
                break;
        }
        //with AllergyIds
        if (!string.IsNullOrEmpty(request.DietPacks.AllergyIds))
        {
            var allergyIds = request.DietPacks.AllergyIds.Split(',');

            string allergyKey = string.Empty;
            foreach (var id in allergyIds)
            {
                allergyKey += "_" + Convert.ToString(id);
            }

            int[] allergyIdsInt = new int[allergyIds.Count()];
            for (var index = 0; index < allergyIds.Length; index++)
            {
                var id = allergyIds[index];
                var intId = Convert.ToInt32(id);
                allergyIdsInt[index] = intId;
            }

            var cachedResult = await _responseCacheService.GetCachedResponseAsync(
                $"DietPackFoods_{calorieResult}_{request.DietPacks.DietCategoryId}_{allergyKey}");

            if (!string.IsNullOrEmpty(cachedResult))
            {
                return JsonConvert.DeserializeObject<ResonseDietPack>(cachedResult);
            }
            var specDietPack = new GetUserPackageIncludeDietPackDietCategoriesIncludeNameIncludeDietPackFoodsIncludeDietPackFoodsSpec(calorieResult, request.DietPacks.DietCategoryId);
            var dietPacks = await _dietPackRepository.ListAsync(specDietPack, cancellationToken);

            var specAllergy = new GetAllAllergyIdsForDietPackSpec(allergyIdsInt);
            var alerges = await _alergesRepository.ListAsync(specAllergy, cancellationToken);

            var packs = dietPacks.Where(x => !alerges.Select(x => x.DietPackId).Contains(x.Id)).ToList();


            var result = _mapper.Map<List<DietPack>, List<GrpcDietPack>>(packs);

            if (result.Count == 0)
            {
                return new ResonseDietPack();
            }

            ResonseDietPack dietPack = new ResonseDietPack();
            dietPack.DietPacks.AddRange(result);

            await _responseCacheService.CacheResponseAsync($"DietPackFoods_{calorieResult}_{request.DietPacks.DietCategoryId}_{allergyKey}", dietPack, timeToLive: TimeSpan.FromDays(90));
            return await Task.FromResult(dietPack);


        }
        //without AllergyIds
        else
        {
            var cachedResult =
                await _responseCacheService.GetCachedResponseAsync(
                    $"DietPackFoods_{calorieResult}_{request.DietPacks.DietCategoryId}");

            if (!string.IsNullOrEmpty(cachedResult))
            {
                return JsonConvert.DeserializeObject<ResonseDietPack>(cachedResult);
            }
            var specDietPack = new GetUserPackageIncludeDietPackDietCategoriesIncludeNameIncludeDietPackFoodsIncludeDietPackFoodsSpec(calorieResult, request.DietPacks.DietCategoryId);
            var dietPacks = await _dietPackRepository.ListAsync(specDietPack, cancellationToken);

            var result = _mapper.Map<List<DietPack>, List<GrpcDietPack>>(dietPacks);

            if (result.Count == 0)
            {
                return new ResonseDietPack();
            }
            ResonseDietPack dietPack = new ResonseDietPack();
            dietPack.DietPacks.AddRange(result);

            await _responseCacheService.CacheResponseAsync($"DietPackFoods_{calorieResult}_{request.DietPacks.DietCategoryId}", dietPack, timeToLive: TimeSpan.FromDays(90));
            return await Task.FromResult(dietPack);

        }

    }
}