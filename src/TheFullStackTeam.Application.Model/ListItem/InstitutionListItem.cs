using System.Linq.Expressions;
using TheFullStackTeam.Application.Model.ValueObjects;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class InstitutionListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Moniker { get; set; } = null!;
        public string? City { get; set; } = null!;
        public CountryListItem Country { get; set; }

        public ImageUrlModel Logo { get; set; } = null!;

        public static implicit operator InstitutionListItem(Institution domainEntity) => new()
        {
            Id = domainEntity.Id,
            Name = domainEntity.Name,
            Description = domainEntity.Description,
            Moniker = domainEntity.Moniker,
            Logo = domainEntity.Logo,
            City = domainEntity.City,
         };

        public static Expression<Func<Institution, InstitutionListItem>> Projection => x => new InstitutionListItem
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Moniker = x.Moniker,
            Logo = x.Logo,
            Country = x.Country,
            City= x.City
        };
    }
}
