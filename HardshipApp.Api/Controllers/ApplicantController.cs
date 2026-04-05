using HardshipApp.Service.DTOs.Applicant;
using HardshipApp.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HardshipApp.Api.Controllers;

[ApiController]
[Route("api/applicants")]
public class ApplicantsController : ControllerBase
{
    private readonly IApplicantService _applicantService;

    public ApplicantsController(IApplicantService applicantService)
    {
        _applicantService = applicantService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateApplicantDto dto)
    {
        var result = await _applicantService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _applicantService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _applicantService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateApplicantDto dto)
    {
        var result = await _applicantService.UpdateAsync(id, dto);
        return Ok(result);
    }
}