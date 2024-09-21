using Blogging.Service.Dtos;
using MediatR;

namespace Blogging.Service.BlogCategories.V1.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery:IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }
}