namespace Identity.V2.Application.NutritionistProfiles.V1.Commands.UpdateNutritionistProfile;

public class UpdateNutritionistProfileCommandHandler : IRequestHandler<UpdateNutritionistProfileCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;

    public UpdateNutritionistProfileCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task Handle(UpdateNutritionistProfileCommand request, CancellationToken cancellationToken)
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
            if (!string.IsNullOrEmpty(user.NutritionistProfile.LicenseImageName))
            {
                _fileService.RemoveImage(user.NutritionistProfile.LicenseImageName,
                    PathAddressesConstants.NutritionistLicenseImage);    
            }

            var licenseImageName = _fileService.AddImage(request.LicenseImage,
                PathAddressesConstants.NutritionistLicenseImage, Guid.NewGuid().ToString());
            user.NutritionistProfile.LicenseImageName = licenseImageName;
        }

        if (!string.IsNullOrEmpty(user.NutritionistProfile.LicenseImageName) && string.IsNullOrEmpty(request.LicenseImage) && string.IsNullOrEmpty(request.LicenseImageUrl))
        {
            _fileService.RemoveImage(user.NutritionistProfile.LicenseImageName,
                PathAddressesConstants.NutritionistLicenseImage);    
        }
        
        if (!string.IsNullOrEmpty(request.OtherDocumentsImage))
        {
            if (!string.IsNullOrEmpty(user.NutritionistProfile.OtherDocumentsImageName))
            {
                _fileService.RemoveImage(user.NutritionistProfile.OtherDocumentsImageName,
                    PathAddressesConstants.NutritionistOtherDocuments);
            }

            var otherDocumentImage = _fileService.AddImage(request.OtherDocumentsImage,
                PathAddressesConstants.NutritionistOtherDocuments, Guid.NewGuid().ToString());
            user.NutritionistProfile.OtherDocumentsImageName = otherDocumentImage;
        }

        
        if (!string.IsNullOrEmpty(user.NutritionistProfile.OtherDocumentsImageName) && string.IsNullOrEmpty(request.OtherDocumentsImage) && string.IsNullOrEmpty(request.OtherDocumentsImage))
        {
            _fileService.RemoveImage(user.NutritionistProfile.OtherDocumentsImageName,
                PathAddressesConstants.NutritionistOtherDocuments);    
        }
        
        if (!string.IsNullOrEmpty(request.OfficeImage1))
        {
            if (!string.IsNullOrEmpty(user.NutritionistProfile.OfficeImage1Name))
            {
                _fileService.RemoveImage(user.NutritionistProfile.OfficeImage1Name,
                    PathAddressesConstants.NutritionistOfficeImage1);
            }

            var officeImage1 = _fileService.AddImage(request.OfficeImage1,
                PathAddressesConstants.NutritionistOfficeImage1, Guid.NewGuid().ToString());
            user.NutritionistProfile.OfficeImage1Name = officeImage1;
        }

        if (!string.IsNullOrEmpty(user.NutritionistProfile.OfficeImage1Name) && string.IsNullOrEmpty(request.OfficeImage1) && string.IsNullOrEmpty(request.OfficeImage1Url))
        {
            _fileService.RemoveImage(user.NutritionistProfile.OfficeImage1Name,
                PathAddressesConstants.NutritionistOfficeImage1); 
        }
        
        if (!string.IsNullOrEmpty(request.OfficeImage2))
        {
            if (!string.IsNullOrEmpty(user.NutritionistProfile.OfficeImage2Name))
            {
                _fileService.RemoveImage(user.NutritionistProfile.OfficeImage2Name,
                    PathAddressesConstants.NutritionistOfficeImage2);
            }

            var officeImage2 = _fileService.AddImage(request.OfficeImage2,
                PathAddressesConstants.NutritionistOfficeImage2, Guid.NewGuid().ToString());
            user.NutritionistProfile.OfficeImage2Name = officeImage2;
        }

        if (!string.IsNullOrEmpty(user.NutritionistProfile.OfficeImage2Name) && string.IsNullOrEmpty(request.OfficeImage2) && string.IsNullOrEmpty(request.OfficeImage2Url))
        {
            _fileService.RemoveImage(user.NutritionistProfile.OfficeImage2Name,
                PathAddressesConstants.NutritionistOfficeImage2);
        }
        
        if (!string.IsNullOrEmpty(request.OfficeImage3))
        {
            if (!string.IsNullOrEmpty(user.NutritionistProfile.OfficeImage3Name))
            {
                _fileService.RemoveImage(user.NutritionistProfile.OfficeImage3Name,
                    PathAddressesConstants.NutritionistOfficeImage3);
            }

            var officeImage3 = _fileService.AddImage(request.OfficeImage3,
                PathAddressesConstants.NutritionistOfficeImage3, Guid.NewGuid().ToString());
            user.NutritionistProfile.OfficeImage3Name = officeImage3;
        }

        if (!string.IsNullOrEmpty(user.NutritionistProfile.OfficeImage3Name) && string.IsNullOrEmpty(request.OfficeImage3) && string.IsNullOrEmpty(request.OfficeImage3Url))
        {
            _fileService.RemoveImage(user.NutritionistProfile.OfficeImage3Name,
                PathAddressesConstants.NutritionistOfficeImage3);
        }

        if (!string.IsNullOrEmpty(request.OfficeImage4))
        {
            if (!string.IsNullOrEmpty(user.NutritionistProfile.OfficeImage4Name))
            {
                _fileService.RemoveImage(user.NutritionistProfile.OfficeImage4Name,
                    PathAddressesConstants.NutritionistOfficeImage4);
            }

            var officeImage4 = _fileService.AddImage(request.OfficeImage4,
                PathAddressesConstants.NutritionistOfficeImage4, Guid.NewGuid().ToString());
            user.NutritionistProfile.OfficeImage4Name = officeImage4;
        }
        
        if (!string.IsNullOrEmpty(user.NutritionistProfile.OfficeImage4Name) && string.IsNullOrEmpty(request.OfficeImage4) && string.IsNullOrEmpty(request.OfficeImage4Url))
        {
            _fileService.RemoveImage(user.NutritionistProfile.OfficeImage4Name,
                PathAddressesConstants.NutritionistOfficeImage4);
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