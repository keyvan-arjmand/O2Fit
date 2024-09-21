using Blogging.Domain.Enum;
using MediatR;

namespace Blogging.Service.BlogCategories.V1.Commands.InsertCategoryBlog
{
    public class InsertCategoryBlogCommand:IRequest
    {
        public int TitleId { get; set; } 
        public string ImageName { get; set; } 
        public string AltImage { get; set; } 
        public CategoryType Type { get; set; }

    }
}