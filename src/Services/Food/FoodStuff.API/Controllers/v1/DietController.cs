using FoodStuff.API.Models;
using FoodStuff.Service.v1.Query.DietQuery;
using MediatR;
using System.Threading;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    public class DietController : BaseController
    {
        private readonly IMediator _mediator;
        public DietController(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async void Get(int UserId)
        {
            var usertrackDiet = await _mediator.Send(new GetDietQuery
            {
                UserId = UserId
            });
        }

        public async void Post(DietDTO diet, CancellationToken cancellationToken)
        {
            // var _diet = await _mediator.Send(new CreateDietCommand
            // {
            //     BodyType = diet.BodyType,
            //     Calori = diet.Calori,
            //     Alergies = diet.Alergies,
            //     Category = diet.Category,
            //     country = diet.country,
            //     StartDate = diet.StartDate,
            //     UserId = diet.UserId,
            //     ZCalori = diet.ZCalori
            //
            // });

        }
    }
}
