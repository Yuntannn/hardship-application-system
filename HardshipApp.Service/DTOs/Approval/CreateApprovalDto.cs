using HardshipApp.Models.Enum;

namespace HardshipApp.Service.DTOs.Approval;

public class CreateApprovalDto
{
    public ApplicationStatus Decision { get; set; }
    public string? Notes { get; set; }
}