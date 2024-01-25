namespace TheFullStackTeam.Domain.ValueObjects;

public class Availability : ValueObject
{
    #region Constructor
    public Availability(
        double hours,
        string description
       )
    {
        Hours = hours;
        Description = description;

    }

    public static readonly Address Empty = new Address("", "", "", "", "", "", "", "");

    #endregion

    public double Hours { get; private set; }
    public string Description { get; private set; }


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Hours;
        yield return Description;

    }
}
