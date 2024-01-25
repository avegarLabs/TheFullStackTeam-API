using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.POST;

public class PaymentMethodPost
{
    public string BankAccount { get; set; } = null!;
    public string Description { get; set; } = null!;


    public static implicit operator PaymentMethod(PaymentMethodPost model) => new()
    {
        BankAccount = model.BankAccount,
        Description = model.Description
    };
}