using HardshipApp.Models.Entities;
namespace HardshipApp.Repository.Interfaces;
public interface IApplicationApprovalRepository
{
    Task<ApplicationApproval?> GetByApplicationIdAsync(Guid applicationId);
    Task<ApplicationApproval> CreateAsync(ApplicationApproval approval);
}