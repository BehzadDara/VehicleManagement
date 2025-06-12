using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using VehicleManagement.API.Features;
using VehicleManagement.Application.Features;
using VehicleManagement.Application.ViewModels;
using Xunit;

namespace VehicleManagement.FunctionalTests;

public class CarTests(
    WebApplicationFactory<Program> factory
    ) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    [Fact]
    public async Task Get_List_Title_Contains_Test()
    {
        // Arrange
        var q = "Test";
        var url = $"Cars?q={q}";

        // Act
        var result = await _httpClient.GetAsync(url);
        var response = await result.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        var output = JsonSerializer.Deserialize<BaseResult<PaginationResult<CarViewModel>>>(response, options);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        output.Should().NotBeNull();
        output.IsSuccess.Should().BeTrue();
        output.Value.Should().NotBeNull();
        
        foreach ( var data in output.Value.Data)
        {
            data.Title.Should().Contain(q);
        }

    }

    [Fact]
    public async Task Get_List_Check_PageSize()
    {
        // Arrange
        var pageSize = 3;
        var url = $"Cars?pageSize={pageSize}";

        // Act
        var result = await _httpClient.GetAsync(url);
        var response = await result.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        var output = JsonSerializer.Deserialize<BaseResult<PaginationResult<CarViewModel>>>(response, options);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        output.Should().NotBeNull();
        output.IsSuccess.Should().BeTrue();
        output.Value.Should().NotBeNull();
        output.Value.Data.Count.Should().BeLessThanOrEqualTo(pageSize);
    }
}
