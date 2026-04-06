using HardshipApp.Models.Entities;
using HardshipApp.Repository.Interfaces;
using HardshipApp.Service.DTOs.Application;
using HardshipApp.Service.Interfaces;
using HardshipApp.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using HardshipApp.Common.Exceptions;

namespace HardshipApp.Service.Services;

public class HardshipApplicationService : IHardshipApplicationService
{
    private readonly IHardshipApplicationRepository _hardshipApplicationRepository;
    private readonly IApplicantRepository _applicantRepository;
    public HardshipApplicationService(IHardshipApplicationRepository hardshipApplicationRepository, IApplicantRepository applicantRepository)
    {
        _hardshipApplicationRepository = hardshipApplicationRepository;
        _applicantRepository = applicantRepository;
    }

    public async Task<HardshipApplicationResponseDto?> GetByIdAsync(Guid id)
    {
        var application = await _hardshipApplicationRepository.GetByIdAsync(id);
        if(application == null) return null;
        var applicant = await _applicantRepository.GetByIdAsync(application.ApplicantId);
        return HardshipApplicationMapper.ToResponseDto(application, applicant!);
    }

    public async Task<IEnumerable<HardshipApplicationResponseDto>> GetAllAsync()
    {
        var applications = await _hardshipApplicationRepository.GetAllAsync();
        var result = new List<HardshipApplicationResponseDto>();

        foreach(var application in applications)
        {
            var applicant = await _applicantRepository.GetByIdAsync(application.ApplicantId);
            result.Add(HardshipApplicationMapper.ToResponseDto(application, applicant!));
        }
        return result;
    }

    public async Task<HardshipApplicationResponseDto> CreateAsync(CreateHardshipApplicationDto createHardshipApplicationDto)
    {
        try
        {
            var applicant = new Applicant(
            createHardshipApplicationDto.FirstName,
            createHardshipApplicationDto.LastName,
            createHardshipApplicationDto.DateOfBirth,
            createHardshipApplicationDto.Email,
            createHardshipApplicationDto.Phone
            );
            await _applicantRepository.CreateAsync(applicant);

            var application = new HardshipApplication(
                applicant.Id,
                createHardshipApplicationDto.Income,
                createHardshipApplicationDto.Expenses,
                createHardshipApplicationDto.HardshipReason
            );
            await _hardshipApplicationRepository.CreateAsync(application);

            return HardshipApplicationMapper.ToResponseDto(application, applicant);
        }
        catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            throw new ApiException("An application with this email already exists.", 400);
        }
        
    }

    public async Task<HardshipApplicationResponseDto> UpdateAsync(Guid id, UpdateHardshipApplicationDto updateHardshipApplicationDto)
    {
        var application = await _hardshipApplicationRepository.GetByIdAsync(id);
        if(application == null) throw new KeyNotFoundException($"Application {id} not found.");

        application.Income = updateHardshipApplicationDto.Income;
        application.Expenses = updateHardshipApplicationDto.Expenses;
        application.HardshipReason = updateHardshipApplicationDto.HardshipReason;
        application.Status = updateHardshipApplicationDto.Status;
        application.UpdatedAt = DateTime.UtcNow;

        await _hardshipApplicationRepository.UpdateAsync(application);
        var applicant = await _applicantRepository.GetByIdAsync(application.ApplicantId);
        return HardshipApplicationMapper.ToResponseDto(application, applicant!);
    }

    public async Task DeleteAsync(Guid id)
    {
        var application = await _hardshipApplicationRepository.GetByIdAsync(id);
        if (application == null) throw new KeyNotFoundException($"Application {id} not found.");
        await _hardshipApplicationRepository.DeleteAsync(id);
    }
}