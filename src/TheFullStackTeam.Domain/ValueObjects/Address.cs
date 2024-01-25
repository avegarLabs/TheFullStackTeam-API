namespace TheFullStackTeam.Domain.ValueObjects;

public class Address : ValueObject
{
    #region Constructor
    public Address(
        string city,
        string country,
        string line1,
        string line2,
        string line3,
        string otherAddressDetails,
        string stateProvinceCountry,
        string zipOrPostalCode)
    {
        City = city;
        Country = country;
        Line1 = line1;
        Line2 = line2;
        Line3 = line3;
        OtherAddressDetails = otherAddressDetails;
        StateProvinceCountry = stateProvinceCountry;
        ZipOrPostalCode = zipOrPostalCode;
    }

    public static readonly Address Empty = new Address("", "", "", "", "", "", "", "");

    #endregion

    public string City { get; private set; }
    public string Country { get; private set; }
    public string Line1 { get; private set; }
    public string Line2 { get; private set; }
    public string Line3 { get; private set; }
    public string OtherAddressDetails { get; private set; }
    public string StateProvinceCountry { get; private set; }
    public string ZipOrPostalCode { get; private set; }


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return City;
        yield return Country;
        yield return Line1;
        yield return Line2;
        yield return Line3;
        yield return OtherAddressDetails;
        yield return StateProvinceCountry;
        yield return ZipOrPostalCode;
    }
}
