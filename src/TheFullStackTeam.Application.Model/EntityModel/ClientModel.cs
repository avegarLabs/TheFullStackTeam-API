using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class ClientModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string LegalName { get; set; } = null!;
        public string? LegalIdentifier { get; set; }

        public static implicit operator Client(ClientModel model) => new()
        {
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            LegalName = model.LegalName,

        };
    }
}
