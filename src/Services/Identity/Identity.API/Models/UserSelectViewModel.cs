using System;
using Identity.Domain.Enum;
using Service;

namespace Identity.API.Models
{
    public class UserSelectViewModel
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
        public string ReferreralInviter { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public AccessToken Token { get; set; }
    }
}
