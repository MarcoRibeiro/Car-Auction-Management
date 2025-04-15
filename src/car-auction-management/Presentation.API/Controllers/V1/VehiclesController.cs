namespace Presentation.API.Controllers.V1;

using Application.Commands.AddVehicle;
using Application.Commands.PlaceBid;
using Application.Commands.StartAuction;
using Application.Commands.StopAuction;
using Application.Queries.GetVehicle;
using Application.Queries.SearchVehicles;

using Domain.Entities;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Presentation.API.Controllers.V1.DTOs;
using Presentation.API.Extensions;

[ApiController]
[Route(DefaultRoute)]
public class VehiclesController(ISender sender, ILogger<VehiclesController> logger)
    : BaseController(logger)
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Vehicle>> GetAsync([FromRoute] Guid id)
    {
        var vehicle = await sender.Send(new GetVehicleQuery { Id = id });
        return vehicle is null ? NotFound() : vehicle;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicle>>> GetAllAsync(
        [FromQuery] string Model,
        [FromQuery] string Manufacturer,
        [FromQuery] int? Year,
        [FromQuery] VehicleType? type)
    {
        var query = new SearchVehiclesQuery
        {
            Model = Model,
            Manufacturer = Manufacturer,
            Year = Year,
            Type = type.HasValue ? type.Value.ToDomain() : null
        };

        return Ok(await sender.Send(query));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> PostAsync([FromBody] CreateVehicleRequest request)
    {
        var command = new AddVehicleCommand
        {
            Model = request.Model,
            Manufacturer = request.Manufacturer,
            Year = request.Year,
            Startingid = request.StartingPrice,
            Type = request.Type.ToDomain(),
            LoadCapacity = request.LoadCapacity,
            NumberOfDoors = request.NumberOfDoors,
            NumberOfSeats = request.NumberOfSeats
        };

        return await sender.Send(command);
    }

    [HttpPost("{id}/auctions/start")]
    public async Task<ActionResult<Guid>> StartAsync([FromRoute] Guid id)
    {
        return await sender.Send(new StartAuctionCommand { VehicleId = id });
    }

    [HttpPost("{id}/auctions/stop")]
    public async Task<IActionResult> StopAsync([FromRoute] Guid id)
    {
        await sender.Send(new StopAuctionCommand { VehicleId = id });
        return NoContent();
    }

    [HttpPost("{id}/auctions/bid")]
    public async Task<IActionResult> BidAsync([FromRoute] Guid id, [FromBody] PlaceBidRequest placeBidRequest)
    {
        await sender.Send(new PlaceBidCommand { VehicleId = id, Amount = placeBidRequest.Amount });
        return NoContent();
    }
}
