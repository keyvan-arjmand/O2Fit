using System;
using Domain;
using MediatR;
using User.Domain.Enum;

namespace User.Service.v1.Command
{
    public class UpdateUserProfileTargetCommand : IRequest<UserProfile>
    {
        public int UserId { get; set; }
        public double TargetWeight { get; set; }
        public Nullable<double> WeightChangeRate { get; set; }
        public Nullable<int> TargetStep { get; set; }
        public DailyActivityRate DailyActivityRate { get; set; }
    }
}