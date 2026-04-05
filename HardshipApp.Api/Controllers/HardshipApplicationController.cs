using HardshipApp.Service.DTOs.Application;
using HardshipApp.Service.DTOs.Approval;
using HardshipApp.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HardshipApp.Api.Controllers;
[ApiController]
[Route("api/hardshipapplications")]
public class HardshipApplicationController : ControllerBase
{
    private readonly IHardshipApplicationService _hardshipApplicationService;
    private readonly IApplicationApprovalService _applicationApprovalService;
    public HardshipApplicationController(IHardshipApplicationService hardshipApplicationService, IApplicationApprovalService applicationApprovalService)
    {
        _hardshipApplicationService = hardshipApplicationService;
        _applicationApprovalService = applicationApprovalService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHardshipApplicationDto createHardshipApplicationDto)
    {
        var result = await _hardshipApplicationService.CreateAsync(createHardshipApplicationDto);
        return CreatedAtAction(nameof(GetById), new{id = result.Id}, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _hardshipApplicationService.GetByIdAsync(id);
        if(result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _hardshipApplicationService.GetAllAsync();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHardshipApplicationDto updateHardshipApplicationDto)
    {
        var result = await _hardshipApplicationService.UpdateAsync(id, updateHardshipApplicationDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _hardshipApplicationService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/approvals")]
    public async Task<IActionResult> CreateApproval(Guid id, [FromBody] CreateApprovalDto dto)
    {
        var result = await _applicationApprovalService.CreateAsync(id, dto);
        return Ok(result);
    }

    [HttpGet("{id}/approvals")]
    public async Task<IActionResult> GetApproval(Guid id)
    {
        var result = await _applicationApprovalService.GetByApplicationIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
}