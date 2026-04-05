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
builder.Services.AddScoped<IApplicationApprovalService, ApplicationApprovalService>();

// Services
builder.Services.AddScoped<IHardshipApplicationService, HardshipApplicationService>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<IApplicationApprovalRepository, ApplicationApprovalRepository>();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite("Data Source=hardship.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();
// app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
        .ExcludeFromDescription();
}
app.UseSwaggerUI();

app.Run();
public partial class Program { }