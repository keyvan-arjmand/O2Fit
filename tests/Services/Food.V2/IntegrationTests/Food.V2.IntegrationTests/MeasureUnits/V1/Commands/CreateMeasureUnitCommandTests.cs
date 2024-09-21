using FluentAssertions;
using Food.V2.Application.Dtos.MeasureUnits;
using Food.V2.Application.MeasureUnits.V1.Commands.CreateMeasureUnit;
using Food.V2.IntegrationTests.Utilities;
using static Food.V2.IntegrationTests.Testing;

namespace Food.V2.IntegrationTests.MeasureUnits.V1.Commands;

public class CreateMeasureUnitCommandTests : BaseTestFixture
{
    [Test]
    public async Task CreateMeasureUnit_AddOneMeasureUnit_ReturnsId()
    {
        var command = new CreateMeasureUnitCommand(5.99m, false, new CreateUpdateMeasureUnitTranslationDto
        {
            Arabic = StringHelper.RandomString(9),
            English = StringHelper.RandomString(9),
            Persian = StringHelper.RandomString(9)
        });
        var id = await SendAsync(command);
        id.Should().NotBeEmpty();
        id.Should().NotBeNull();
    }
}