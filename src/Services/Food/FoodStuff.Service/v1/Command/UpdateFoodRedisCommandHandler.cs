using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Command
{
    public class UpdateFoodRedisCommandHandler : IRequestHandler<UpdateFoodRedisCommand, Unit>, ITransientDependency
    {
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly IRepository<Food> _repository;

        public UpdateFoodRedisCommandHandler(IRedisCacheClient redisCacheClient, IRepository<Food> repository)
        {
            _redisCacheClient = redisCacheClient;
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateFoodRedisCommand request, CancellationToken cancellationToken)
        {
            var food = await _repository.Table.Include(a => a.TranslationName)
                                        .Include(a => a.Brand)
                                        .ThenInclude(t => t.Translation)
                                        .Skip(0)
                                        .Take(500)
                                        .ToListAsync();

            foreach (var item in food)
            {
                FoodTranslation foodTranslation = new FoodTranslation
                {
                    FoodId = item.Id,
                    FoodType = (int)item.FoodType,
                    FoodCode = item.FoodCode,
                    NamePersian = item.TranslationName != null ? item.TranslationName.Persian.ToLower() : null,
                    NameEnglish = item.TranslationName.English != null ? item.TranslationName.English.ToLower() : null,
                    NameArabic = item.TranslationName.Arabic != null ? item.TranslationName.Arabic.ToLower() : null,
                    BrandNamePersian = item.Brand != null ? item.Brand.Translation.Persian.ToLower() : null,
                    BrandNameEnglish = item.Brand != null ? item.Brand.Translation.English.ToLower() : null,
                    BrandNameArabic = item.Brand != null ? item.Brand.Translation.Arabic.ToLower() : null,
                };

                await _redisCacheClient.Db1.AddAsync($"Food_Search_{item.Id}", foodTranslation);
            }

            return Unit.Value;
        }
    }
}
