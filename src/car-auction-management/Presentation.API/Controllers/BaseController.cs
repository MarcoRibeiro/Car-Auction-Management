namespace Presentation.API.Controllers;

using Microsoft.AspNetCore.Mvc;

public class BaseController(ILogger<BaseController> logger) : ControllerBase
{
    protected const string DefaultRoute = "/v1/[controller]";
    protected readonly ILogger<BaseController> Logger = logger;
}
