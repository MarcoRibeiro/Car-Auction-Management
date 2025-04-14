namespace Presentation.API.Controllers.V1;

using Application.Commands.AddVehicle;
using Application.Queries.GetVehicle;

using Domain.Entities;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Presentation.API.Controllers.V1.DTOs;
using Presentation.API.Controllers.V1.Extensions;

[ApiController]
[Route(DefaultRoute)]
public class VehiclesController(ISender sender, ILogger<VehiclesController> logger)
    : BaseController(logger)
{
    [HttpGet]
    public async Task<Vehicle> GetAsync([FromRoute] Guid id)
    {
        return await sender.Send(new GetVehicleQuery { Id = id });
    }

    [HttpPost]
    public async Task<Guid> PostAsync([FromBody] CreateVehicleRequest request)
    {
        var command = new AddVehicleCommand
        {
            Model = request.Model,
            Manufacturer = request.Manufacturer,
            Year = request.Year,
            Startingid = request.StartingPrice,
            Type = request.Type.ToDomain()
        };
        return await sender.Send(command);
    }
}
