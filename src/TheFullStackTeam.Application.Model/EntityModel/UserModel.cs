using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class UserModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? ContactEmail { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Line1 { get; set; }
        public string? OtherAddressDetails { get; set; }
        public string? StateProvinceCountry { get; set; }
        public string? ZipOrPostalCode { get; set; }
        public List<string> Roles { get; set; } = null!;
        public string AccountId { get; set; } = null!;

        public static implicit operator User(UserModel model) => new()
        {
           FirstName= model.FirstName,
           LastName= model.LastName,
           Name= model.Name,
           ContactEmail= model.ContactEmail,
           Phone= model.Phone,
           AccountId= model.AccountId,
        };

    }
}
