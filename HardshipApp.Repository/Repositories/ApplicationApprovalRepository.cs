using HardshipApp.Models.Entities;
using HardshipApp.Repository.Interfaces;
using HardshipApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace HardshipApp.Repository.Repositories;

public class ApplicationApprovalRepository : IApplicationApprovalRepository
{
    private readonly AppDbContext _context;

    public ApplicationApprovalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationApproval?> GetByApplicationIdAsync(Guid applicationId)
    {
        return await _context.ApplicationApprovals
            .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);
    }

    public async Task<ApplicationApproval> CreateAsync(ApplicationApproval approval)
    {
        _context.ApplicationApprovals.Add(approval);
        await _context.SaveChangesAsync();
        return approval;
    }
}