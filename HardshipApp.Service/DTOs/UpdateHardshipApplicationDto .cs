namespace HardshipApp.Service.DTOs;
public class UpdateHardshipApplicationDto
{
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public string? HardshipReason { get; set; }
    public string Status { get; set; } = string.Empty;
}