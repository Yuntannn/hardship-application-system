using HardshipApp.Models.Entities;

namespace HardshipApp.Repository.Interfaces;

public interface IHardshipApplicationRepository
{
    Task<HardshipApplication?> GetByIdAsync(Guid id);
    Task<IEnumerable<HardshipApplication>> GetAllAsync();
    Task<HardshipApplication> CreateAsync(HardshipApplication application);
    Task<HardshipApplication> UpdateAsync(HardshipApplication application);
    Task DeleteAsync(Guid id);
}