using System.Collections.Generic;
using Blogging.Domain.Enum;

namespace Blogging.Service.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public TranslationDto Title { get; set; } 
        public string ImageName { get; set; } 
        public string AltImage { get; set; }
        public CategoryType Type { get; set; }
        public List<SubCategoryBlogDto> SubCategories { get; set; }
    }
}