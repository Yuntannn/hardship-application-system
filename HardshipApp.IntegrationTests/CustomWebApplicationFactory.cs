using HardshipApp.DataAccess.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HardshipApp.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove real DbContext
            var descriptors = services.Where(d =>
            d.ServiceType == typeof(DbContextOptions<AppDbContext>) ||
            d.ServiceType == typeof(AppDbContext) ||
            d.ServiceType.FullName?.Contains("EntityFrameworkCore") == true ||
            d.ServiceType.FullName?.Contains("Sqlite") == true
            ).ToList();

            foreach (var descriptor in descriptors)
                services.Remove(descriptor);
                
            // use InMemory database instead
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // make sure database is created
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        });
    }
}