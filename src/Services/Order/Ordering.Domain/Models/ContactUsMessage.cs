using Domain;
using Domain.Enum;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Models
{
    public class ContactUsMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public string Message { get; set; }
        public string ImageUri { get; set; }
        public string Url { get; set; }
        public bool IsRead { get; set; }
        public int AdminId { get; set; }
        public bool ToAdmin { get; set; }
        public bool IsGeneral { get; set; }
        public string Title { get; set; }
        public bool IsForce { get; set; }
        public Classification Classification { get; set; }
        public bool IsReadAdmin { get; set; }
        public int ReplyToMessage { get; set; }
        public bool CanReply { get; set; }
        public string Language{ get; set; }
    }
}
