using HardshipApp.Models.Entities;
using HardshipApp.Repository.Interfaces;
using HardshipApp.Service.DTOs.Applicant;
using HardshipApp.Service.Interfaces;
using HardshipApp.Service.Mappers;

namespace HardshipApp.Service.Services;

public class ApplicantService : IApplicantService
{
    private readonly IApplicantRepository _applicantRepository;

    public ApplicantService(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }

    public async Task<ApplicantResponseDto?> GetByIdAsync(Guid id)
    {
        var applicant = await _applicantRepository.GetByIdAsync(id);
        if (applicant == null) return null;

        return ApplicantMapper.ToResponseDto(applicant);
    }

    public async Task<ApplicantResponseDto> CreateAsync(CreateApplicantDto dto)
    {
        var applicant = new Applicant(
            dto.FirstName,
            dto.LastName,
            dto.DateOfBirth,
            dto.Email,
            dto.Phone
        );
        await _applicantRepository.CreateAsync(applicant);

        return ApplicantMapper.ToResponseDto(applicant);
    }
    public async Task<IEnumerable<ApplicantResponseDto>> GetAllAsync()
    {
        var applicants = await _applicantRepository.GetAllAsync();
        return applicants.Select(ApplicantMapper.ToResponseDto);
    }

    public async Task<ApplicantResponseDto> UpdateAsync(Guid id, UpdateApplicantDto dto)
    {
        var applicant = await _applicantRepository.GetByIdAsync(id);
        if (applicant == null) throw new KeyNotFoundException($"Applicant {id} not found.");

        applicant.FirstName = dto.FirstName;
        applicant.LastName = dto.LastName;
        applicant.DateOfBirth = dto.DateOfBirth;
        applicant.Email = dto.Email;
        applicant.Phone = dto.Phone;

        await _applicantRepository.UpdateAsync(applicant);
        return ApplicantMapper.ToResponseDto(applicant);
    }
}