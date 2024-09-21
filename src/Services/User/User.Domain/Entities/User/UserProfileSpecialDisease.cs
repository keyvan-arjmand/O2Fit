using Domain;

namespace User.Domain.Entities.User
{
    public class UserProfileSpecialDisease : BaseEntity
    {
        public int SpecialDiseaseId { get; set; }
        public SpecialDisease SpecialDiseases { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfiles { get; set; }
    }
}
