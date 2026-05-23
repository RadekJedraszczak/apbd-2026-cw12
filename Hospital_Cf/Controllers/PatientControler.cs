using Hospital_Cf.DTOs;
using Hospital_Cf.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Cf.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
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
       var pat =  await _dbService.GetAllPatients(search);
       return Ok(pat);
    }
}