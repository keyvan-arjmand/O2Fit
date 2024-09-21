using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using Payment.Application.Common.Exceptions;
using Payment.IntegrationTests.Utilities;

namespace Payment.IntegrationTests.Commands.Command;

public class UpdatePackageCommandTests : BaseTestFixture
{
    //[Test]
    //public async Task UpdatePackageCommand_UpdatePackage_ReturnSuccess()
    //{
    //    var commandCreate = new CreatePackageCommand
    //    {
    //        PackageType = 0,
    //        IsActive = true,
    //        Description = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Sort = 1,
    //        Currency = 0,
    //        DiscountPercent = 2.3,
    //        Duration = 1,
    //        IsPromote = true,
    //        Name = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Price = 999,
    //    };
    //    var package = await Testing.SendAsync(commandCreate);

    //    var command = new UpdatePackageCommand(package.Id.ToString(), new Translation(), new Translation(), 99, 4, 1, 0, 0, true, 1, true);
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().NotThrowAsync();


    //}

    //[Test]
    //public async Task UpdatePackageCommand_NullPackageID_ThrowValidationException()
    //{
    //    var command = new UpdatePackageCommand(null, new Translation(), new Translation(), 99, 4, 1, 0, 0, true, 1, true);
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

    //}
    //[Test]
    //public async Task UpdatePackageCommand_EmptyPackageID_ThrowValidationException()
    //{
    //    var command = new UpdatePackageCommand(string.Empty, new Translation(), new Translation(), 99, 4, 1, 0, 0, true, 1, true);
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

    //}
    //[Test]
    //public async Task UpdatePackageCommand_NullPackageName_ThrowValidationException()
    //{
    //    var commandCreate = new CreatePackageCommand
    //    {
    //        PackageType = 0,
    //        IsActive = true,
    //        Description = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Sort = 1,
    //        Currency = 0,
    //        DiscountPercent = 2.3,
    //        Duration = 1,
    //        IsPromote = true,
    //        Name = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Price = 999,
    //    };
    //    var package = await Testing.SendAsync(commandCreate);
    //    var command = new UpdatePackageCommand(package.Id.ToString(), null, new Translation(), 99, 4, 1, 0, 0, true, 1, true);
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task UpdatePackageCommand_NullPackageDescription_ThrowValidationException()
    //{
    //    var commandCreate = new CreatePackageCommand
    //    {
    //        PackageType = 0,
    //        IsActive = true,
    //        Description = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Sort = 1,
    //        Currency = 0,
    //        DiscountPercent = 2.3,
    //        Duration = 1,
    //        IsPromote = true,
    //        Name = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Price = 999,
    //    };
    //    var package = await Testing.SendAsync(commandCreate);
    //    var command = new UpdatePackageCommand(package.Id.ToString(), new Translation(), null, 99, 4, 1, 0, 0, true, 1, true);
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}

    //[Test]
    //public async Task UpdatePackageCommand_NegativeNumberPackageDuration_ThrowValidationException()
    //{
    //    var commandCreate = new CreatePackageCommand
    //    {
    //        PackageType = 0,
    //        IsActive = true,
    //        Description = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Sort = 1,
    //        Currency = 0,
    //        DiscountPercent = 2.3,
    //        Duration = 1,
    //        IsPromote = true,
    //        Name = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Price = 999,
    //    };
    //    var package = await Testing.SendAsync(commandCreate);
    //    var command = new UpdatePackageCommand(package.Id.ToString(), new Translation(), new Translation(), 99, 4, -1, 0, 0, true, 1, true);
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
    //[Test]
    //public async Task UpdatePackageCommand_EmptyPackageDuration_ThrowValidationException()
    //{
    //    var commandCreate = new CreatePackageCommand
    //    {
    //        PackageType = 0,
    //        IsActive = true,
    //        Description = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Sort = 1,
    //        Currency = 0,
    //        DiscountPercent = 2.3,
    //        Duration = 1,
    //        IsPromote = true,
    //        Name = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Price = 999,
    //    };
    //    var package = await Testing.SendAsync(commandCreate);
    //    var command = new UpdatePackageCommand(package.Id.ToString(), new Translation(), new Translation(), 99, 4, new int(), 0, 0, true, 1, true);
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
   
    //[Test]
    //public async Task UpdatePackageCommand_NegativeNumberPackageSort_ThrowValidationException()
    //{
    //    var commandCreate = new CreatePackageCommand
    //    {
    //        PackageType = 0,
    //        IsActive = true,
    //        Description = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Sort = 1,
    //        Currency = 0,
    //        DiscountPercent = 2.3,
    //        Duration = 1,
    //        IsPromote = true,
    //        Name = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Price = 999,
    //    };
    //    var package = await Testing.SendAsync(commandCreate);
    //    var command = new UpdatePackageCommand(package.Id.ToString(), new Translation(), new Translation(), 99, 4, 1, 0, 0, true, -1, true);
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    //}
}