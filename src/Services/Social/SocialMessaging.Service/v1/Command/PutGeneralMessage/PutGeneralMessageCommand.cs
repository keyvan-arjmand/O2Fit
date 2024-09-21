using MediatR;
using SocialMessaging.Domain.Entities.ContactUs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMessaging.Service.v1.Command.PutGeneralMessage
{
   public class PutGeneralMessageCommand:IRequest<ContactUsMessage>
    {
        public ContactUsMessage generalMessage { get; set; }
    }
}
