using Domain;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace User.Domain.Entities.User
{
    public class SpecialDisease : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserProfileSpecialDisease> UserProfileSpecialDiseases { get; set; }
        public string _id { get; set; }
    }
}
