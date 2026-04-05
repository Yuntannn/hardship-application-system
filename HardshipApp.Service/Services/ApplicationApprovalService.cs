using HardshipApp.Models.Entities;
using HardshipApp.Repository.Interfaces;
using HardshipApp.Service.DTOs.Approval;
using HardshipApp.Service.Interfaces;
using HardshipApp.Service.Mappers;

namespace HardshipApp.Service.Services;

public class ApplicationApprovalService : IApplicationApprovalService
{
    private readonly IApplicationApprovalRepository _approvalRepository;
    private readonly IHardshipApplicationRepository _applicationRepository;

    public ApplicationApprovalService(
        IApplicationApprovalRepository approvalRepository,
        IHardshipApplicationRepository applicationRepository)
    {
        _approvalRepository = approvalRepository;
        _applicationRepository = applicationRepository;
    }

    public async Task<ApprovalResponseDto> CreateAsync(Guid applicationId, CreateApprovalDto dto)
    {
        var application = await _applicationRepository.GetByIdAsync(applicationId);
        if (application == null) throw new KeyNotFoundException($"Application {applicationId} not found.");

        var approval = new ApplicationApproval(applicationId, dto.Decision, dto.Notes);
        await _approvalRepository.CreateAsync(approval);

        application.Status = dto.Decision;
        await _applicationRepository.UpdateAsync(application);

        return ApprovalMapper.ToResponseDto(approval);
    }

    public async Task<ApprovalResponseDto?> GetByApplicationIdAsync(Guid applicationId)
    {
        var approval = await _approvalRepository.GetByApplicationIdAsync(applicationId);
        if (approval == null) return null;
        return ApprovalMapper.ToResponseDto(approval);
    }
}