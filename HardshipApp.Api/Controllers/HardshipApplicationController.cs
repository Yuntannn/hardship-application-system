using HardshipApp.Service.DTOs;
using HardshipApp.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace HardshipApp.Api.Controllers;
[ApiController]
[Route("api/hardshipapplications")]
public class HardshipApplicationController : ControllerBase
{
    private readonly IHardshipApplicationService _hardshipApplicationService;
    public HardshipApplicationController(IHardshipApplicationService hardshipApplicationService)
    {
        _hardshipApplicationService = hardshipApplicationService;
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
    public async Task<IActionResult> Update(Guid id, UpdateHardshipApplicationDto updateHardshipApplicationDto)
    {
        var result = await _hardshipApplicationService.UpdateAsync(id, updateHardshipApplicationDto);
        return Ok(result);
    }
}