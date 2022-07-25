namespace StocksData.Domain;

public class DomainException : Exception
{
    public DomainException(string comment) : base(comment)
    {}
}