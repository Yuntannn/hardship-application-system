using System.Net;
using System.Net.Http.Json;
using HardshipApp.Service.DTOs.Application;
using HardshipApp.Models.Enum;
using HardshipApp.IntegrationTests.Helpers;

namespace HardshipApp.IntegrationTests.Test;

public class HardshipApplicationTests : IClassFixture<CustomWebApplicationFactory>  
{
    private readonly HttpClient _client;
    public HardshipApplicationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateApplication_ReturnsCreated()
    {
        // Arrange
        var dto = new CreateHardshipApplicationDto
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Email = "john.doe@example.com",
            Income = 50000,
            Expenses = 30000,
            HardshipReason = "Lost job"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/hardshipapplications", dto);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<HardshipApplicationResponseDto>();
        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
        Assert.Equal(ApplicationStatus.Pending, result.Status);
    }

    [Fact]
    public async Task GetAllApplications_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/hardshipapplications");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetApplicationById_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/api/hardshipapplications/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetApplicationById_ReturnsOk()
    {
    // Arrange
    var created = await TestDataHelper.CreateTestApplication(_client, "getbyid@example.com");

    // Act
    var response = await _client.GetAsync($"/api/hardshipapplications/{created!.Id}");

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    var result = await response.Content.ReadFromJsonAsync<HardshipApplicationResponseDto>();
    Assert.NotNull(result);
    Assert.Equal(created.Id, result.Id);
    }

    [Fact]
    public async Task UpdateApplication_ReturnsOk()
    {
        // Arrange
        var created = await TestDataHelper.CreateTestApplication(_client, "update@example.com");
        var updateDto = new UpdateHardshipApplicationDto
        {
            Income = 60000,
            Expenses = 40000,
            HardshipReason = "Medical expenses",
            Status = ApplicationStatus.UnderReview
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/hardshipapplications/{created!.Id}", updateDto);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<HardshipApplicationResponseDto>();
        Assert.NotNull(result);
        Assert.Equal(60000, result.Income);
        Assert.Equal(ApplicationStatus.UnderReview, result.Status);
    }

    [Fact]
    public async Task DeleteApplication_ReturnsNoContent()
    {
        // Arrange
        var created = await TestDataHelper.CreateTestApplication(_client, "delete@example.com");

        // Act
        var response = await _client.DeleteAsync($"/api/hardshipapplications/{created!.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteApplication_ThenGetById_ReturnsNotFound()
    {
        // Arrange
        var created = await TestDataHelper.CreateTestApplication(_client, "deleteverify@example.com");
        await _client.DeleteAsync($"/api/hardshipapplications/{created!.Id}");

        // Act
        var response = await _client.GetAsync($"/api/hardshipapplications/{created.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}