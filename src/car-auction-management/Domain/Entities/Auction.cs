namespace Domain.Entities;

public class Auction
{
    public Guid Id { get; set; }

    public Guid VehicleId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public double StartingPrice { get; set; }

    public double CurrentPrice { get; set; }

    public bool IsActive { get; set; }
}
