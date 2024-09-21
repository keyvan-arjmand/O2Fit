using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Command.DeletePersonalFoods
{
    public class DeletePersonalFoodCommandHandler : IRequestHandler<DeletePersonalFoodCommand>, IScopedDependency
    {
        private readonly IRepositoryRedis<List<PersonalFoodSelectDTO>> _personalFoodListRepositoryRedis;
        private readonly IRepositoryRedis<PersonalFoodSelectDTO> _personalFoodRepositoryRedis;
        private readonly IPersonalFoodRepository _personalFoodRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public DeletePersonalFoodCommandHandler(
            IRepositoryRedis<List<PersonalFoodSelectDTO>> personalFoodListRepositoryRedis,
            IRepositoryRedis<PersonalFoodSelectDTO> personalFoodRepositoryRedis,
            IPersonalFoodRepository personalFoodRepository,
            IMediator mediator, IMapper mapper)
        {
            _personalFoodListRepositoryRedis = personalFoodListRepositoryRedis;
            _personalFoodRepositoryRedis = personalFoodRepositoryRedis;
            _personalFoodRepository = personalFoodRepository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeletePersonalFoodCommand request, CancellationToken cancellationToken)
        {
            var personalFood = await _personalFoodRepository.GetByIdAsync(request.Id, cancellationToken);
            if (personalFood!=null)
            {
                List<PersonalFoodSelectDTO> userPersonalFoodsRedis = await _personalFoodListRepositoryRedis.GetAsync($"UserPersonalFoods_{personalFood.UserId}");
                if (userPersonalFoodsRedis != null)
                {
                    var personalFoodDto = userPersonalFoodsRedis.Where(p => p.PersonalFoodId == personalFood.Id).FirstOrDefault();
                    userPersonalFoodsRedis.Remove(personalFoodDto);
                    await _personalFoodListRepositoryRedis.UpdateAsync($"UserPersonalFoods_{personalFood.UserId}", userPersonalFoodsRedis);
                }
                personalFood.Isdelete = true;
                await _personalFoodRepository.UpdateAsync(personalFood, cancellationToken);
                await _personalFoodRepositoryRedis.DeleteAsync($"PersonalFood_{personalFood.Id}");
            }
            return Unit.Value;
        }
    }
}
