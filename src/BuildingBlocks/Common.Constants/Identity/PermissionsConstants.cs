namespace Common.Constants.Identity
{
    public static class PermissionsConstants
    {
        public const string Permissions = "Permissions";

        #region Identity
        public const string FullAccess = "Permissions.Identity.FullAccess";
        public const string GetAllCountry = "Permissions.Identity.GetAllCountry";

        #endregion
        #region PermissionCategory

        public const string GetPermissionCategoriesAllPaginated =
            O2fitIdentityConstants.PermissionWithDot + "GetPermissionCategoriesAllPaginated";

        public const string GetCategoryPermissionByIdWithPermissions =
            O2fitIdentityConstants.PermissionWithDot + "GetCategoryPermissionByIdWithPermissions";

        public const string GetAllCategoryPermissionsWithPermissions =
            O2fitIdentityConstants.PermissionWithDot + "GetAllCategoryPermissionsWithPermissions";

        public const string CreateCategoryPermission =
            O2fitIdentityConstants.PermissionWithDot + "CreateCategoryPermission";

        public const string UpdateCategoryPermission =
            O2fitIdentityConstants.PermissionWithDot + "UpdateCategoryPermission";

        public const string DeleteCategoryPermission =
            O2fitIdentityConstants.PermissionWithDot + "DeleteCategoryPermission";

        #endregion

        #region Permissions

        public const string CreatePermission = O2fitIdentityConstants.PermissionWithDot + "CreatePermission";
        public const string UpdatePermission = O2fitIdentityConstants.PermissionWithDot + "UpdatePermission";
        public const string DeletePermission = O2fitIdentityConstants.PermissionWithDot + "DeletePermission";

        #endregion

        #region Roles

        public const string GetAllRoles = O2fitIdentityConstants.PermissionWithDot + "GetAllRoles";
        public const string GetRoleById = O2fitIdentityConstants.PermissionWithDot + "GetRoleById";
        public const string CreateRole = O2fitIdentityConstants.PermissionWithDot + "CreateRole";
        public const string AddPermissionToRole = O2fitIdentityConstants.PermissionWithDot + "AddPermissionToRole";
        public const string UpdateRole = O2fitIdentityConstants.PermissionWithDot + "UpdateRole";
        public const string DeleteRole = O2fitIdentityConstants.PermissionWithDot + "DeleteRole";

        public const string AddSinglePermissionToRole =
            O2fitIdentityConstants.PermissionWithDot + "AddSinglePermissionToRole";

        #endregion

        #region Users

        public const string RegisterUser = O2fitIdentityConstants.PermissionWithDot + "RegisterUser";
        public const string AssignRoleToUser = O2fitIdentityConstants.PermissionWithDot + "AssignRoleToUser";
        public const string AssignReferralCode = O2fitIdentityConstants.PermissionWithDot + "AssignReferralCode";
        public const string GetUserData = O2fitIdentityConstants.PermissionWithDot + "GetUserData";

        public const string UpdateSecurityStampByUsername =
            O2fitIdentityConstants.PermissionWithDot + "UpdateSecurityStampByUsername";

        public const string UpdateSecurityStampByUserId =
            O2fitIdentityConstants.PermissionWithDot + "UpdateSecurityStampByUserId";

        public const string AddPasswordForUser = O2fitIdentityConstants.PermissionWithDot + "AddPasswordForUser";
        public const string ChangeIsComplete = O2fitIdentityConstants.PermissionWithDot + "ChangeIsComplete";

        public const string UpdateStateIdAndCityId =
            O2fitIdentityConstants.PermissionWithDot + "UpdateStateIdAndCityId";

        public const string GetProfileNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "GetProfileNutritionist";

        public const string ChangeUserStatusByUserId =
            O2fitIdentityConstants.PermissionWithDot + "ChangeUserStatusByUserId";

        #endregion

        #region Countries

        public const string GetAllCountries = O2fitIdentityConstants.PermissionWithDot + "GetAllCountries";
        public const string UpdateUnCountryInfo = O2fitIdentityConstants.PermissionWithDot + "UpdateUnCountryInfo";
        public const string AddStateToCountry = O2fitIdentityConstants.PermissionWithDot + "AddStateToCountry";
        public const string AddCityToState = O2fitIdentityConstants.PermissionWithDot + "AddCityToState";
        public const string UpdateNameOfState = O2fitIdentityConstants.PermissionWithDot + "UpdateNameOfState";
        public const string UpdateNameOfCity = O2fitIdentityConstants.PermissionWithDot + "UpdateNameOfCity";


        public const string SoftDeleteCountryByOldSystemId =
            O2fitIdentityConstants.PermissionWithDot + "SoftDeleteCountryByOldSystemId";

        #endregion


        #region UserProfile

        public const string CreateUserProfile = O2fitIdentityConstants.PermissionWithDot + "CreateUserProfile";
        public const string UpdateFastingMode = O2fitIdentityConstants.PermissionWithDot + "UpdateFastingMode";
        public const string UpdateTarget = O2fitIdentityConstants.PermissionWithDot + "UpdateTarget";
        public const string UpdateTargetNutrient = O2fitIdentityConstants.PermissionWithDot + "UpdateTargetNutrient";
        public const string UpdateProfile = O2fitIdentityConstants.PermissionWithDot + "UpdateProfile";
        public const string GetUserNutrient = O2fitIdentityConstants.PermissionWithDot + "GetUserNutrient";

        #endregion

        #region DeviceInfo

        public const string AddDeviceInfo = O2fitIdentityConstants.PermissionWithDot + "AddDeviceInfo";

        #endregion

        #region SpecialDisease

        public const string GetAllSpecialDiseases = O2fitIdentityConstants.PermissionWithDot + "GetAllSpecialDiseases";
        public const string GetSpecialDiseaseById = O2fitIdentityConstants.PermissionWithDot + "GetSpecialDiseaseById";
        public const string CreateSpecialDisease = O2fitIdentityConstants.PermissionWithDot + "CreateSpecialDisease";
        public const string UpdateSpecialDisease = O2fitIdentityConstants.PermissionWithDot + "UpdateSpecialDisease";

        public const string SoftDeleteSpecialDiseaseById =
            O2fitIdentityConstants.PermissionWithDot + "SoftDeleteSpecialDiseaseById";

        #endregion

        #region NutritionistProfile

        public const string CreateNutritionistProfile =
            O2fitIdentityConstants.PermissionWithDot + "CreateNutritionistProfile";

        public const string UpdateNutritionistProfile =
            O2fitIdentityConstants.PermissionWithDot + "UpdateNutritionistProfile";

        #endregion

        public const string UnAuthorizePermission = O2fitIdentityConstants.PermissionWithDot + "UnAuthorizePermission";

        #region Currency

        public const string GetAllCurrency = O2fitIdentityConstants.PermissionWithDot + "GetAllCurrency";
        public const string GetByIdCurrency = O2fitIdentityConstants.PermissionWithDot + "GetByIdCurrency";
        public const string GetByNameCurrency = O2fitIdentityConstants.PermissionWithDot + "GetByNameCurrency";
        public const string PostCurrency = O2fitIdentityConstants.PermissionWithDot + "PostCurrency";
        public const string PutCurrency = O2fitIdentityConstants.PermissionWithDot + "PutCurrency";
        public const string DeleteCurrency = O2fitIdentityConstants.PermissionWithDot + "DeleteCurrency";

        #endregion

        #region Payment

        public const string UpdateTransactionMyket =
            O2fitIdentityConstants.PermissionWithDot + "UpdateTransactionMyket";

        public const string UpdateTransactionCafeBazar =
            O2fitIdentityConstants.PermissionWithDot + "UpdateTransactionCafeBazar";

        public const string CreateTransaction = O2fitIdentityConstants.PermissionWithDot + "CreateTransaction";

        public const string CreateTransactionWallet =
            O2fitIdentityConstants.PermissionWithDot + "CreateTransactionWallet";

        #endregion

        #region Track

        public const string PostUserTrackSpecification =
            O2fitIdentityConstants.PermissionWithDot + "PostUserTrackSpecification";

        public const string GetLastUserSpecificationTrack =
            O2fitIdentityConstants.PermissionWithDot + "GetLastUserSpecificationTrack";

        #endregion

        #region DiscountCode

        //admin
        public const string GetAllDiscountAdmin = O2fitIdentityConstants.PermissionWithDot + "GetAllDiscountAdmin";
        public const string GetByIdDiscountAdmin = O2fitIdentityConstants.PermissionWithDot + "GetByIdDiscountAdmin";

        public const string GetByCodeDiscountAdmin =
            O2fitIdentityConstants.PermissionWithDot + "GetByCodeDiscountAdmin";

        public const string PostDiscountAdmin = O2fitIdentityConstants.PermissionWithDot + "PostDiscountAdmin";
        public const string ActiveDiscountAdmin = O2fitIdentityConstants.PermissionWithDot + "ActiveDiscountAdmin";

        public const string PostWithGeneratorDiscountAdmin =
            O2fitIdentityConstants.PermissionWithDot + "PostWithGeneratorDiscountAdmin";

        public const string ApplyDiscountCode = O2fitIdentityConstants.PermissionWithDot + "ApplyDiscountCode";

        public const string PatchDiscountDiscountAdmin =
            O2fitIdentityConstants.PermissionWithDot + "PatchDiscountDiscountAdmin";

        public const string PatchDiscountTranslateAdmin =
            O2fitIdentityConstants.PermissionWithDot + "PatchDiscountTranslateAdmin";

        public const string DeleteDiscountAdmin = O2fitIdentityConstants.PermissionWithDot + "DeleteDiscountAdmin";

        //Nutritionist
        public const string GetAllNutritionist = O2fitIdentityConstants.PermissionWithDot + "GetAllNutritionist";
        public const string GetByIdNutritionist = O2fitIdentityConstants.PermissionWithDot + "GetByIdNutritionist";
        public const string GetByCodeNutritionist = O2fitIdentityConstants.PermissionWithDot + "GetByCodeNutritionist";

        #endregion

        #region DiscountPackage

        //admin
        public const string GetAllDiscountPackageAdmin =
            O2fitIdentityConstants.PermissionWithDot + "GetAllPackageAdmin";

        public const string GetByIdDiscountPackageAdmin =
            O2fitIdentityConstants.PermissionWithDot + "GetByIdPackageAdmin";

        public const string PostDiscountPackageAdmin = O2fitIdentityConstants.PermissionWithDot + "PostPackageAdmin";
        public const string PatchDiscountPackageAdmin = O2fitIdentityConstants.PermissionWithDot + "PatchPackageAdmin";

        public const string DeleteDiscountPackageAdmin =
            O2fitIdentityConstants.PermissionWithDot + "DeletePackageAdmin";

        //Nutritionist
        public const string GetAllDiscountPackageNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "GetAllPackageNutritionist";

        public const string GetByIdDiscountPackageNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "GetByIdPackageNutritionist";

        public const string PostDiscountPackageNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "PostPackageNutritionist";

        public const string PatchDiscountPackageNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "PatchPackageNutritionist";

        public const string DeleteDiscountPackageNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "DeletePackageNutritionist";

        #endregion

        #region Package

        //admin
        public const string GetAllPackageAdmin = O2fitIdentityConstants.PermissionWithDot + "GetAllPackageAdmin";

        public const string GetAllPackagePaginationAdmin =
            O2fitIdentityConstants.PermissionWithDot + "GetAllPackageAdmin";

        public const string GetByIdPackageAdmin = O2fitIdentityConstants.PermissionWithDot + "GetByIdPackageAdmin";
        public const string PostPackageAdmin = O2fitIdentityConstants.PermissionWithDot + "PostPackageAdmin";
        public const string PutPackageAdmin = O2fitIdentityConstants.PermissionWithDot + "PutPackageAdmin";

        public const string PatchTranslationNamePackageAdmin =
            O2fitIdentityConstants.PermissionWithDot + "PatchTranslationNamePackageAdmin";

        public const string PatchTranslationDescriptionPackageAdmin =
            O2fitIdentityConstants.PermissionWithDot + "PatchTranslationDescriptionPackageAdmin";

        public const string DeletePackageAdmin = O2fitIdentityConstants.PermissionWithDot + "DeletePackageAdmin";

        //Nutritionist
        public const string GetAllPackageNutritionist = O2fitIdentityConstants.PermissionWithDot + "GetAllPackageAdmin";

        public const string GetAllPackagePaginationNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "GetAllPackagePaginationNutritionist";

        public const string GetByIdPackageNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "GetByIdPackageNutritionist";

        public const string PostPackageNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "PostPackageNutritionist";

        public const string PutPackageNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "PutPackageNutritionist";

        public const string PatchTranslationNamePackageNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "PatchTranslationNamePackageNutritionist";

        public const string PatchTranslationDescriptionPackageNutritionist = O2fitIdentityConstants.PermissionWithDot +
                                                                             "PatchTranslationDescriptionPackageNutritionist";

        public const string DeletePackageNutritionist =
            O2fitIdentityConstants.PermissionWithDot + "DeletePackageNutritionist";

        #endregion

        #region Wallet

        public const string GetWalletById = O2fitIdentityConstants.PermissionWithDot + "GetWalletById";
        public const string PaymentWithWallet = O2fitIdentityConstants.PermissionWithDot + "PaymentWithWallet";
        public const string CreateWallet = O2fitIdentityConstants.PermissionWithDot + "CreateWallet";

        #endregion

        #region Food

        public const string GetAllMeasureUnits = O2fitIdentityConstants.PermissionWithDot + "GetAllMeasureUnits";
        public const string GetMeasureUnitById = O2fitIdentityConstants.PermissionWithDot + "GetMeasureUnitById";
        public const string CreateMeasureUnit = O2fitIdentityConstants.PermissionWithDot + "CreateMeasureUnit";
        public const string UpdateMeasureUnit = O2fitIdentityConstants.PermissionWithDot + "UpdateMeasureUnit";

        public const string GetByIdAdminIngredient =
            O2fitIdentityConstants.PermissionWithDot + "GetByIdAdminIngredient";

        public const string GetIngredientById = O2fitIdentityConstants.PermissionWithDot + "GetIngredientById";

        public const string SearchIngredientByNamePaginated =
            O2fitIdentityConstants.PermissionWithDot + "SearchIngredientByNamePaginated";

        public const string CreateIngredient = O2fitIdentityConstants.PermissionWithDot + "CreateIngredient";
        public const string UpdateIngredient = O2fitIdentityConstants.PermissionWithDot + "UpdateIngredient";
        public const string DeleteIngredientById = O2fitIdentityConstants.PermissionWithDot + "DeleteIngredientById";

        public const string CreateRootIngredientAllergy =
            O2fitIdentityConstants.PermissionWithDot + "CreateRootIngredientAllergy";

        public const string AddChildrenToIngredientAllergyCategory =
            O2fitIdentityConstants.PermissionWithDot + "AddChildrenToIngredientAllergyCategory";

        public const string DeleteIngredientAllergy =
            O2fitIdentityConstants.PermissionWithDot + "DeleteIngredientAllergy";

        public const string RemoveChildFromIngredientAllergyCategory =
            O2fitIdentityConstants.PermissionWithDot + "RemoveChildFromIngredientAllergyCategory";

        public const string GetByIdRecipeCategory = O2fitIdentityConstants.PermissionWithDot + "GetByIdRecipeCategory";

        public const string GetRecipeCategoryPaginated =
            O2fitIdentityConstants.PermissionWithDot + "GetRecipeCategoryPaginated";

        public const string CreateRecipeCategory = O2fitIdentityConstants.PermissionWithDot + "CreateRecipeCategory";
        public const string UpdateRecipeCategory = O2fitIdentityConstants.PermissionWithDot + "UpdateRecipeCategory";

        public const string SoftDeleteRecipeCategory =
            O2fitIdentityConstants.PermissionWithDot + "SoftDeleteRecipeCategory";

        public const string CreateNationality = O2fitIdentityConstants.PermissionWithDot + "CreateNationality";
        public const string PatchNationality = O2fitIdentityConstants.PermissionWithDot + "PatchNationality";
        public const string GetAllNationality = O2fitIdentityConstants.PermissionWithDot + "GetAllNationality";
        public const string DeleteNationality = O2fitIdentityConstants.PermissionWithDot + "DeleteNationality";

        public const string GetAllNationalityPagination =
            O2fitIdentityConstants.PermissionWithDot + "GetAllNationalityPagination";

        public const string CreateFoodCategory = O2fitIdentityConstants.PermissionWithDot + "CreateFoodCategory";
        public const string PatchFoodCategory = O2fitIdentityConstants.PermissionWithDot + "PatchFoodCategory";
        public const string GetByIdFoodCategory = O2fitIdentityConstants.PermissionWithDot + "GetByIdFoodCategory";

        public const string GetByParentIdFoodCategory =
            O2fitIdentityConstants.PermissionWithDot + "GetByParentIdFoodCategory";

        public const string GetAllFoodCategory = O2fitIdentityConstants.PermissionWithDot + "GetAllFoodCategory";

        public const string SoftDeleteFoodCategory =
            O2fitIdentityConstants.PermissionWithDot + "SoftDeleteFoodCategory";

        public const string GetAllCategoryPagination =
            O2fitIdentityConstants.PermissionWithDot + "GetAllCategoryPagination";

        public const string GetAllActiveDietCategory =
            O2fitIdentityConstants.PermissionWithDot + "GetAllActiveDietCategory";

        public const string SoftDeleteDietCategory =
            O2fitIdentityConstants.PermissionWithDot + "SoftDeleteDietCategory";

        public const string GetByIdDietCategory = O2fitIdentityConstants.PermissionWithDot + "GetByIdDietCategory";
        public const string PatchDietCategory = O2fitIdentityConstants.PermissionWithDot + "PatchDietCategory";
        public const string PostDietCategory = O2fitIdentityConstants.PermissionWithDot + "PostDietCategory";

        public const string GetAllDietCategoryPagination =
            O2fitIdentityConstants.PermissionWithDot + "GetAllDietCategoryPagination";

        public const string GetDietPackById = O2fitIdentityConstants.PermissionWithDot + "GetDietPackById";
        public const string GetAllPaginated = O2fitIdentityConstants.PermissionWithDot + "GetAllPaginated";
        public const string GetUserPackage = O2fitIdentityConstants.PermissionWithDot + "GetUserPackage";
        public const string CreateDietPack = O2fitIdentityConstants.PermissionWithDot + "CreateDietPack";
        public const string UpdateDietPack = O2fitIdentityConstants.PermissionWithDot + "GetAllDietCategoryPagination";
        public const string SoftDeleteDietPack = O2fitIdentityConstants.PermissionWithDot + "SoftDeleteDietPack";

        public const string GetAllActiveRecipes = O2fitIdentityConstants.PermissionWithDot + "GetAllActiveRecipes";

        public const string GetRecipeFoodWithDetailById =
            O2fitIdentityConstants.PermissionWithDot + "GetRecipeFoodWithDetailById";

        public const string GetAllRecipePaging = O2fitIdentityConstants.PermissionWithDot + "GetAllRecipePaging";
        public const string GetFullRecipeById = O2fitIdentityConstants.PermissionWithDot + "GetFullRecipeById";
        public const string GetAllRecipeTips = O2fitIdentityConstants.PermissionWithDot + "GetAllRecipeTips";
        public const string GetByIdRecipe = O2fitIdentityConstants.PermissionWithDot + "GetByIdRecipe";

        public const string GetRecipeStepsByFoodId =
            O2fitIdentityConstants.PermissionWithDot + "GetRecipeStepsByFoodId";

        public const string SearchFoodByFilter = O2fitIdentityConstants.PermissionWithDot + "SearchFoodByFilter";
        public const string GetFoodById = O2fitIdentityConstants.PermissionWithDot + "GetFoodById";
        public const string CreateRecipeSteps = O2fitIdentityConstants.PermissionWithDot + "CreateRecipeSteps";
        public const string CreateFood = O2fitIdentityConstants.PermissionWithDot + "CreateFood";
        public const string UpdateFood = O2fitIdentityConstants.PermissionWithDot + "UpdateFood";
        public const string ChangeRecipeStatus = O2fitIdentityConstants.PermissionWithDot + "ChangeRecipeStatus";
        public const string UpdateStep = O2fitIdentityConstants.PermissionWithDot + "UpdateStep";
        public const string UpdateTip = O2fitIdentityConstants.PermissionWithDot + "UpdateTip";
        public const string SoftDeleteRecipe = O2fitIdentityConstants.PermissionWithDot + "SoftDeleteRecipe";
        public const string SoftDeleteFood = O2fitIdentityConstants.PermissionWithDot + "SoftDeleteFood";
        public const string SoftDeleteRecipeStep = O2fitIdentityConstants.PermissionWithDot + "SoftDeleteRecipeStep";

        #endregion

        #region Advertise

        public const string GetAllActiveAdminAdvertises =
            O2fitIdentityConstants.PermissionWithDot + "GetAllActiveAdminAdvertises";

        public const string CreateAdminAdvertise = O2fitIdentityConstants.PermissionWithDot + "CreateAdminAdvertise";
        public const string PauseAdminAdvertise = O2fitIdentityConstants.PermissionWithDot + "PauseAdminAdvertise";
        public const string ActiveAdminAdvertise = O2fitIdentityConstants.PermissionWithDot + "ActiveAdminAdvertise";
        public const string DecreaseViewCount = O2fitIdentityConstants.PermissionWithDot + "DecreaseViewCount";
        public const string IncreaseClickCount = O2fitIdentityConstants.PermissionWithDot + "IncreaseClickCount";

        public const string SoftDeleteAdminAdvertise =
            O2fitIdentityConstants.PermissionWithDot + "SoftDeleteAdminAdvertise";

        public const string CreateNutritionistBannerAdvertise =
            O2fitIdentityConstants.PermissionWithDot + "CreateNutritionistBannerAdvertise";

        public const string ActiveNutritionistBannerAdvertise =
            O2fitIdentityConstants.PermissionWithDot + "ActiveNutritionistBannerAdvertise";

        public const string PauseNutritionistBannerAdvertise =
            O2fitIdentityConstants.PermissionWithDot + "PauseNutritionistBannerAdvertise";

        public const string IncreaseNutritionistBannerAdvertiseClickCount = O2fitIdentityConstants.PermissionWithDot +
                                                                            "IncreaseNutritionistBannerAdvertiseClickCount";

        public const string IncreaseNutritionistBannerAdvertiseViewCount = O2fitIdentityConstants.PermissionWithDot +
                                                                           "IncreaseNutritionistBannerAdvertiseViewCount";

        public const string GetNutritionistAdvertiseBannerById =
            O2fitIdentityConstants.PermissionWithDot + "GetNutritionistAdvertiseBannerById";

        public const string DecreaseNutritionistBannerAdvertiseClickCount =
            O2fitIdentityConstants.PermissionWithDot + "DecreaseNutritionistBannerAdvertiseClickCount";

        public const string IncreaseViewCount =
            O2fitIdentityConstants.PermissionWithDot + "IncreaseViewCount";
        
        #endregion

        #region Workout

        public const string GetAllBodyMuscle =
            O2fitIdentityConstants.PermissionWithDot + "GetAllBodyMuscle";

        public const string GetBodyMuscleById =
            O2fitIdentityConstants.PermissionWithDot + "GetBodyMuscleById";

        public const string CreateBodyMuscle =
            O2fitIdentityConstants.PermissionWithDot + "CreateBodyMuscle";

        public const string UpdateBodyMuscle =
            O2fitIdentityConstants.PermissionWithDot + "UpdateBodyMuscle";

        public const string SoftDeleteBodyMuscle =
            O2fitIdentityConstants.PermissionWithDot + "SoftDeleteBodyMuscle";

        public const string DeleteBodyMuscle =
            O2fitIdentityConstants.PermissionWithDot + "DeleteBodyMuscle";

        public const string GetExercisePaginated =
            O2fitIdentityConstants.PermissionWithDot + "GetExercisePaginated";

        public const string GetExerciseById =
            O2fitIdentityConstants.PermissionWithDot + "GetExerciseById";

        public const string GetCardioPaginated =
            O2fitIdentityConstants.PermissionWithDot + "GetCardioPaginated";
        public const string GetCardioById =
            O2fitIdentityConstants.PermissionWithDot + "GetCardioById";

        public const string GetBodyBuildingPaginated =
            O2fitIdentityConstants.PermissionWithDot + "GetBodyBuildingPaginated";

        public const string GetBodyBuildingById =
            O2fitIdentityConstants.PermissionWithDot + "GetBodyBuildingById";

        public const string CreateExercise =
            O2fitIdentityConstants.PermissionWithDot + "CreateExercise";

        public const string CreateCardio =
            O2fitIdentityConstants.PermissionWithDot + "CreateCardio";

        public const string CreateBodyBuilding =
            O2fitIdentityConstants.PermissionWithDot + "CreateBodyBuilding";

        public const string EditExercise =
            O2fitIdentityConstants.PermissionWithDot + "EditExercise";

        public const string EditCardio =
            O2fitIdentityConstants.PermissionWithDot + "EditCardio";
            
        public const string EditBodyBuilding =
            O2fitIdentityConstants.PermissionWithDot + "EditBodyBuilding";

        public const string SoftDeleteExercise =
            O2fitIdentityConstants.PermissionWithDot + "SoftDeleteExercise";

        public const string SoftDeleteCardio =
            O2fitIdentityConstants.PermissionWithDot + "SoftDeleteCardio";

        public const string SoftDeleteBodyBuilding =
            O2fitIdentityConstants.PermissionWithDot + "SoftDeleteBodyBuilding";

        #endregion

        #region Chat
        
        public const string GetGroupById =
            O2fitIdentityConstants.PermissionWithDot + "GetGroupById";

        public const string GetReportedGroupsPaginated =
            O2fitIdentityConstants.PermissionWithDot + "GetReportedGroupsPaginated";

        public const string GetAllGroupsPaginated =
            O2fitIdentityConstants.PermissionWithDot + "GetAllGroupsPaginated";

        public const string AddFileToGroup =
            O2fitIdentityConstants.PermissionWithDot + "AddFileToGroup";

        public const string ReportGroup =
            O2fitIdentityConstants.PermissionWithDot + "ReportGroup";

        public const string DeleteChatFileByUrl =
            O2fitIdentityConstants.PermissionWithDot + "DeleteChatFileByUrl";

        public const string Chat =
            O2fitIdentityConstants.PermissionWithDot + "Chat";

        #endregion
    }
}