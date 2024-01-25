using System.Linq.Expressions;
using TheFullStackTeam.Application.Model.ValueObjects;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class ProfessionalListItem
    {
        public Guid id { get; set; }
        public string Name { get; set; } = null!;
        public string AboutMe { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Moniker { get; set; } = null!;
        public string Industry { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;

        public string PersonalWeb { get; set; } = null!;

        public string LinkedInProfile { get; set; } = null!;

        public string YoutubeProfile { get; set; } = null!;

        public List<ProfessionalServiceListItem> Services { get; set; } = new();
        public List<PaymentMethodListItem> PaymentMethods { get; set; } = new();
        public List<ProfessionalLanguegeListItem> ProfessionalLanguegeListItems  { get; set; } = new();
        public CountryListItem? Country { get; set; }
        public ImageUrlModel? Picture { get; set; }


        public static implicit operator ProfessionalListItem(Professional domainEntity) => new()
        {
            id = domainEntity.Id,
            Moniker = domainEntity.Moniker,
            Name = domainEntity.Name,
            AboutMe = domainEntity.AboutMe,
            Phone = domainEntity.Phone,
            ContactEmail = domainEntity.ContactEmail,
            Country= domainEntity.Country,
            Industry = domainEntity.Industry
            
            
        };


        public static Expression<Func<Professional, ProfessionalListItem>> Projection =>
            x => new ProfessionalListItem
            {
                id = x.Id,
                Name = x.Name,
                Moniker = x.Moniker,
                AboutMe = x.AboutMe,
                Title = x.Title,
                Industry = x.Industry,
                Phone = x.Phone,
                ContactEmail = x.ContactEmail,
                PersonalWeb = x.PersonalWeb,
                YoutubeProfile = x.YoutubeProfile,
                LinkedInProfile = x.LinkedInProfile,
                Services = x.ProfessionalSevices.AsQueryable().Select(ProfessionalServiceListItem.Projection).ToList(),
                PaymentMethods = x.PaymentMethods.AsQueryable().Select(PaymentMethodListItem.Projection).ToList(),
                Country = x.Country, 
                ProfessionalLanguegeListItems = x.ProfessionalLanguages.AsQueryable().Select(ProfessionalLanguegeListItem.Projection).ToList(),
                Picture = x.Picture == null ? x.User.Picture : x.Picture
            };
    }
}
