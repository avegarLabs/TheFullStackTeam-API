using System.Linq.Expressions;
using TheFullStackTeam.Application.Model.ValueObjects;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class UserListItem
    {
        public Guid Id { get; set; }
        public string Moniker { get; set; } = null!;
        public ImageUrlModel? Picture { get; set; }
        public string AccountId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? ContactEmail { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public CountryListItem? Country { get; set; }
        public string? Line1 { get; set; }
        public string? OtherAddressDetails { get; set; }
        public string? StateProvinceCountry { get; set; }
        public string? ZipOrPostalCode { get; set; }
        public List<RolesUserListItem> Roles { get; set; }


        public static implicit operator UserListItem(User domainEntity) => new()
        {
            Id = domainEntity.Id,
            Moniker = domainEntity.Moniker,
            AccountId = domainEntity.AccountId,
            FirstName = domainEntity.FirstName,
            LastName = domainEntity.LastName,
            Name = domainEntity.Name,
            Picture = domainEntity.Picture,
            Phone = domainEntity.Phone,
            ContactEmail = domainEntity.ContactEmail,
            City = domainEntity.Address?.City,
            Line1 = domainEntity.Address?.Line1,
            OtherAddressDetails = domainEntity.Address?.OtherAddressDetails,
            StateProvinceCountry = domainEntity.Address?.StateProvinceCountry,
            ZipOrPostalCode = domainEntity.Address?.ZipOrPostalCode,
            Country = domainEntity.Country
        };


        public static Expression<Func<User, UserListItem>> Projection =>
        x => new UserListItem
        {
            Id = x.Id,
            Moniker = x.Moniker,
            AccountId = x.AccountId,
            Name = x.Name,
            FirstName= x.FirstName,
            LastName= x.LastName,
            Picture = x.Picture,
            Phone = x.Phone,
            ContactEmail = x.ContactEmail,
            City = x.Address != null ? x.Address.City : null,
            Line1 = x.Address != null ? x.Address.Line1 : null,
            OtherAddressDetails = x.Address != null ? x.Address.OtherAddressDetails : null,
            StateProvinceCountry = x.Address != null ? x.Address.StateProvinceCountry : null,
            ZipOrPostalCode = x.Address != null ? x.Address.ZipOrPostalCode : null,
            Country = x.Country,
            Roles = x.UserRoles.AsQueryable().Select(RolesUserListItem.Projection).ToList()

        };
    }
}
