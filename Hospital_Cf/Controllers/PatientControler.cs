using Hospital_Cf.DTOs;
using Hospital_Cf.Exceptions;
using Hospital_Cf.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Cf.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientControler : ControllerBase
{
    private readonly IDbService _dbService;
    
    public PatientControler(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients([FromQuery] string? search)
    {
        try
        {
            var pat = await _dbService.GetAllPatients(search);
            return Ok(pat);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [Route("{pesel}/bedassignments")]
    [HttpPost]
    public async Task<IActionResult> AddBedAssignments([FromQuery] string? pesel, AddBedAssignmentDto dto)
    {
        try
        {
            await _dbService.AddBedAssignment(dto, pesel);
            return Created();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}