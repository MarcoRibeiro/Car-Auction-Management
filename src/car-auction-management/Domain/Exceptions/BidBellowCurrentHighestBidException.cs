namespace Domain.Exceptions;

public class BidBellowCurrentHighestBidException : BusinessRuleException
{
    private const string ValueBellowCurrentHighestBidMessage = "Bid amount must be greater than the current highest bid";

    public BidBellowCurrentHighestBidException() : base(ValueBellowCurrentHighestBidMessage)
    {
    }
}
