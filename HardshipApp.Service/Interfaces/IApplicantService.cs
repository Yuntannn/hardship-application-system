using HardshipApp.Service.DTOs.Applicant;

namespace HardshipApp.Service.Interfaces;

public interface IApplicantService
{
    Task<ApplicantResponseDto?> GetByIdAsync(Guid id);
    Task<ApplicantResponseDto> CreateAsync(CreateApplicantDto dto);
    Task<IEnumerable<ApplicantResponseDto>> GetAllAsync();
    Task<ApplicantResponseDto> UpdateAsync(Guid id, UpdateApplicantDto dto);
}