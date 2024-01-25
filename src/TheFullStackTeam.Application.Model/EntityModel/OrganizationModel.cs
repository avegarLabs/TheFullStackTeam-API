using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class OrganizationModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string OrganizationWeb { get; set; } = null!;
        public string LinkedInProfile { get; set; } = null!;
        public string YoutubeProfile { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string Zise { get; set; } = null!;
        public string Sector { get; set; } = null!;
        public string AccountId { get; set; } = null!;
        public CountryListItem Country { get; set; } = null!;
      
        public static implicit operator Organization(OrganizationModel model) => new()
        {
            Name = model.Name,
            Description = model.Description,
            OrganizationWeb = model.OrganizationWeb,
            LinkedInProfile = model.LinkedInProfile,
            YoutubeProfile = model.YoutubeProfile,
            ContactEmail = model.ContactEmail,
            Phone = model.Phone,
            Zise = model.Zise,
            Sector = model.Sector
        };
    }
}
