using HardshipApp.Models.Entities;
using HardshipApp.Service.DTOs;

namespace HardshipApp.Service.Mappers;

public class HardshipApplicationMapper{
    public static HardshipApplicationResponseDto ToResponseDto(HardshipApplication application, Applicant applicant)
    {
        return new HardshipApplicationResponseDto
        {
            Id = application.Id,
            FirstName = applicant.FirstName,
            LastName = applicant.LastName,
            DateOfBirth = applicant.DateOfBirth,
            Email = applicant.Email,
            Phone = applicant.Phone,
            Income = application.Income,
            Expenses = application.Expenses,
            HardshipReason = application.HardshipReason,
            Status = application.Status,
            CreatedAt = application.CreatedAt,
            UpdatedAt = application.UpdatedAt
        };
    }
}
