using HardshipApp.Models.Enum;

namespace HardshipApp.Service.DTOs.Approval;

public class ApprovalResponseDto
{
    public Guid Id { get; set; }
    public Guid ApplicationId { get; set; }
    public ApplicationStatus Decision { get; set; }
    public string? Notes { get; set; }
    public DateTime DecidedAt { get; set; }
}