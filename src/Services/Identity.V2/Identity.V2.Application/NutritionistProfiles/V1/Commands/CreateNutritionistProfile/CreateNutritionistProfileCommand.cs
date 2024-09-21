namespace Identity.V2.Application.NutritionistProfiles.V1.Commands.CreateNutritionistProfile;

public record CreateNutritionistProfileCommand(string UserId, Gender Gender, string ProfileImage, string FirstName, string LastName, ScientificDegree ScientificDegree, string MedicalSystemNumber, int ExperienceYear,
    DateTime ActivityExpirationDate, string LicenseImage, string? OtherDocumentsImage, string AboutTheSpecialist, CategoryOfFitnessGoal CategoryOfFitnessGoal, List<string> DietTypeIds,
    List<string>? SpecialDiseaseIds, bool HasOffice, string? OfficeAddress, string? OfficePhoneNumber, string? OfficeImage1, string? OfficeImage2, string? OfficeImage3,
    string? OfficeImage4, CoordinatesDto? Coordinates) : IRequest;