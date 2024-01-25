namespace TheFullStackTeam.Application.Exceptions;

public class DomainException : Exception
{
    public DomainException(string Description)
        : base(Description)
    {
    }
}