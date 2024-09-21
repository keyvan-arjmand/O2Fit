using System;
using Domain;

namespace SocialMessaging.Domain.Entities.ContactUs
{
    public class BlockUsers : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
