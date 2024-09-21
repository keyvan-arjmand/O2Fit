using Domain;
using System;
using User.Domain.Enum;
using WebFramework.Api;

namespace User.API.Models
{
    public class UserProfileRegisterDto : BaseDto<UserProfileRegisterDto, UserProfile>
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string ImageUri { get; set; }
        public FoodHabit FoodHabit { get; set; }
        public Gender Gender { get; set; }
        public double HeightSize { get; set; }
        public DateTime BirthDate { get; set; }
        public DailyActivityRate DailyActivityRate { get; set; }
        public double TargetWeight { get; set; }
        public Nullable<double> WeightChangeRate { get; set; }
        public Nullable<int> TargetStep { get; set; }
        //public Nullable<double> UserTrackSpecificationsWaistSize { get; set; }
        // public Nullable<double> UserTrackSpecificationsWeightSize { get; set; }
        public Nullable<double> TargetThighSize { get; set; }
        public Nullable<double> TargetNeckSize { get; set; }

        //public override void CustomMappings(IMappingExpression<UserProfile, UserProfileRegisterDto> mappingExpression)
        //{
        //}

    }
}
