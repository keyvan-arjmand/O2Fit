using Domain;

namespace User.API.Models
{
    public class UserProfileViewModel
    {
        public UserProfile UserProfile { get; set; }

        public UserTrackSpecification UserTrackSpecification { get; set; }

        public DeviceInformationViewModel DeviceInformationViewModel { get; set; }


    }
}
