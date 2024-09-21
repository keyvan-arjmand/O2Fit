using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Diet;
using MediatR;

namespace FoodStuff.Service.v1.Command.UserTrackDietPack
{
    public class CreateUserTrackDietPackCommandHandler : IRequestHandler<CreateUserTrackDietPackCommand, Unit>
    , IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Diet.UserTrackDietPack> _repository;
        private readonly IRepository<UserTrackDietPackDetail> _userTrackDietPackDetailRepository;

        public CreateUserTrackDietPackCommandHandler(IRepository<Domain.Entities.Diet.UserTrackDietPack>
            repository, IRepository<UserTrackDietPackDetail> userTrackDietPackDetailRepository)
        {
            _repository = repository;
            _userTrackDietPackDetailRepository = userTrackDietPackDetailRepository;
        }

        public async Task<Unit> Handle(CreateUserTrackDietPackCommand request, CancellationToken cancellationToken)
        {

            var userTrackDietPack = new Domain.Entities.Diet.UserTrackDietPack
            {
                DailyCalorie = request.DailyCalorie,
                EndDate = request.EndDate,
                PackageName = request.PackageName,
                NutritionistId = request.NutritionistId,
                StartDate = request.StartDate,
                UserId = request.UserId,
                CreateDate = DateTime.Now,
                IsDelete = false
            };

            await _repository.AddAsync(userTrackDietPack, cancellationToken);


            List<UserTrackDietPackDetail> userTrackDietPackDetails = new List<UserTrackDietPackDetail>();

            foreach (var item in request.UserTrackDietPacks)
            {
                var userDietPackDetail = new UserTrackDietPackDetail
                {
                    Meal = item.Meal,
                    DietPackId = item.DietPackId,
                    Repeat = item.Repeat,
                    UserTrackDietPackId = userTrackDietPack.Id
                };
                userTrackDietPackDetails.Add(userDietPackDetail);
            }

            await _userTrackDietPackDetailRepository.AddRangeAsync(userTrackDietPackDetails, cancellationToken);

            return Unit.Value;
        }
    }
}
