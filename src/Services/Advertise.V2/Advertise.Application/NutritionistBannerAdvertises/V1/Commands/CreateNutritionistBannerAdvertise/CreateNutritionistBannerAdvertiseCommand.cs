namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.CreateNutritionistBannerAdvertise;

public record CreateNutritionistBannerAdvertiseCommand(string Link, double Cost, double Budget,
    CreateNutritionistAdvertiseImagePerLanguageDto Image,
    CreateNutritionistAdvertiseTitleTranslationDto Title,
    CreateNutritionistAdvertiseDescriptionTranslationDto Description, List<Language> Languages,
    List<string> StateIds, int ClickCount) : IRequest;
