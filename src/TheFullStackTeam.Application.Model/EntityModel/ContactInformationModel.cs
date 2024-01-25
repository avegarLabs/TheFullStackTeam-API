using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class ContactInformationModel
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? ContactEmail { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public string? OtherAddressDetails { get; set; }
        public string? StateProvinceCountry { get; set; }
        public string? ZipOrPostalCode { get; set; }


        public static implicit operator ContactInformationModel(User domainEntity) => new()
        {
            Name= domainEntity.Name,
            Phone = domainEntity.Phone,
            ContactEmail = domainEntity.ContactEmail,
            City = domainEntity.Address?.City,
            Line1 = domainEntity.Address?.Line1,
            Line2 = domainEntity.Address?.Line2,
            Line3 = domainEntity.Address?.Line3,
            OtherAddressDetails = domainEntity.Address?.OtherAddressDetails,
            StateProvinceCountry = domainEntity.Address?.StateProvinceCountry,
            ZipOrPostalCode = domainEntity.Address?.ZipOrPostalCode,
            Country = domainEntity.Address?.Country
        };

        public static Expression<Func<User, ContactInformationModel>> Projection =>
            x => new ContactInformationModel
            {
                Name = x.Name,
                Phone = x.Phone,
                ContactEmail = x.ContactEmail,
                City = x.Address != null ? x.Address.City : null,
                Line1 = x.Address != null ? x.Address.Line1 : null,
                Line2 = x.Address != null ? x.Address.Line2 : null,
                Line3 = x.Address != null ? x.Address.Line3 : null,
                OtherAddressDetails = x.Address != null ? x.Address.OtherAddressDetails : null,
                StateProvinceCountry = x.Address != null ? x.Address.StateProvinceCountry : null,
                ZipOrPostalCode = x.Address != null ? x.Address.ZipOrPostalCode : null,
                Country = x.Address != null ? x.Address.Country : null,
            };

    }
}
