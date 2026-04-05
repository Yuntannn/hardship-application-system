using HardshipApp.Models.Entities;
using HardshipApp.Service.DTOs.Applicant;

namespace HardshipApp.Service.Mappers;

public static class ApplicantMapper
{
    public static ApplicantResponseDto ToResponseDto(Applicant applicant) => new ApplicantResponseDto
    {
        Id = applicant.Id,
        FirstName = applicant.FirstName,
        LastName = applicant.LastName,
        DateOfBirth = applicant.DateOfBirth,
        Email = applicant.Email,
        Phone = applicant.Phone,
        CreatedAt = applicant.CreatedAt
    };
}