using HardshipApp.Models.Entities;
using HardshipApp.Service.DTOs.Approval;

namespace HardshipApp.Service.Mappers;

public static class ApprovalMapper
{
    public static ApprovalResponseDto ToResponseDto(ApplicationApproval approval) => new ApprovalResponseDto
    {
        Id = approval.Id,
        ApplicationId = approval.ApplicationId,
        Decision = approval.Decision,
        Notes = approval.Notes,
        DecidedAt = approval.DecidedAt
    };
}