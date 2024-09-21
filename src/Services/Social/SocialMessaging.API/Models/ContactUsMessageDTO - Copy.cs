using Social.Domain.Enum;
using SocialMessaging.Domain.Entities.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace SocialMessaging.API.Models
{
    public class ContactUsMessageDTO:BaseDto<ContactUsMessageDTO, ContactUsMessage>
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public int ReplyToMessage { get; set; }
        public Classification Classification { get; set; }
    }
}
