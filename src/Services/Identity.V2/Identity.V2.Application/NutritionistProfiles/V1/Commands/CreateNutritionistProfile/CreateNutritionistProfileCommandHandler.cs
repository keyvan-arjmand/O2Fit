namespace Identity.V2.Application.NutritionistProfiles.V1.Commands.CreateNutritionistProfile;

public class CreateNutritionistProfileCommandHandler : IRequestHandler<CreateNutritionistProfileCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;
    public CreateNutritionistProfileCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task Handle(CreateNutritionistProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>()
            .GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId);

        user.NutritionistProfile.Gender = request.Gender;
        user.NutritionistProfile.Coordinates = request.Coordinates!.ToEntity<Coordinates>();
        user.NutritionistProfile.DietTypesIds = request.DietTypeIds.Select(ObjectId.Parse).ToList();
        user.NutritionistProfile.ExperienceYear = request.ExperienceYear;
        user.NutritionistProfile.FirstName = request.FirstName;
        user.NutritionistProfile.LastName = request.LastName;
        user.NutritionistProfile.HasOffice = request.HasOffice;
        user.NutritionistProfile.ScientificDegree = request.ScientificDegree;
        user.NutritionistProfile.AboutTheSpecialist = request.AboutTheSpecialist;
        user.NutritionistProfile.ActivityExpirationDate = request.ActivityExpirationDate;
        user.NutritionistProfile.MedicalSystemNumber = request.MedicalSystemNumber;
        
        user.NutritionistProfile.SpecialDiseasesIds = request.SpecialDiseaseIds.Count > 0
            ? request.SpecialDiseaseIds.Select(ObjectId.Parse).ToList()
            : new List<ObjectId>();
        user.NutritionistProfile.CategoryOfFitnessGoal = request.CategoryOfFitnessGoal;
        user.NutritionistProfile.OfficeAddress =
            !string.IsNullOrEmpty(request.OfficeAddress) ? request.OfficeAddress : null;

        user.NutritionistProfile.OfficePhoneNumber =
            !string.IsNullOrEmpty(request.OfficePhoneNumber) ? request.OfficePhoneNumber : null;
        
        if (!string.IsNullOrEmpty(request.ProfileImage))
        {
            var profileImageName = _fileService.AddImage(request.ProfileImage,
                PathAddressesConstants.NutritionistProfileImage, Guid.NewGuid().ToString());
            user.NutritionistProfile.ProfileImageName = profileImageName;
        }
        if (!string.IsNullOrEmpty(request.LicenseImage))
        {
            var licenseImageName = _fileService.AddImage(request.LicenseImage,
                PathAddressesConstants.NutritionistLicenseImage, Guid.NewGuid().ToString());
            user.NutritionistProfile.LicenseImageName = licenseImageName;
        }

        if (!string.IsNullOrEmpty(request.OtherDocumentsImage))
        {
            var otherDocumentImage = _fileService.AddImage(request.OtherDocumentsImage,
                PathAddressesConstants.NutritionistOtherDocuments, Guid.NewGuid().ToString());
            user.NutritionistProfile.OtherDocumentsImageName = otherDocumentImage;
        }

        if (!string.IsNullOrEmpty(request.OfficeImage1))
        {
            var officeImage1 = _fileService.AddImage(request.OfficeImage1,
                PathAddressesConstants.NutritionistOfficeImage1, Guid.NewGuid().ToString());
            user.NutritionistProfile.OfficeImage1Name = officeImage1;
        }
        
        if (!string.IsNullOrEmpty(request.OfficeImage2))
        {
            var officeImage2 = _fileService.AddImage(request.OfficeImage2,
                PathAddressesConstants.NutritionistOfficeImage2, Guid.NewGuid().ToString());
            user.NutritionistProfile.OfficeImage2Name = officeImage2;
        }
        
        
        if (!string.IsNullOrEmpty(request.OfficeImage3))
        {
            var officeImage3 = _fileService.AddImage(request.OfficeImage3,
                PathAddressesConstants.NutritionistOfficeImage3, Guid.NewGuid().ToString());
            user.NutritionistProfile.OfficeImage3Name = officeImage3;
        }
        
        
        if (!string.IsNullOrEmpty(request.OfficeImage4))
        {
            var officeImage4 = _fileService.AddImage(request.OfficeImage4,
                PathAddressesConstants.NutritionistOfficeImage4, Guid.NewGuid().ToString());
            user.NutritionistProfile.OfficeImage4Name = officeImage4;
        }

        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.Id == ObjectId.Parse(request.UserId), user,
            new Expression<Func<User, object>>[]
            {
                u => u.NutritionistProfile.Gender,
                u => u.NutritionistProfile.Coordinates.Lat,
                u => u.NutritionistProfile.Coordinates.Long,
                u => u.NutritionistProfile.DietTypesIds,
                u => u.NutritionistProfile.ExperienceYear,
                u => u.NutritionistProfile.FirstName,
                u => u.NutritionistProfile.LastName,
                u => u.NutritionistProfile.HasOffice,
                u => u.NutritionistProfile.ScientificDegree,
                u => u.NutritionistProfile.AboutTheSpecialist,
                u => u.NutritionistProfile.ActivityExpirationDate,
                u => u.NutritionistProfile.MedicalSystemNumber,
                u => u.NutritionistProfile.SpecialDiseasesIds,
                u => u.NutritionistProfile.CategoryOfFitnessGoal,
                u => u.NutritionistProfile.OfficeAddress,
                u => u.NutritionistProfile.OfficePhoneNumber,
                u => u.NutritionistProfile.ProfileImageName,
                u => u.NutritionistProfile.LicenseImageName,
                u => u.NutritionistProfile.OtherDocumentsImageName,
                u => u.NutritionistProfile.OfficeImage1Name,
                u => u.NutritionistProfile.OfficeImage2Name,
                u => u.NutritionistProfile.OfficeImage3Name,
                u => u.NutritionistProfile.OfficeImage4Name
            }, null, cancellationToken);
    }
}