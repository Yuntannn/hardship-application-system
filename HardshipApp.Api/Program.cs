using HardshipApp.Api.Middlewares;
using HardshipApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using HardshipApp.Repository.Interfaces;
using HardshipApp.Repository.Repositories;
using HardshipApp.Service.Interfaces;
using HardshipApp.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Repositories
builder.Services.AddScoped<IHardshipApplicationRepository, HardshipApplicationRepository>();
builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
// Services
builder.Services.AddScoped<IHardshipApplicationService, HardshipApplicationService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite("Data Source=hardship.db"));

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();