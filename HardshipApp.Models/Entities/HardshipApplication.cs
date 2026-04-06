
using HardshipApp.Models.Enum;

namespace HardshipApp.Models.Entities;
public class HardshipApplication
{
    public Guid Id {get; init;}
    public Guid ApplicantId {get; init;}
    public decimal Income {get; set;}
    public decimal Expenses {get; set;}
    public string? HardshipReason {get; set;}
    public ApplicationStatus Status {get; set;} = ApplicationStatus.Pending;
    public DateTime CreatedAt {get; init;}
    public DateTime UpdatedAt {get; set;}
    public Applicant Applicant{get; set;}
    public ApplicationApproval ApplicationApproval{get; set;}

    private HardshipApplication() { } //EF Core

    public HardshipApplication(Guid applicantId, decimal income, decimal expenses, string hardshipReason = null)
    {
        Id = Guid.NewGuid();
        ApplicantId = applicantId;
        Income = income;
        Expenses = expenses;
        HardshipReason = hardshipReason;
        Status = ApplicationStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}