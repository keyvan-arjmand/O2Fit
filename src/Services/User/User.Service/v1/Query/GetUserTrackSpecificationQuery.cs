using System;
using System.Collections.Generic;
using Domain;
using MediatR;

namespace User.Service.v1.Query
{
    public class GetUserTrackSpecificationQuery : IRequest<UserProfileTrack>
    {
        public int UserId { get; set; }
    }

    public class UserProfileTrack
    {
        public UserProfile UserProfile {get; set;}
        public List<UserTrackSpecification> TrackSpecifications {get; set;}
        public DateTime TodayDate { get; set; }
        public string ForceUpdateVersions { get; set; }
    }
}