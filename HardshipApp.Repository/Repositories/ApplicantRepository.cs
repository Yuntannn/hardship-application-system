using HardshipApp.Models.Entities;
using HardshipApp.Repository.Interfaces;
using HardshipApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace HardshipApp.Repository.Repositories;
public class ApplicantRepository : IApplicantRepository
{
    private readonly AppDbContext _context;
    public ApplicantRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Applicant?> GetByIdAsync(Guid id)
    {
        return await _context.Applicants.FindAsync(id);
    }
    public async Task<Applicant> CreateAsync(Applicant applicant)
    {
        _context.Applicants.Add(applicant);
        await _context.SaveChangesAsync();
        return applicant;
    }

    public async Task<IEnumerable<Applicant>> GetAllAsync()
    {
        return await _context.Applicants.ToListAsync();
    }

    public async Task<Applicant> UpdateAsync(Applicant applicant)
    {
        _context.Applicants.Update(applicant);
        await _context.SaveChangesAsync();
        return applicant;
    }
}