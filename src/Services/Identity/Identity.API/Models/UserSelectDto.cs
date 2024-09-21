using Domain;
using Identity.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Identity.API.Models
{
    public class UserSelectDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Language Language { get; set; }
        public DayOfWeek StartOfWeek { get; set; }
        public string ImageUri { get; set; }
        public int CountryId { get; set; }
        public string ReferreralCode { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
