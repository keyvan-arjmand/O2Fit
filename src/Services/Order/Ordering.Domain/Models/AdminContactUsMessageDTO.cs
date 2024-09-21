using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Ordering.Domain.Models
{
    public class AdminContactUsMessageDTO
    {
        public int Id { get; set; }
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
    public enum Classification
    {
        [Display(Name = "مالی")]
        Finance = 1,

        [Display(Name = "مدیریت")]
        Admin = 2,

        [Display(Name = "منابع انسانی")]
        HumanResource = 3,

        [Display(Name = "پشتیبانی")]
        TechnicalSupport = 4,

        [Display(Name = "پیام وب سایت")]
        WebMessage = 5
    }
}
