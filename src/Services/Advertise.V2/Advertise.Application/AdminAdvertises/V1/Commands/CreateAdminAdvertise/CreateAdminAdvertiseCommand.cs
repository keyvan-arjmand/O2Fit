namespace Advertise.Application.AdminAdvertises.V1.Commands.CreateAdminAdvertise;

public record CreateAdminAdvertiseCommand(string Link, double Cost, double Budget, CreateAdminAdvertiseImagePerLanguageDto Images, CreateAdminAdvertiseTitleTranslationDto Title,
        CreateAdminAdvertiseDescriptionTranslationDto Description, List<Language> Languages,  int ClickCount , List<string> StateIds) : IRequest;