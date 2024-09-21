using Domain;
using Identity.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models
{
    public class UserDto
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public int CountryId { get; set; }

        public Language LanguageId { get; set; }

        public string ReferreralInviter { get; set; }

        public DayOfWeek StartOfWeek { get; set; }
    }
}
