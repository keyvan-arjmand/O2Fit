using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace User.Domain.Entities.User
{
    public class Note:BaseEntity
    {
        public int UserId { get; set; }
        [StringLength(500, ErrorMessage = "Maximum Note Characters Is Less Than 500")]
        public string Text { get; set; }
        public DateTime InsertDate { get; set; }
        public string _id { get; set; }
    }
}
