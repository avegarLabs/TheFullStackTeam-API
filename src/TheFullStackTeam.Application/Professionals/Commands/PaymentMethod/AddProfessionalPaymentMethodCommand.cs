using MediatR;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Professionals.Commands.PaymentMethod
{
    public class AddProfessionalPaymentMethodCommand : IRequest<PaymentMethodResult>
    {
        public Guid ProfessionalId { get; set; }
        public PaymentMethodModel Model { get; set; }

        public AddProfessionalPaymentMethodCommand(Guid id, PaymentMethodModel model)
        {
            ProfessionalId = id;
            Model = model;
        }
    }
}
