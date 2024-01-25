using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class InstitutionModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string OrganizationWeb { get; set; } = null!;
        public CountryListItem? Country { get; set; } = null!;
        public string? City { get; set; } = null!;
      
        public static implicit operator Institution(InstitutionModel model) => new()
        {
            Name = model.Name,
            Description = model.Description,
            City = model.City,
            CountryId =model.Country.Id
        };
    }
}
