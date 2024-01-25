using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class TitleListItem
    {
        public Guid Id { get; set; }
        public string Moniker { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string OrganizationName { get; set; } = null!;
        public string OrganizationCountry { get; set; } = null!;
        public string TitleType { get; set; } = null!;
        public DateTime StartMonthYear { get; set; }
        public DateTime EndMonthYear { get; set; }
        public Guid ProfessionalId { get; set; }
        public Guid? OrganizationId { get; set; }
        public string? Type { get; set; }


        public static implicit operator TitleListItem(Title domainEntity) => new()
        {
            Id = domainEntity.Id,
            Name = domainEntity.Name,
            TitleType = domainEntity.TitleType,
            StartMonthYear = DateTime.Parse(domainEntity.StartMonthYear.ToString()),
            EndMonthYear = DateTime.Parse(domainEntity.EndMonthYear.ToString()),
            ProfessionalId = domainEntity.ProfessionalId,
            OrganizationName = domainEntity.OrganizationId != null ? domainEntity.Organization.Name : domainEntity.Institution.Name,
            OrganizationCountry = domainEntity.OrganizationId != null ? domainEntity.Organization.Country.CommonName : string.Empty,
            OrganizationId = domainEntity.OrganizationId != null ? domainEntity.OrganizationId : domainEntity.InstitutionId,
            Type = domainEntity.OrganizationId != null ? "org" : "inst",
        };

        public static Expression<Func<Title, TitleListItem>> Projection =>
            x => new TitleListItem
            {
                Id = x.Id,
                Name = x.Name,
                TitleType = x.TitleType,
                StartMonthYear = DateTime.Parse(x.StartMonthYear.ToString()),
                EndMonthYear = DateTime.Parse(x.EndMonthYear.ToString()),
                ProfessionalId = x.ProfessionalId,
                OrganizationName = x.OrganizationId != null ? x.Organization.Name : x.Institution.Name,
                OrganizationCountry = x.OrganizationId != null ? x.Organization.Country.CommonName : string.Empty,
                OrganizationId = x.OrganizationId != null ? x.OrganizationId : x.InstitutionId,
                Type = x.OrganizationId != null ? "org" : "inst",
            };
    }
}
