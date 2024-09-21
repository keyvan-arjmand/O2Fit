using MediatR;
using SocialMessaging.Domain.Entities.ContactUs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMessaging.Service.v1.Query
{
   public class GetMarketMessageQuery:IRequest<ContactUsMessage>
    {
        public string LanguageName { get; set; }
    }
}
