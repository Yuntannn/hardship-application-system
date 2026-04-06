using System.Net;
using System.Net.Http.Json;
using HardshipApp.Service.DTOs.Applicant;
using HardshipApp.IntegrationTests.Helpers;
namespace HardshipApp.IntegrationTests.Test;

public class ApplicantTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ApplicantTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateApplicant_ReturnsCreated()
    {
        var dto = new CreateApplicantDto
        {
            FirstName = "Jane",
            LastName = "Smith",
            DateOfBirth = new DateOnly(1995, 5, 5),
            Email = "createapplicant@example.com",
        };

        var response = await _client.PostAsJsonAsync("/api/applicants", dto);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApplicantResponseDto>();
        Assert.NotNull(result);
        Assert.Equal("Jane", result.FirstName);
    }

    [Fact]
    public async Task GetAllApplicants_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/applicants");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetApplicantById_ReturnsOk()
    {
        var created = await TestDataHelper.CreateTestApplicant(_client, "getbyid@example.com");

        var response = await _client.GetAsync($"/api/applicants/{created!.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApplicantResponseDto>();
        Assert.NotNull(result);
        Assert.Equal(created.Id, result.Id);
    }

    [Fact]
    public async Task GetApplicantById_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/api/applicants/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateApplicant_ReturnsOk()
    {
        var created = await TestDataHelper.CreateTestApplicant(_client, "updateapplication@example.com");
        var updateDto = new UpdateApplicantDto
        {
            FirstName = "Updated",
            LastName = "Name",
            DateOfBirth = new DateOnly(1995, 5, 5),
            Email = "updateapplicant@example.com",
        };

        var response = await _client.PutAsJsonAsync($"/api/applicants/{created!.Id}", updateDto);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApplicantResponseDto>();
        Assert.NotNull(result);
        Assert.Equal("Updated", result.FirstName);
    }
}