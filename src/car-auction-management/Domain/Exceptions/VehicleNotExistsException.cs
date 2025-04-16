namespace Domain.Exceptions;

public class VehicleNotExistsException : BusinessRuleException
{
    private const string VehicleNotExistsMessage = "Vehicle doesn't exists in the inventory";

    public VehicleNotExistsException() : base(VehicleNotExistsMessage)
    {
    }
}
