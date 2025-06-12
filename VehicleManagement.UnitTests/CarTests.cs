using FluentAssertions;
using VehicleManagement.DomainModel.Enums;
using VehicleManagement.DomainModel.Models.CarAggregate;
using Xunit;

namespace VehicleManagement.UnitTests;

public class CarTests
{
    private const string title = "My Title";
    private const GearboxType gearbox = GearboxType.Manual;

    [Fact]
    public void Create_Car_Set_Fields()
    {
        // Arrange

        // Act
        var car = Car.Create(title, gearbox);

        // Assert
        Assert.NotNull(car);
        Assert.Equal(title, car.Title);
        Assert.Equal(gearbox, car.Gearbox);
        Assert.True(car.IsActive);
        Assert.False(car.IsDeleted);
    }

    [Fact]
    public void Toggle_Activation_IsActive_False()
    {
        // Arrange
        var car = Car.Create(title, gearbox);

        // Act
        car.ToggleActivation();

        // Assert
        car.IsActive.Should().BeFalse();
    }

    [Theory]
    [InlineData("My Title2")]
    public void Update_Car_Set_Fields(string newTitle)
    {
        // Arrange
        var car = Car.Create(title, gearbox);

        // Act
        car.Update(newTitle, car.Gearbox);

        // Assert
        car.Title.Should().NotBe(title);
        car.Title.Should().Be(newTitle);
    }

    [Theory]
    [MemberData(nameof(AddOptionData))]
    public void Add_Option_Check_Count(IEnumerable<string> descriptions)
    {
        // Arrange
        var car = Car.Create(title, gearbox);

        // Act
        foreach (var description in descriptions)
        {
            car.AddOption(description);
        }

        // Assert
        car.Options.Count.Should().Be(descriptions.Count());
    }

    public static IEnumerable<object[]> AddOptionData =>
        [
            [new List<string> { "Option1", "Option2", "Option3", }], 
        ];
}
