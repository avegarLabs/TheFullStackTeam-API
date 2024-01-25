using System.Linq.Expressions;
using TheFullStackTeam.Application.Model.GET;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class ClientListItem : ClientLookup
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string OwnerMoniker { get; set; } = null!;
        public string LegalName { get; set; } = null!;
        public string? LegalIdentifier { get; set; } 
        public string? Name { get; set; }


        public static implicit operator ClientListItem(Client domainEntity) => new()
        {
            Id = domainEntity.Id,
            Moniker = domainEntity.Moniker,
            Name = domainEntity.Name,
            LegalName = domainEntity.LegalName,
            Email = domainEntity.Email,
            Phone = domainEntity.Phone,
            LegalIdentifier = domainEntity.LegalIdentifier
        };

        public new static Expression<Func<Client, ClientListItem>> Projection =>
            x => new ClientListItem()
            {
                Id = x.Id,
                Moniker = x.Moniker,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                OwnerMoniker = x.ProfessionalId != null ? x.Professional.Moniker: x.Organization.Moniker,
                LegalName = x.LegalName,
                LegalIdentifier = x.LegalIdentifier

            };
    }
}
