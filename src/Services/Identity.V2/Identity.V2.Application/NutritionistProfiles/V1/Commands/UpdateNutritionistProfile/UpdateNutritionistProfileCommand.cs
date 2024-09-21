namespace Identity.V2.Application.NutritionistProfiles.V1.Commands.UpdateNutritionistProfile;

public record UpdateNutritionistProfileCommand(string UserId, Gender Gender, string ProfileImage, string ProfileImageUrl, string FirstName, string LastName, ScientificDegree ScientificDegree, string MedicalSystemNumber, int ExperienceYear,
    DateTime ActivityExpirationDate, string LicenseImage, string LicenseImageUrl, string? OtherDocumentsImage,string? OtherDocumentsImageUrl, string AboutTheSpecialist, CategoryOfFitnessGoal CategoryOfFitnessGoal, List<string> DietTypeIds,
    List<string>? SpecialDiseaseIds, bool HasOffice, string? OfficeAddress, string? OfficePhoneNumber, string? OfficeImage1,string? OfficeImage1Url, string? OfficeImage2, string? OfficeImage2Url,
    string? OfficeImage3, string? OfficeImage3Url, string? OfficeImage4, string? OfficeImage4Url, CoordinatesDto? Coordinates) : IRequest;