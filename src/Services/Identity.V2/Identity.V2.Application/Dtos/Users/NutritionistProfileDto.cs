namespace Identity.V2.Application.Dtos.Users;

public class NutritionistProfileDto : IDto
{
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
    public List<string> DietTypesIds { get; set; } = new();
    public List<string> SpecialDiseaseIds { get; set; } = new();
    public bool HasOffice { get; set; }
    public string? OfficeAddress { get; set; }
    public string? OfficePhoneNumber { get; set; }
    public string? OfficeImage1Name { get; set; }
    public string? OfficeImage2Name { get; set; }
    public string? OfficeImage3Name { get; set; }
    public string? OfficeImage4Name { get; set; }
    public CoordinatesDto Coordinates { get; set; } = null!;
}