using MediatR;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Professionals.Commands.PaymentMethod
{
    public class DeleteProfessionalMethodUserCommand : IRequest<DelatePaymentMethodUserCommandResult>
    {
        public Guid ProfessionalId { get; set; }
        public Guid PaymentMethodId { get; set; }

        public DeleteProfessionalMethodUserCommand(Guid pId, Guid id)
        {
            ProfessionalId = pId;
            PaymentMethodId = id;
        }
    }
}
