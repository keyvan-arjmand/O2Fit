using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Query.DietPackQuery
{
   public class GetByIdsListQuery:IRequest<List<GetDietPackViewModel>>
    {
        public int Id { get; set; }
        public int DietCategoryId { get; set; }
        public int NationalityId { get; set; }
    }
}
