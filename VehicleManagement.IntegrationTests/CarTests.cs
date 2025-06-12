using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using VehicleManagement.API;
using VehicleManagement.DomainModel.Enums;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.Infrastructure.Data.Repositories;
using VehicleManagement.IntegrationTests.Fixtures;
using Xunit;

namespace VehicleManagement.IntegrationTests;

public class CarTests(
    VehicleManagementDBContextFixture fixture
    ) : IClassFixture<VehicleManagementDBContextFixture>
{
    private const string title = "My Title";
    private const GearboxType gearbox = GearboxType.Manual;

    [Fact]
    public async Task Create_Repository()
    {
        // Arrange
        var db = fixture.Build(Guid.NewGuid().ToString());

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var currentUser = new CurrentUser(httpContextAccessorMock.Object);

        var repository = new CarRepository(db, currentUser);
        var car = Car.Create(title, gearbox);

        // Act
        await repository.AddAsync(car, CancellationToken.None);
        await db.SaveChangesAsync();

        // Assert
        var entity = await repository.GetByIdAsync(car.Id, CancellationToken.None);
        entity.Should().NotBeNull();
        entity.Title.Should().Be(title);
        entity.Gearbox.Should().Be(gearbox);
        entity.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Repository_Created_By()
    {
        // Arrange
        var username = "Behzad";
        var db = fixture.Build(Guid.NewGuid().ToString());

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, username)
        };
        var identity = new ClaimsIdentity(claims);
        var user = new ClaimsPrincipal(identity);

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.User).Returns(user);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

        var currentUser = new CurrentUser(httpContextAccessorMock.Object);

        var repository = new CarRepository(db, currentUser);
        var car = Car.Create(title, gearbox);

        // Act
        await repository.AddAsync(car, CancellationToken.None);
        await db.SaveChangesAsync();

        // Assert
        var entity = await repository.GetByIdAsync(car.Id, CancellationToken.None);
        entity.Should().NotBeNull();
        entity.CreatedBy.Should().Be(username);
    }
}
