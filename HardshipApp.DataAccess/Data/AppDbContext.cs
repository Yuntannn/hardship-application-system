using HardshipApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HardshipApp.DataAccess.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
    public DbSet<Applicant> Applicants {get; set;}
    public DbSet<HardshipApplication> HardshipApplications {get; set;}
    public DbSet<ApplicationApproval> ApplicationApprovals {get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Applicant
        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(a => a.LastName).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Email).IsRequired().HasMaxLength(200);
            entity.HasIndex(a => a.Email).IsUnique();
        });

        // HardshipApplication
        modelBuilder.Entity<HardshipApplication>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Income).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(a => a.Expenses).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(a => a.Status).IsRequired().HasMaxLength(50);

            // One-to-many relationship: One Applicant -> many HardshipApplications 
            entity.HasOne(a => a.Applicant)
                 .WithMany(a => a.HardshipApplications)
                 .HasForeignKey(a => a.ApplicantId)
                 .OnDelete(DeleteBehavior.Cascade);
        });

        // ApplicationApproval
        modelBuilder.Entity<ApplicationApproval>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Decision).IsRequired().HasMaxLength(50);

            // One-to-one relationship: One Application -> one Approval
            entity.HasOne(a => a.HardshipApplication)
                 .WithOne(a => a.ApplicationApproval)
                 .HasForeignKey<ApplicationApproval>(a => a.ApplicationId)
                 .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasIndex(a => a.ApplicationId).IsUnique();         
        });
    }
}