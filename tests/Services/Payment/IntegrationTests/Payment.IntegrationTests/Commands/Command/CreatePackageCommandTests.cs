using FluentAssertions;
using FluentValidation;
using Payment.Application.Dtos;
using Payment.IntegrationTests.Utilities;
using static Payment.IntegrationTests.Testing;
using static Payment.IntegrationTests.MasstransitHarnessConfiguration;
using System.Xml.Linq;
using Currency.Application;
using Currency.Application.Common.Behaviours;
using Currency.Application.Consumers.Currencies;
using MongoDB.Bson;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using Moq;

namespace Payment.IntegrationTests.Commands.Command;

public class CreatePackageCommandTests : BaseTestFixture
{
    [Test]
    public async Task CreatePackageCommand_InsertToDb_ReturnPackage()
    {
        //await using var provider = new ServiceCollection()
        //    .AddScoped<IUnitOfWork, UnitOfWork>()
        //    .AddSingleton<IConfiguration>(Testing.Configuration)
        //    .AddScoped<Currency.Application.Common.Interfaces.Persistence.UoW.IUnitOfWork, 
        //                Currency.Infrastructure.Persistence.UoW.UnitOfWork>()
        //    .AddMediatR(cfg =>
        //    {
        //        cfg.RegisterServicesFromAssembly(typeof(IMediarMarker).Assembly);
        //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        //        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        //    })
        //    .AddMassTransitTestHarness(x =>
        //    {
        //        x.SetKebabCaseEndpointNameFormatter();
        //        x.AddConsumer<GetCurrencyByCodeConsumer>();
        //        x.AddConsumerTestHarness<GetCurrencyByCodeConsumer>();
        //        // x.AddHandler<GetCurrencyByName>(context =>
        //        //     context.RespondAsync(new GetCurrencyByNameResult
        //        //     {
        //        //         Id = ObjectId.GenerateNewId().ToString()!,
        //        //         Name = "Dollar"
        //        //     }));
        //         x.AddRequestClient<GetCurrencyByName>();
        //    })
        //    .AddGenericRequestClient()
        //    .BuildServiceProvider(true);

        //var harness = provider.GetTestHarness();

        //await harness.Start();
        
        ////var bus = provider.GetRequiredService<IBusControl>();

        //IRequestClient<GetCurrencyByName>? client = harness.GetRequestClient<GetCurrencyByName>();
        //await Task.Delay(2000);
        //var req = new GetCurrencyByName
        //{
        //    Name = "Dollar"
        //};
        //var response = await client.GetResponse<GetCurrencyByNameResult>(req);
        
        //(await harness.Sent.Any<GetCurrencyByNameResult>()).Should().BeTrue();

        //(await harness.Consumed.Any<GetCurrencyByName>()).Should().BeTrue();

        //var consumerHarness = GetConsumerTestHarness<GetCurrencyByCodeConsumer>();

        //(await consumerHarness.Consumed.Any<GetCurrencyByName>()).Should().BeTrue();

        //var command = new CreatePackageNutritionistCommand
        //{
            
        //    IsActive = true,
        //    Description = new Translation
        //    {
        //        Id = ObjectId.GenerateNewId().ToString(),
        //        Arabic = StringHelper.RandomString(10),
        //        English = StringHelper.RandomString(10),
        //        Persian = StringHelper.RandomString(10),
        //    },
        //    Sort = 1,
        //    Name = new Translation
        //    {
        //        Id = ObjectId.GenerateNewId().ToString(),
        //        Arabic = StringHelper.RandomString(10),
        //        English = StringHelper.RandomString(10),
        //        Persian = StringHelper.RandomString(10),
        //    },
        //    Price = 999,
        //    CurrencyName = "Dollar"
        //};
        //var packageId = await SendAsync(command);
        //packageId.Should().NotBeNull();
        //packageId.Should().NotBeEmpty();
    }
    //[Test]
    //public async Task CreatePackageCommand_InsertToDb_ReturnPackage()
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

    //    package.Should().NotBeNull();
    //    package.Id.Should().NotBeNull();
    //    package.IsActive.Should().Be(command.IsActive);
    //    package.Sort.Should().Be(command.Sort);
    //    package.Currency.Should().Be(command.Currency);
    //    package.DiscountPercent.Should().Be(command.DiscountPercent);
    //    package.PackageType.Should().Be(command.PackageType);
    //    package.TranslationDescription.Arabic.Should().Be(command.Description.Arabic);
    //    package.TranslationDescription.English.Should().Be(command.Description.English);
    //    package.TranslationDescription.Persian.Should().Be(command.Description.Persian);
    //    package.TranslationName.Arabic.Should().Be(command.Name.Arabic);
    //    package.TranslationName.English.Should().Be(command.Name.English);
    //    package.TranslationName.Persian.Should().Be(command.Name.Persian);
    //    package.Price.Should().Be(command.Price);
    //    package.Duration.Should().Be(command.Duration);
    //    package.IsPromote.Should().Be(command.IsPromote);

    //}

    //[Test]
    //public async Task CreatePackageCommand_NullPackageName_ThrowValidationException()
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
    //        Name = null,
    //        Price = 999,
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<Payment.Application.Common.Exceptions.ValidationException>();
    //}
    //[Test]
    //public async Task CreatePackageCommand_NullPackageDescription_ThrowValidationException()
    //{
    //    var command = new CreatePackageCommand
    //    {
    //        PackageType = 0,
    //        IsActive = true,
    //        Description = null,
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<Payment.Application.Common.Exceptions.ValidationException>();
    //}

    //[Test]
    //public async Task CreatePackageCommand_NegativeNumberPackageDuration_ThrowValidationException()
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
    //        Duration = -1,
    //        IsPromote = true,
    //        Name = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Price = 999,
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<Payment.Application.Common.Exceptions.ValidationException>();
    //}
    //[Test]
    //public async Task CreatePackageCommand_EmptyPackageDuration_ThrowValidationException()
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
    //        Duration = new int(),
    //        IsPromote = true,
    //        Name = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Price = 999,
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<Payment.Application.Common.Exceptions.ValidationException>();
    //}
    //[Test]
    //public async Task CreatePackageCommand_NullPackageDuration_ThrowValidationException()
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
    //        IsPromote = true,
    //        Name = new Translation
    //        {
    //            Arabic = StringHelper.RandomString(10),
    //            English = StringHelper.RandomString(10),
    //            Persian = StringHelper.RandomString(10),
    //        },
    //        Price = 999,
    //    };
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<Payment.Application.Common.Exceptions.ValidationException>();
    //}
    //[Test]
    //public async Task CreatePackageCommand_NullPackageSort_ThrowValidationException()
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
    //        Currency = 0,
    //        Sort = new int(),
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<Payment.Application.Common.Exceptions.ValidationException>();
    //}
    //[Test]
    //public async Task CreatePackageCommand_NegativeNumberPackageSort_ThrowValidationException()
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
    //        Currency = 0,
    //        Sort = -1,
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
    //    await FluentActions.Invoking(() => Testing.SendAsync(command)).Should().ThrowAsync<Payment.Application.Common.Exceptions.ValidationException>();
    //}
}