namespace Domain.Exceptions;

public class NotActiveAuctionException : BusinessRuleException
{
    private const string NotActiveAuctionMessage = "There is not active auction for the given vehicle";

    public NotActiveAuctionException() : base(NotActiveAuctionMessage)
    {
    }
}
