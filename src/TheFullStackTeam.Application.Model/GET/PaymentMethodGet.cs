using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.GET;

public class PaymentMethodGet
{
    public Guid Id { get; set; }
    public string BankAccount { get; set; } = null!;
    public string Description { get; set; } = null!;

    public static implicit operator PaymentMethodGet(PaymentMethod domainEntity) => new()
    {
        Id = domainEntity.Id,
        BankAccount = domainEntity.BankAccount,
        Description = domainEntity.Description,
    };

    public static Expression<Func<PaymentMethod, PaymentMethodGet>> Projection =>
        x => new PaymentMethodGet
        {
            Id = x.Id,
            BankAccount = x.BankAccount,
            Description = x.Description,
        };
}