using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.PaymentMethod;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.PaymentMethod
{
    internal class UpdateProfessionalPaymentMethodCommandHandler : AppRequestHandler, IRequestHandler<UpdateProfessionalPaymentMethodCommand, PaymentMethodResult>
    {
        public UpdateProfessionalPaymentMethodCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<PaymentMethodResult> Handle(UpdateProfessionalPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var paymentMethod = await _context.PaymentMethods.Where(pm => pm.Id == request.PaymentMethodId && pm.ProfessionalId == request.ProfessionalId).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            if (paymentMethod == null)
            {
                throw new NotFoundException(nameof(PaymentMethod), request.PaymentMethodId);
            }
            paymentMethod.BankAccount = request.PaymentMethod.BankAccount;
            paymentMethod.Description = request.PaymentMethod.Description;
            _context.PaymentMethods.Update(paymentMethod);
            await _context.SaveChangesAsync(cancellationToken);
            return new PaymentMethodResult(paymentMethod);

        }
    }
}
