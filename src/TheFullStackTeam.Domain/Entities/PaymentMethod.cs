using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class PaymentMethod : BaseEntity
{
    public const int BankAccountMaxLenght = 100;
    public const int DescriptionMaxLenght = 1024;

    public string BankAccount { get; set; } = null!;
    public string Description { get; set; } = null!;

    public Guid ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;

}
