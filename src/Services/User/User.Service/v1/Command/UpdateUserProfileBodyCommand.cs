using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Service.v1.Command
{
    public class UpdateUserProfileBodyCommand : IRequest<UserProfile>
    {
        public int UserId { get; set; }
        public Nullable<double> TargetBust { get; set; }
        public Nullable<double> TargetArm { get; set; }
        public Nullable<double> TargetWaist { get; set; }
        public Nullable<double> TargetHighHip { get; set; }
        public Nullable<double> TargetHip { get; set; }
        public Nullable<double> TargetShoulder { get; set; }
        public Nullable<double> TargetWrist { get; set; }
        public Nullable<double> TargetThighSize { get; set; }
        public Nullable<double> TargetNeckSize { get; set; }
        public double HeightSize { get; set; }
    }
}
