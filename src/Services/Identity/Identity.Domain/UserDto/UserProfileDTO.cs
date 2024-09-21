﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.UserDto
{
   public class UserProfileDTO
    {
        public int UserId { get; set; }
        public int CityId { get; set; }
        public string FullName { get; set; }
        public string ImageUri { get; set; }
        public Nullable<DateTime> PkExpireDate { get; set; }
        public DateTime TodayDate { get; set; }
        public Nullable<double> WeightChangeRate { get; set; }
        public int FoodHabit { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public Nullable<int> WeightTimeRange { get; set; }
        public Nullable<int> TargetStep { get; set; }
        public double TargetWeight { get; set; }
        public Nullable<double> TargetBust { get; set; }
        public Nullable<double> TargetArm { get; set; }
        public Nullable<double> TargetWaist { get; set; }
        public Nullable<double> TargetHighHip { get; set; }
        public Nullable<double> TargetHip { get; set; }
        public Nullable<double> TargetShoulder { get; set; }
        public Nullable<double> TargetThighSize { get; set; }
        public Nullable<double> TargetNeckSize { get; set; }
        public Nullable<double> TargetWrist { get; set; }
        public Nullable<double> TargetWater { get; set; }
        public int DailyActivityRate { get; set; }
        public double HeightSize { get; set; }
        public string ReferreralCount { get; set; }
        public string BounsCount { get; set; }
        public bool FirstPay { get; set; }
        public double Wallet { get; set; }
        public string TargetNutrient { get; set; }
    }
}
