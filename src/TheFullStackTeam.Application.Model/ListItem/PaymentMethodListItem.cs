using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class PaymentMethodListItem
    {
        public Guid Id { get; set; }
        public string BankAccount { get; set; } = null!;
        public string Description { get; set; } = null!;

        public static implicit operator PaymentMethodListItem(PaymentMethod domainEntity) => new()
        {
            Id = domainEntity.Id,
            BankAccount = domainEntity.BankAccount,
            Description = domainEntity.Description,
        };

        public static Expression<Func<PaymentMethod, PaymentMethodListItem>> Projection =>
            x => new PaymentMethodListItem
            {
                Id = x.Id,
                BankAccount = x.BankAccount,
                Description = x.Description,
            };



    }
}
