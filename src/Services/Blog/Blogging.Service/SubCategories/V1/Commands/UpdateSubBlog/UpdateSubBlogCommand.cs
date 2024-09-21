using Blogging.Service.Dtos;
using MediatR;

namespace Blogging.Service.SubCategories.V1.Commands.UpdateSubBlog
{
    public class UpdateSubBlogCommand:IRequest
    {
        
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string AltImage { get; set; }
        public int CategoryId { get; set; }
    }
}