using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.PaymentMethod;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.PaymentMethod
{
    public class DeleteProfessionalPaymentMethodCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalMethodUserCommand, DelatePaymentMethodUserCommandResult>
    {
        public DeleteProfessionalPaymentMethodCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<DelatePaymentMethodUserCommandResult> Handle(DeleteProfessionalMethodUserCommand request, CancellationToken cancellationToken)
        {
            var paymentMethod = await _context.PaymentMethods.Where(pm => pm.Id == request.PaymentMethodId && pm.ProfessionalId == request.ProfessionalId).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            if (paymentMethod == null)
            {
                throw new NotFoundException(nameof(PaymentMethod), request.PaymentMethodId);
            }
            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync(cancellationToken);
            return new DelatePaymentMethodUserCommandResult(true);
        }
    }
}
