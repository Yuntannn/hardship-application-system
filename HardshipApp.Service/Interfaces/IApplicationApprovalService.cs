using HardshipApp.Service.DTOs.Approval;

namespace HardshipApp.Service.Interfaces;

public interface IApplicationApprovalService
{
    Task<ApprovalResponseDto> CreateAsync(Guid applicationId, CreateApprovalDto dto);
    Task<ApprovalResponseDto?> GetByApplicationIdAsync(Guid applicationId);
}