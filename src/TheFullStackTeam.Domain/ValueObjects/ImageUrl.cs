namespace TheFullStackTeam.Domain.ValueObjects;

public class ImageUrl : ValueObject
{
    public string ThumbUrl { get; set; } = null!;
    public string DisplayUrl { get; set; } = null!;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return ThumbUrl;
        yield return DisplayUrl;
    }
}