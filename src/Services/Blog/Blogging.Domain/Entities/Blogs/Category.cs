using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Blogging.Domain.Common;
using Blogging.Domain.Enum;

namespace Blogging.Domain.Entities.Blogs
{
    public class Category : BaseEntity
    {
        public int TitleId { get; set; }
        [ForeignKey("TitleId")] public Translation.Translation Title { get; set; }
        public string ImageName { get; set; }
        public string AltImage { get; set; }
        public CategoryType Type { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}