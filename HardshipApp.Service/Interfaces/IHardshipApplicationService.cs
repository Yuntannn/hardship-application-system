using HardshipApp.Service.DTOs;
namespace HardshipApp.Service.Interfaces;
public interface IHardshipApplicationService
{
    Task<HardshipApplicationResponseDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<HardshipApplicationResponseDto>> GetAllAsync();
    Task<HardshipApplicationResponseDto> CreateAsync(CreateHardshipApplicationDto dto);
    Task<HardshipApplicationResponseDto> UpdateAsync(Guid id, UpdateHardshipApplicationDto dto);
}