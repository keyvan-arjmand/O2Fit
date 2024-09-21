using Social.Domain.Enum;
using System;

namespace SocialMessaging.API.Models
{
    public class ContactUsMessageViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public string Message { get; set; }
        public string? ImageUri { get; set; }
        public bool IsRead { get; set; }
        public int AdminId { get; set; }
        public bool IsGeneral { get; set; }
        public string? Title { get; set; }
        public Classification Classification { get; set; }
        public string UserName { get; set; }
        public string? FullName { get; set; }

    }
}
