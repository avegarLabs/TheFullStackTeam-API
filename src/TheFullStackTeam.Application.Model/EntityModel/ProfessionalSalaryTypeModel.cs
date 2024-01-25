using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class ProfessionalSalaryTypeModel
    {
        public string PaymentPeriod { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public double Amount { get; set; }

        public static implicit operator ProfessionalSalaryType(ProfessionalSalaryTypeModel model) => new()
        {
            PaymentPeriod = model.PaymentPeriod,
            Amount = model.Amount,
            Currency = model.Currency,
        };
    }
}
