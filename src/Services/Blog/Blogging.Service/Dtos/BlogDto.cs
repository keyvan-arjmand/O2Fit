using System;
using System.Collections.Generic;
using Blogging.Domain.Enum;

namespace Blogging.Service.Dtos
{
    public class BlogDto
    {
        public int Id { get; set; }
        public TranslationDto Title { get; set; }
        public TranslationDto Description { get; set; }
        public TranslationDto ShortDescription { get; set; }
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
        public TranslationDto SubCategory { get; set; }
        public bool FirstPagePost { get; set; }
        public string InsertDate { get; set; }
        public DateTime EnDate { get; set; }
    }
}