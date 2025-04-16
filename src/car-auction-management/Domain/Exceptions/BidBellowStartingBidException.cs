namespace Domain.Exceptions;

public class BidBellowStartingBidException : BusinessRuleException
{
    private const string ValueBellowStartingBidMessage = "Bib amount is bellow the starting bid amount";

    public BidBellowStartingBidException() : base(ValueBellowStartingBidMessage)
    {
    }
}
