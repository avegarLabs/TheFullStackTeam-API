using System.Linq.Expressions;
using TheFullStackTeam.Application.Model.ValueObjects;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class OrganizationListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Moniker { get; set; } = null!;
        public string Zise { get; set; } = null!;
        public string Sector { get; set; } = null!;

        /// <summary>
        /// Image url of the organization
        /// </summary>
        public ImageUrlModel Logo { get; set; } = null!;
        public string OrganizationWeb { get; set; } = null!;
        public string LinkedInProfile { get; set; } = null!;
        public string YoutubeProfile { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public List<OrganizationServiceListItem> organizationServices { get; set; } = new();
        public CountryListItem Country { get; set; }

        public static implicit operator OrganizationListItem(Organization domainEntity) => new()
        {
            Id = domainEntity.Id,
            Name = domainEntity.Name,
            Description = domainEntity.Description,
            Moniker = domainEntity.Moniker,
            Logo = domainEntity.Logo,
            Zise = domainEntity.Zise,
            Sector = domainEntity.Sector,
            OrganizationWeb = domainEntity.OrganizationWeb,
            LinkedInProfile = domainEntity.LinkedInProfile,
            YoutubeProfile = domainEntity.YoutubeProfile,
            ContactEmail = domainEntity.ContactEmail,
            Phone = domainEntity.Phone,
             Country = domainEntity.Country
        };

        public static Expression<Func<Organization, OrganizationListItem>> Projection => x => new OrganizationListItem
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Moniker = x.Moniker,
            Logo = x.Logo,
            Zise = x.Zise,
            Sector = x.Sector,
            OrganizationWeb = x.OrganizationWeb,
            LinkedInProfile = x.LinkedInProfile,
            YoutubeProfile = x.YoutubeProfile,
            ContactEmail = x.ContactEmail,
            Phone = x.Phone,
            organizationServices = x.OrganizationSevices.AsQueryable().Select(OrganizationServiceListItem.Projection).ToList(),
            Country = x.Country
        };
    }
}
