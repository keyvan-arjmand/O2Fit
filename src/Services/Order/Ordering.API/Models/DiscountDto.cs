using Ordering.Domain.Entities.Discount;
using Ordering.Domain.Entities.Translation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Ordering.API.Models
{
    public class DiscountDto : BaseDto<DiscountDto, Discount>
    {
        public string Code { get; set; }
        public int Percent { get; set; }
        public Nullable<int> UsableCount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
        public Nullable<int> UserId { get; set; }
        public bool IsActive { get; set; }
        public Translation Translation { get; set; }
    }
}
