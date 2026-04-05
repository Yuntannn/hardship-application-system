using System.Net;
using System.Net.Http.Json;
using HardshipApp.Service.DTOs.Application;
using HardshipApp.Models.Enum;

namespace HardshipApp.IntegrationTests;

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
}