using Common;
using Data.Contracts;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query.GetVersion
{
    public class GetRedisFoodsVersionQueryHandller : IRequestHandler<GetRedisFoodsVersionQuery, FoodsVersionModel>, ITransientDependency
    {
        private readonly IRepositoryRedis<FoodsVersionModel> _repositoryRedis;
        public GetRedisFoodsVersionQueryHandller(IFoodRepository foodRepository, IRepositoryRedis<FoodsVersionModel> repositoryRedis)
        {
            _repositoryRedis = repositoryRedis;
        }
        public async Task<FoodsVersionModel> Handle(GetRedisFoodsVersionQuery request, CancellationToken cancellationToken)
        {
           var result= await _repositoryRedis.GetAsync($"FoodsVersion_{request.lastVersion+0.01}");
            return result;
        }
    }
}
