using Social.Domain.Enum;
using SocialMessaging.Domain.Entities.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace SocialMessaging.API.Models
{
    public class AdminContactUsMessageDTO:BaseDto<AdminContactUsMessageDTO, ContactUsMessage>
    {
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public int AdminId { get; set; }
        public bool ToAdmin { get; set; }
        public bool IsGeneral { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string ImageUri { get; set; }
        public string Url { get; set; }
        public bool IsForce { get; set; }
        public Classification Classification { get; set; }
        public bool IsReadAdmin { get; set; }
        public int ReplyToMessage { get; set; }
        public bool CanReply { get; set; }
    }
}
