using System.Net.Http.Json;
using HardshipApp.Service.DTOs.Application;
using HardshipApp.Service.DTOs.Applicant;

namespace HardshipApp.IntegrationTests.Helpers;

public static class TestDataHelper
{
    public static async Task<HardshipApplicationResponseDto> CreateTestApplication(
        HttpClient client, string email)
    {
        var dto = new CreateHardshipApplicationDto
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Email = email,
            Income = 50000,
            Expenses = 30000,
            HardshipReason = "Lost job"
        };
        var response = await client.PostAsJsonAsync("/api/hardshipapplications", dto);
        return await response.Content.ReadFromJsonAsync<HardshipApplicationResponseDto>();
    }

    public static async Task<ApplicantResponseDto> CreateTestApplicant(
        HttpClient client, string email)
    {
        var dto = new CreateApplicantDto
        {
            FirstName = "Jane",
            LastName = "Smith",
            DateOfBirth = new DateOnly(1995, 5, 5),
            Email = email,
        };
        var response = await client.PostAsJsonAsync("/api/applicants", dto);
        return await response.Content.ReadFromJsonAsync<ApplicantResponseDto>();
    }
}