namespace HardshipApp.Service.DTOs.Application;
public class CreateHardshipApplicationDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public string? HardshipReason { get; set; }
}