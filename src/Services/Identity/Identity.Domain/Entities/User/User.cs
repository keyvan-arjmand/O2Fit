using Identity.Domain.Entities.Country;
using Identity.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class User : IdentityUser<int>, IEntity
    {
        public Language Language { get; set; }
        public DateTimeOffset? LastSeenTime { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime RegisterDate { get; set; }
        public string ImageUri { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        public bool IsActive { get; set; }
        public string ReferreralCode { get; set; }
        public string ReferreralInviter { get; set; }
        public int ReferreralCountBuy { get; set; }
        [MaxLength(20)]
        public string ConfirmCode { get; set; }

        public DateTime ConfirmCodeExpireTime { get; set; }
        public DayOfWeek StartOfWeek { get; set; }
        public int? NutritionDietCategoryId { get; set; }
        public int? NormalDietCategoryId { get; set; }
        public bool IsSendToNewSystem { get; set; }

    }
}
