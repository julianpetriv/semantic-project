using Microsoft.AspNetCore.Mvc;
using SemanticWebProject.BusinessLogic;

namespace SemanticWebProject.Controllers;

[ApiController]
[Route("[controller]")]
public class UniversitiesController : ControllerBase
{
    private readonly UniversityService _service;
    
    public UniversitiesController(UniversityService service)
    {
        _service = service;
    }
    
    [HttpPost]
    [Route("FillUniversityData")]
    public async Task<IActionResult> FillUniversityData()
    {
        var universities = _service.GetUniversitiesFromDbPedia();
        await _service.SaveUniversitiesToDbAsync(universities);

        return Ok();
    }
    
    [HttpGet]
    [Route("GetIAUUniversities")]
    public async Task<IActionResult> GetIAUUniversities()
    {
        return Ok(await _service.GetAllUniversitiesAsync());
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUniversities()
    {
        await _service.DeleteUniversitiesAsync();
        
        return Ok();
    }
}