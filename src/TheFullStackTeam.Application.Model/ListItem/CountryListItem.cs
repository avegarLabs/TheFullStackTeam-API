using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem;

public class CountryListItem
{
    public Guid Id { get; set; }
    public string CommonName { get; set; } = null!;
    public string OfficialName { get; set; } = null!;
    public string NativeName { get; set; } = null!;
    public string Tld { get; set; } = null!;
    public string Cca2 { get; set; } = null!;
    public string Cca3 { get; set; } = null!;
    public string Ccn3 { get; set; } = null!;

    public static implicit operator CountryListItem?(Country? domainEntity) => domainEntity != null
        ? new CountryListItem
        {
            Id = domainEntity.Id,
            CommonName = domainEntity.CommonName,
            OfficialName = domainEntity.OfficialName,
            NativeName = domainEntity.NativeName,
            Tld = domainEntity.Tld,
            Cca2 = domainEntity.Cca2,
            Cca3 = domainEntity.Cca3,
            Ccn3 = domainEntity.Ccn3
        }
        : null;

    public static Expression<Func<Country, CountryListItem>> Projection =>
        x => new CountryListItem
        {
            Id = x.Id,
            CommonName = x.CommonName,
            OfficialName = x.OfficialName,
            NativeName = x.NativeName,
            Tld = x.Tld,
            Cca2 = x.Cca2,
            Cca3 = x.Cca3,
            Ccn3 = x.Ccn3
        };
}