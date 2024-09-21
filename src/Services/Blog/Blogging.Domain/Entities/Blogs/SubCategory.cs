using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Blogging.Domain.Common;

namespace Blogging.Domain.Entities.Blogs
{
    public class SubCategory : BaseEntity
    {
        public string ImageName { get; set; }
        public string AltImage { get; set; }
        public int TitleId { get; set; }
        [ForeignKey("TitleId")] public Translation.Translation Title { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")] public Category Category { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}