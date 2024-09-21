using System;
using Domain;
using MediatR;

namespace User.Service.v1.Command
{
    public class CreateUserProfileCommand : IRequest<UserProfile>
    {
        public UserProfile UserProfile {get; set;}
       /// public Nullable<double> UserTrackSpecificationsWaistSize { get; set; }
       // public Nullable<double> UserTrackSpecificationsWeightSize { get; set; }
    }
}