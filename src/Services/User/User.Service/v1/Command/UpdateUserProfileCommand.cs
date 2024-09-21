using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Enum;

namespace User.Service.v1.Command
{
    public class UpdateUserProfileCommand : IRequest<UserProfile>
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public Nullable<double> HeightSize { get; set; }
        public Nullable<int> FoodHabit { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }
        public Nullable<int> DailyActivityRate { get; set; }

    }
}
