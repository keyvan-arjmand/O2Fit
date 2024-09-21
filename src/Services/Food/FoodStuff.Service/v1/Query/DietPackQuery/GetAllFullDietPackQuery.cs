using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;

namespace FoodStuff.Service.v1.Query.DietPackQuery
{
    public class GetAllFullDietPackQuery : IRequest<PageResult<GetAllFullDietPackViewModel>>
    {
        public int? Page { get; set; }
        public int PageSize { get; set; }
    }
}
