using System;
using System.ComponentModel.DataAnnotations.Schema;
using Blogging.Domain.Common;
using Blogging.Domain.Enum;

namespace Blogging.Domain.Entities.Blogs
{
    public class Blog : BaseEntity
    {
        public int TitleId { get; set; }
        [ForeignKey("TitleId")] public Translation.Translation Title { get; set; }
        public int DescriptionId { get; set; }
        [ForeignKey("DescriptionId")] public Translation.Translation Description { get; set; }
        public int ShortDescriptionId { get; set; }
        [ForeignKey("ShortDescriptionId")] public Translation.Translation ShortDescription { get; set; }
        public BlogStatus Status { get; set; }
        public string ImageName { get; set; }
        public string AltImage { get; set; }
        public string ThumbName { get; set; }
        public string AltThumb { get; set; }
        public string BannerName { get; set; }
        public string AltBanner { get; set; }
        public string KeyWords { get; set; }
        public long Like { get; set; }
        public long View { get; set; }
        public int SubCategoryId { get; set; }
        public bool FirstPagePost { get; set; }
        public DateTime InsertDate { get; set; }
        [ForeignKey("SubCategoryId")] public SubCategory SubCategory { get; set; }
    }
}