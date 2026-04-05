using HardshipApp.Models.Entities;
using HardshipApp.Repository.Interfaces;
using HardshipApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace HardshipApp.Repository.Repositories;
public class HardshipApplicationRepository : IHardshipApplicationRepository
{
    private readonly AppDbContext _context;
    public HardshipApplicationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HardshipApplication?> GetByIdAsync(Guid id)
    {
        return await _context.HardshipApplications.FindAsync(id);
    }

    public async Task<IEnumerable<HardshipApplication>> GetAllAsync()
    {
        return await _context.HardshipApplications.ToListAsync();
    }

    public async Task<HardshipApplication> CreateAsync(HardshipApplication hardshipApplication)
    {
        _context.HardshipApplications.Add(hardshipApplication);
        await _context.SaveChangesAsync();
        return hardshipApplication;
    }

    public async Task<HardshipApplication> UpdateAsync(HardshipApplication hardshipApplication)
    {
        _context.HardshipApplications.Update(hardshipApplication);
        await _context.SaveChangesAsync();
        return hardshipApplication;
    }  

    public async Task DeleteAsync(Guid id)
    {
        var application = await _context.HardshipApplications.FindAsync(id);
        if (application == null) return;
        _context.HardshipApplications.Remove(application);
        await _context.SaveChangesAsync();
    } 

}