using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class ProfessionalSalaryTypeListItem
    {
        public Guid Id { get; set; }
        public string PaymentPeriod { get; set; } = null!;
        public double Amount { get; set; }
        public string Currency { get; set; } = null!;

        public static implicit operator ProfessionalSalaryTypeListItem(ProfessionalSalaryType domainEntity) => new()
        {
            Id = domainEntity.Id,
            PaymentPeriod = domainEntity.PaymentPeriod,
            Amount = domainEntity.Amount,
            Currency = domainEntity.Currency,

        };

        public static Expression<Func<ProfessionalSalaryType, ProfessionalSalaryTypeListItem>> Projection =>
            x => new ProfessionalSalaryTypeListItem
            {
                Id = x.Id,
                PaymentPeriod = x.PaymentPeriod,
                Amount = x.Amount,
                Currency = x.Currency

            };
    }
}
