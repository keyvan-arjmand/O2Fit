using MediatR;

namespace Blogging.Service.BlogCategories.V1.Commands.SoftDeleteCategory
{
    public class SoftDeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }
}