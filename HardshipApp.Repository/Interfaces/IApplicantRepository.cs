using HardshipApp.Models.Entities;

namespace HardshipApp.Repository.Interfaces;
public interface IApplicantRepository
{
    Task<Applicant?> GetByIdAsync(Guid id);
    Task<Applicant> CreateAsync(Applicant applicant);
}