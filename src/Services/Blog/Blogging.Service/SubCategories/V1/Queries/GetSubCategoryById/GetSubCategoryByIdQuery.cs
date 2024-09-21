using Blogging.Service.Dtos;
using MediatR;

namespace Blogging.Service.SubCategories.V1.Queries.GetSubCategoryById
{
    public class GetSubCategoryByIdQuery:IRequest<SubCategoryBlogDto>
    {
        public int Id { get; set; }
    }
}