﻿namespace Application.Commands.PlaceBid;

using MediatR;

public record PlaceBidCommand : IRequest
{
    public Guid VehicleId { get; init; }
    
    public Guid UserId { get; init; }

    public double Amount { get; init; }
}
