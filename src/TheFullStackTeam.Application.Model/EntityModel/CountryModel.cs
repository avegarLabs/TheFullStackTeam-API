using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel;

public class CountryModel
{
    public string CommonName { get; set; } = null!;
    public string OfficialName { get; set; } = null!;
    public string NativeName { get; set; } = null!;
    public string Tld { get; set; } = null!;
    public string Cca2 { get; set; } = null!;
    public string Cca3 { get; set; } = null!;
    public string Ccn3 { get; set; } = null!;

    public static implicit operator Country(CountryModel model) => new()
    {
        CommonName = model.CommonName,
        OfficialName = model.OfficialName,
        NativeName = model.NativeName,
        Tld = model.Tld,
        Cca2 = model.Cca2,
        Cca3 = model.Cca3,
        Ccn3 = model.Ccn3,
    };
}