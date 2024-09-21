namespace Identity.V2.Domain.Aggregates.UserAggregate;

public class NutritionistProfile : BaseEntity
{
    public NutritionistProfile()
    {
    }

    public NutritionistProfile(string id)
    {
        Id = id;
    }
    public Gender Gender { get; set; }
    public string ProfileImageName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ScientificDegree ScientificDegree { get; set; }
    public string MedicalSystemNumber { get; set; } = string.Empty;
    public int ExperienceYear { get; set; }
    public DateTime ActivityExpirationDate { get; set; }
    public string LicenseImageName { get; set; } = string.Empty;
    public string? OtherDocumentsImageName { get; set; } 
    public string AboutTheSpecialist { get; set; } = string.Empty;
    public CategoryOfFitnessGoal CategoryOfFitnessGoal { get; set; }
    public List<ObjectId> DietTypesIds { get; set; } = new();
    public List<ObjectId> SpecialDiseasesIds { get; set; } = new();
    public bool HasOffice { get; set; }
    public string? OfficeAddress { get; set; }
    public string? OfficePhoneNumber { get; set; }
    public string? OfficeImage1Name { get; set; }
    public string? OfficeImage2Name { get; set; }
    public string? OfficeImage3Name { get; set; }
    public string? OfficeImage4Name { get; set; }
    public Coordinates Coordinates { get; set; } = new();

}