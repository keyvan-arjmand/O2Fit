using MediatR;

namespace FoodStuff.Service.v1.Query.DietPackQuery
{
   public class GetDietPackByIdQuery: IRequest<GetDietPackViewModel>
    {
        public int Id { get; set; }
        public string Language { get; set; }
    }
}
