using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class PaymentMethodModel
    {
        public string BankAccount { get; set; } = null!;
        public string Description { get; set; } = null!;


        public static implicit operator PaymentMethod(PaymentMethodModel model) => new()
        {
            BankAccount = model.BankAccount,
            Description = model.Description
        };


    }
}
