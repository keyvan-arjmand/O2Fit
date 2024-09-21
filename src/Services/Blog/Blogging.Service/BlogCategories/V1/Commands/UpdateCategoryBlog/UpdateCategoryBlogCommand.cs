using Blogging.Domain.Enum;
using MediatR;

namespace Blogging.Service.BlogCategories.V1.Commands.UpdateCategoryBlog
{
    public class UpdateCategoryBlogCommand:IRequest
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string AltImage { get; set; }
        public CategoryType Type { get; set; }

    }
}