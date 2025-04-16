namespace Domain.Exceptions;

public class AuctionAlreadyExistsException : BusinessRuleException
{
    private static string AuctionAlreadyExistsMessage = "Already exists an active auction for this vehicle";

    public AuctionAlreadyExistsException() : base(AuctionAlreadyExistsMessage)
    {
    }
}
