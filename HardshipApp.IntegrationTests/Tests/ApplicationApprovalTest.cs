using System.Net;
using System.Net.Http.Json;
using HardshipApp.Service.DTOs.Application;
using HardshipApp.Service.DTOs.Approval;
using HardshipApp.Models.Enum;
using HardshipApp.IntegrationTests.Helpers;

namespace HardshipApp.IntegrationTests.Test;

public class ApprovalTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ApprovalTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateApproval_ReturnsOk()
    {
        var application = await TestDataHelper.CreateTestApplication(_client, "test@example.com");
        var dto = new CreateApprovalDto
        {
            Decision = ApplicationStatus.Approved,
            Notes = "Approved after review"
        };

        var response = await _client.PostAsJsonAsync(
            $"/api/hardshipapplications/{application!.Id}/approvals", dto);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApprovalResponseDto>();
        Assert.NotNull(result);
        Assert.Equal(ApplicationStatus.Approved, result.Decision);
    }

    [Fact]
    public async Task GetApproval_ReturnsOk()
    {
        var application = await TestDataHelper.CreateTestApplication(_client, "approval_get_test@example.com");
        var dto = new CreateApprovalDto
        {
            Decision = ApplicationStatus.Declined,
            Notes = "Insufficient evidence"
        };
        await _client.PostAsJsonAsync(
            $"/api/hardshipapplications/{application!.Id}/approvals", dto);

        var response = await _client.GetAsync(
            $"/api/hardshipapplications/{application.Id}/approvals");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApprovalResponseDto>();
        Assert.NotNull(result);
        Assert.Equal(ApplicationStatus.Declined, result.Decision);
    }

    [Fact]
    public async Task GetApproval_ReturnsNotFound()
    {
        var response = await _client.GetAsync(
            $"/api/hardshipapplications/{Guid.NewGuid()}/approvals");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}