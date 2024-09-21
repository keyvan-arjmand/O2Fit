using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMessaging.Domain.Entities.ContactUs
{
    public class ContactUsMessageReader : BaseEntity
    {
        public int UserId { get; set; }
        public int MessageId { get; set; }
    }
}
