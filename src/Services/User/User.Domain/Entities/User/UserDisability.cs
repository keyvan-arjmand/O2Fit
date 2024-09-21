using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using User.Domain.Enum;

namespace Domain
{
    public class UserDisability : BaseEntity
    {
        public int UserId { get; set; }
        public Disability Disability { get; set; }
    }
}
