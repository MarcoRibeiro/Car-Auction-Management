namespace Presentation.API.Controllers.V1;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(DefaultRoute)]
public class CarsController : BaseController
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public CarsController(ILogger<CarsController> logger)
        : base(logger)
    {
    }

    [HttpGet]
    public IEnumerable<String> Get()
    {
        return Summaries;
    }
}
