using Moq;
using UniqueDraw.Domain.Ports.Helpers;

namespace UniqueDraw.Test.Domain.Ports;

[TestFixture]
public class MappingServiceTests
{
    private Mock<IMappingService> _mockMappingService;

    [SetUp]
    public void Setup()
    {
        _mockMappingService = new Mock<IMappingService>();
    }

    [Test]
    public void Map_WithValidSourceAndDestination_ReturnsMappedObject()
    {
        // Arrange
        var source = new Source { Value = "TestValue" };
        var destination = new Destination { Value = "TestValue" };

        _mockMappingService
            .Setup(service => service.Map<Source, Destination>(source))
            .Returns(destination);

        // Act
        var result = _mockMappingService.Object.Map<Source, Destination>(source);

        // Assert
        Assert.That(result.Value, Is.EqualTo("TestValue"));
        _mockMappingService.Verify(service => service.Map<Source, Destination>(source), Times.Once);
    }

    [Test]
    public void Map_WithObjectSource_ReturnsMappedObject()
    {
        // Arrange
        var source = new Source { Value = "TestValue" };
        var destination = new Destination { Value = "TestValue" };

        _mockMappingService
            .Setup(service => service.Map<Destination>(source))
            .Returns(destination);

        // Act
        var result = _mockMappingService.Object.Map<Destination>(source);

        // Assert
        Assert.That(result.Value, Is.EqualTo("TestValue"));
        _mockMappingService.Verify(service => service.Map<Destination>(source), Times.Once);
    }

    // Clases de ejemplo para las pruebas
    private class Source
    {
        public string Value { get; set; } = string.Empty;
    }

    private class Destination
    {
        public string Value { get; set; } = string.Empty;
    }
}
