using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ValuesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok("Presentation'a api işlemi");
    }
}
