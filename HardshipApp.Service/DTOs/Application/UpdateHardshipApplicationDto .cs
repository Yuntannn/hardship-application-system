using HardshipApp.Models.Enum;

namespace HardshipApp.Service.DTOs.Application;
public class UpdateHardshipApplicationDto
{
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public string? HardshipReason { get; set; }
    public ApplicationStatus Status { get; set; }
}