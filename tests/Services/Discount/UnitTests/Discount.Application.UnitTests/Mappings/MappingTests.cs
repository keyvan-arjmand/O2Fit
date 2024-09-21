using AutoMapper;
using Discount.Application.Common.Mapping;
using System.Runtime.Serialization;
using Discount.Application.Dtos;
using Discount.Domain.Aggregates.DiscountPackageO2FitAggregate;

namespace Discount.Application.UnitTests.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }
    //
    // [Test]
    // // [TestCase(typeof(DiscountPackageO2Fit), typeof(DiscountPackageDto))]
    // // [TestCase(typeof(Domain.Aggregates.DiscountAggregate.Discount), typeof(DiscountDto))]
    // public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    // {
    //     var instance = GetInstanceOf(source);
    //
    //     _mapper.Map(instance, source, destination);
    // }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        // TODO: Figure out an alternative approach to the now obsolete `FormatterServices.GetUninitializedObject` method.
#pragma warning disable SYSLIB0050 // Type or member is obsolete
        return FormatterServices.GetUninitializedObject(type);
#pragma warning restore SYSLIB0050 // Type or member is obsolete
    }
}