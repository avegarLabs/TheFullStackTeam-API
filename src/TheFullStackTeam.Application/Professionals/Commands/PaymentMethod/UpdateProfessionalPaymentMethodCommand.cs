using MediatR;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Professionals.Commands.PaymentMethod
{
    public class UpdateProfessionalPaymentMethodCommand : IRequest<PaymentMethodResult>
    {
        public Guid ProfessionalId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public PaymentMethodModel PaymentMethod { get; set; }

        public UpdateProfessionalPaymentMethodCommand(Guid ipP, Guid id, PaymentMethodModel paymentMethod)
        {
            ProfessionalId = ipP;
            PaymentMethodId = id;
            PaymentMethod = paymentMethod;
        }
    }
}
