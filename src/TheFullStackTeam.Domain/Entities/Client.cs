using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class Client : NicknamedEntity
{
    public const int NameMaxLength = 100;
    public const int LegalNameMaxLength = 100;
    public const int EmailMaxLength = 100;
    public const int PhoneMaxLength = 20;
    public const int PersonalIdentifierMaxLenght = 30;
    public const int FirstNameMaxLenght = 100;
    public const int LastNameMaxLenght = 100;

    public string Name { get; set; } = null!;
    public string LegalName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Type { get; set; } = null!;

    public string? LegalIdentifier { get; set; }

    public Guid? ProfessionalId { get; set; }

    public virtual Professional? Professional { get; set; } = null!;

    public Guid? OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = null!;
}