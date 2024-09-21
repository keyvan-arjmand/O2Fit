using FluentAssertions;
using MongoDB.Bson;
using Payment.Application.Common.Exceptions;
using Payment.IntegrationTests.Utilities;

namespace Payment.IntegrationTests.Commands.Command;

public class DeletePackageCommandTests : BaseTestFixture
{
    //[Test]
    //public async Task DeletePackageCommand_DeleteInDb_ReturnSuccess()
    //{
    //    var command = new CreatePackageCommand
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
    //    var package = await Testing.SendAsync(command);
    //    var commandDelete = new DeletePackageCommand(package.Id);
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandDelete)).Should().NotThrowAsync();
    //}
    //[Test]
    //public async Task DeletePackageCommand_EmptyPackageId_ThrowValidationException()
    //{

    //    var commandDelete = new DeletePackageCommand(new ObjectId());
    //    await FluentActions.Invoking(() => Testing.SendAsync(commandDelete)).Should().ThrowAsync<ValidationException>();
    //}
}