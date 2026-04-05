
using HardshipApp.Models.Enum;

namespace HardshipApp.Models.Entities;
public class ApplicationApproval
{
    public Guid Id{get; init;}
    public Guid ApplicationId{get; set;}
    public ApplicationStatus Decision{get; set;} 
    public string? Notes{get; set;}
    public DateTime DecidedAt{get; init;}

    public HardshipApplication HardshipApplication{get; set;} = null!;
    private ApplicationApproval(){ } //EF Core

    public ApplicationApproval(Guid applicationId, ApplicationStatus decision, string? notes = null)
    {
        Id = Guid.NewGuid();
        ApplicationId = applicationId;
        Decision = decision;
        Notes = notes;
        DecidedAt = DateTime.UtcNow;
    }
}