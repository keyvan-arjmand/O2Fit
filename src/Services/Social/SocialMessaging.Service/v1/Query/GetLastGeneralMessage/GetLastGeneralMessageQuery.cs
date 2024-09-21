using MediatR;
using SocialMessaging.Domain.Entities.ContactUs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMessaging.Service.v1.Query.GetLastGeneralMessage
{
   public class GetLastGeneralMessageQuery:IRequest<List<ContactUsMessage>>
    {
        public int lastMessageId { get; set; }
        public string language { get; set; }
    }
}
